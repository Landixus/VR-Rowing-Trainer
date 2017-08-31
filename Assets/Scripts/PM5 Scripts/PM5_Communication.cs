/*
 * Author: Grant Burgess
 * Date: 04/08/17
 * Purpose: Script to handle all communication with the Concept II PM5 Ergometer 
 */

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using System;

// Data Type Definitions
using UINT16_T = System.UInt16;
using UINT32_T = System.UInt32;
using ERRCODE_T = System.UInt16;
using PTR_T = System.UIntPtr;

public class PM5_Communication : MonoBehaviour {

	public double current_Speed, current_Power, current_MinPer500m;
	public UINT32_T current_Time, current_Distance, current_Cadence;
	
	// Variables for speed calculation and idle sensing
	private static UINT32_T last_Distance = 0, last_Time = 0;
	private static LinkedList <double> speed_List = new LinkedList<double>();
	private static LinkedList<double> power_List = new LinkedList<double>();
	private static int idle_Counter;

	// Import of all dll functions required for communication with PM device
	[DllImport("PM3DDICP.dll")]
	// Initializes the Command Set Toolkit functions.
	static extern ERRCODE_T tkcmdsetDDI_init();
	[DllImport("PM3CsafeCP.dll")]
	//Initializes the DLL error code interface and configures the CSAFE protocol.
	static extern ERRCODE_T tkcmdsetCSAFE_init_protocol(UINT16_T timeout);
	[DllImport("PM3DDICP.dll")]
	// Discover all PM3 devices connected to the PC via various media interfaces.
	static extern ERRCODE_T tkcmdsetDDI_discover_pm3s(byte[] productname, UINT16_T address, ref PTR_T num_units);
	[DllImport("PM3CsafeCP.dll")]
	// Sends a CSAFE command to a PM device and returns the response data.
	static extern ERRCODE_T tkcmdsetCSAFE_command(UINT16_T unit_address, UINT16_T cmd_data_size, UINT32_T[] cmd_data, ref PTR_T rsp_data_size, UINT32_T[] rsp_data);

	// Use this for initialization
	void Start() {
		Initialize();
		for (int i = 1; i <= 5; i++) {
			speed_List.AddFirst(0.0);
		}
		for (int i = 1; i <= 2; i++) {
			power_List.AddFirst(0.0);
		}
	}

	// Update is called once per frame
	void Update() {
		Get_Speed();
	}

	// Initialize communication protocols with the Concept2 device
	private static void Initialize() {

		ERRCODE_T error = 1;
		error = tkcmdsetDDI_init();
		Handle_error(error, "tkcmdsetDDI_init");
		
		error = 1;
		error = tkcmdsetCSAFE_init_protocol(1000);			// Timeout set to 1000ms
		Handle_error(error, "tkcmdsetCSAFE_init_protocol");
		Device_counter();

		return;
	}

	// Error handling function to display command name that caused error
	private static void Handle_error(ERRCODE_T error_value, String error_identifier) {
		if (error_value != 0) {
			Debug.Log("Failure to load " + error_identifier);
		}
		return;
	}

	// Detects the number of devices connected
	// Note: Currently only looks for PM5 devices but can be rewritten to search for other PM Models
	private static void Device_counter() {
		string product_name_str = "Concept2 Performance Monitor 5 (PM5)";
		byte[] product_name_ptr = System.Text.Encoding.UTF8.GetBytes(product_name_str);
		UINT16_T address = 0;
		UINT16_T num_units = 0;
		PTR_T num_units_ptr = (PTR_T)num_units;
		ERRCODE_T error = 1;
		error = tkcmdsetDDI_discover_pm3s(product_name_ptr, address, ref num_units_ptr);
		Handle_error(error, "tkcmdsetDDI_discover_pm3s");
		Debug.Log("PM5's connected: " + num_units_ptr.ToString());
		return;
	}

	// Requests the workout duration from the PM device - CSAFE_GETTWORK_CMD = 0xA0
	private static UINT32_T Get_Time() {
		UINT16_T unit_address = 0, cmd_data_size = 1;
		UINT32_T[] cmd_data = new UINT32_T[] { 0xA0, 0, 0, 0, 0 }, rsp_data = new UINT32_T[] { 0, 0, 0, 0, 0 };
		UINT16_T rsp_data_size_val = 64;
		PTR_T rsp_data_size = (PTR_T)rsp_data_size_val;
		ERRCODE_T error = 1;
		error = tkcmdsetCSAFE_command(unit_address, cmd_data_size, cmd_data, ref rsp_data_size, rsp_data);
		Handle_error(error, "tkcmdsetCSAFE_command: CSAFE_PM_GET_WORKTIME");
		// return the seconds from byte 4, minutes from byte 3 and hours from byte 2, all in seconds
		return rsp_data[4] + (rsp_data[3] * 60) + (rsp_data[2] * 3600);
	}

	// Requests the distance travelled from the PM device - CSAFE_GETHORIZONTAL_CMD = 0xA1
	private static UINT32_T Get_Distance() {
		UINT16_T unit_address = 0, cmd_data_size = 1;
		UINT32_T[] cmd_data = new UINT32_T[] { 0xA1, 0, 0 }, rsp_data = new UINT32_T[] { 0, 0, 0 };
		UINT16_T rsp_data_size_val = 64;
		PTR_T rsp_data_size = (PTR_T)rsp_data_size_val;
		ERRCODE_T error = 1;
		error = tkcmdsetCSAFE_command(unit_address, cmd_data_size, cmd_data, ref rsp_data_size, rsp_data);
		Handle_error(error, "tkcmdsetCSAFE_command: CSAFE_PM_GET_WORKDISTANCE");
		return rsp_data[2];
	}

	// Requests the current power from the PM device - CSAFE_GETPOWER_CMD = 0xB4
	private static UINT32_T Get_Power() {
		UINT16_T unit_address = 0, cmd_data_size = 1;
		UINT32_T[] cmd_data = new UINT32_T[] { 0xB4, 0, 0 }, rsp_data = new UINT32_T[] { 0, 0, 0 };
		UINT16_T rsp_data_size_val = 64;
		PTR_T rsp_data_size = (PTR_T)rsp_data_size_val;
		ERRCODE_T error = 1;
		error = tkcmdsetCSAFE_command(unit_address, cmd_data_size, cmd_data, ref rsp_data_size, rsp_data);
		Handle_error(error, "tkcmdsetCSAFE_command: CSAFE_GETPOWER_CMD");
		return rsp_data[2];
	}

	// Requests the current cadence from the PM device - CSAFE_GETCADENCE_CMD = 0xA7
	private static UINT32_T Get_Cadence() {
		UINT16_T unit_address = 0, cmd_data_size = 1;
		UINT32_T[] cmd_data = new UINT32_T[] { 0xA7, 0, 0 }, rsp_data = new UINT32_T[] { 0, 0, 0 };
		UINT16_T rsp_data_size_val = 64;
		PTR_T rsp_data_size = (PTR_T)rsp_data_size_val;
		ERRCODE_T error = 1;
		error = tkcmdsetCSAFE_command(unit_address, cmd_data_size, cmd_data, ref rsp_data_size, rsp_data);
		Handle_error(error, "tkcmdsetCSAFE_command: CSAFE_GETCADENCE_CMD");
		return rsp_data[2];
	}

	// Calculates the current change of speed of the rower once per second.
	// Averages out the last 5 readings in order to eliminate fluctuation.
	private void Get_Speed() {
		current_Time = Get_Time();
		current_Distance = Get_Distance();
		double new_Speed, speed_Total = 0;
		if ((current_Time - last_Time) > 0) {
			new_Speed = ((double)current_Distance - (double)last_Distance) / (current_Time - last_Time);
			last_Distance = current_Distance;
			last_Time = current_Time;
			speed_List.AddFirst(new_Speed);
			speed_List.RemoveLast();
			foreach (var speed in speed_List) {
				speed_Total += speed;
			}
			current_Speed = speed_Total / speed_List.Count;
			Session_Stats();
			//Debug.Log("Speed (m/s): " + current_Speed.ToString("N3"));
			idle_Counter = 0;
		} else {
			idle_Counter++;
			if (idle_Counter >= 200) {						// 200 update cycles to register no rowing has been done
				speed_List.AddFirst(0.0);
				speed_List.RemoveLast();
				foreach (var speed in speed_List) {
					speed_Total += speed;
				}
				current_Speed = speed_Total / 5;
				idle_Counter = 0;
				if (current_Speed == 0) {
					Debug.Log("Rower has stopped.");
				} else {
					Debug.Log("Rowing stopping. Speed (m/s): " + current_Speed.ToString("N3"));
				}
			}
		}
		return;
	}

	// Added session statistics for summary calculations
	private void Session_Stats() {
		current_Power = Get_Power();
		current_MinPer500m = (500 / current_Speed) / 60;	// In minutes
		current_Cadence = Get_Cadence();
	}
}
