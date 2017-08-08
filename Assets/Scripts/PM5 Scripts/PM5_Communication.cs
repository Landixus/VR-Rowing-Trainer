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

	public double current_Speed = 0;
	public double set_Speed; // Used to set a manual speed for testing purposes
	// Counter controls how often current_Speed is changed
	// Timeout senses the rower hasn't rowed so speed can be set to 0
	private static UINT32_T counter = 1, last_Distance = 0, timeout = 0;

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

	}

	// Update is called once per frame
	void Update() {
		if (set_Speed != 0) {
			current_Speed = set_Speed;
		} else {
			Get_Speed();
			timeout++; 
		}
	}

	// Initialize communication protocols with the Concept2 device
	private static void Initialize() {

		ERRCODE_T error = 1;
		error = tkcmdsetDDI_init();
		Handle_error(error, "tkcmdsetDDI_init");

		// Timeout set to 1000ms
		error = 1;
		error = tkcmdsetCSAFE_init_protocol(1000);
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
		Handle_error(error, "tkcmdsetCSAFE_command: CSAFE_PM_GET_WORKTIME");
		return rsp_data[2];
	}

	// Calculates the current change of speed of the rower for every interval.
	private void Get_Speed() {
		UINT32_T time = Get_Time();
		UINT32_T current_Distance = Get_Distance();
		if (counter <= time) {
			current_Speed = ((double)current_Distance - (double)last_Distance) / 2;
			last_Distance = current_Distance;
			counter += 2; // This can be used to change speed every 'n' seconds
			timeout = 0;
			Debug.Log("Speed (m/s): " + current_Speed);
		} else {
			if (timeout >= 100) {
				if (current_Speed != 0) {
					current_Speed = 0;
					Debug.Log("Speed (m/s): " + current_Speed + " Rower stopped?");
				}
			} 
		}
		return;
	}

}
