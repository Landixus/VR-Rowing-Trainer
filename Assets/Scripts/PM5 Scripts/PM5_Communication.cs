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

	public static DateTime startTime;

	public static double rawSpeed, rawPower;
	public static UINT32_T rawTime, rawDistance, rawCadence;
	public static UINT32_T prevTime, prevDist;

	// Variables for speed calculation and idle sensing
	private static UINT32_T last_Distance = 0, last_Time = 0;
	public static LinkedList <double> speed_List = new LinkedList<double>();
	private static LinkedList<double> power_List = new LinkedList<double>();
	private static int idle_Counter;

	// Import of all dll functions required for communication with PM device
#if UNITY_EDITOR
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
# else
	[DllImport("PM3DDICP")]
	// Initializes the Command Set Toolkit functions.
	static extern ERRCODE_T tkcmdsetDDI_init();
	[DllImport("PM3CsafeCP")]
	//Initializes the DLL error code interface and configures the CSAFE protocol.
	static extern ERRCODE_T tkcmdsetCSAFE_init_protocol(UINT16_T timeout);
	[DllImport("PM3DDICP")]
	// Discover all PM3 devices connected to the PC via various media interfaces.
	static extern ERRCODE_T tkcmdsetDDI_discover_pm3s(byte[] productname, UINT16_T address, ref PTR_T num_units);
	[DllImport("PM3CsafeCP")]
	// Sends a CSAFE command to a PM device and returns the response data.
	static extern ERRCODE_T tkcmdsetCSAFE_command(UINT16_T unit_address, UINT16_T cmd_data_size, UINT32_T[] cmd_data, ref PTR_T rsp_data_size, UINT32_T[] rsp_data);
#endif

	// Use this for initialization
	void Start() {
		Initialize();
		// Setting the status to the workout state seemed to fix a recurring bug on first use
		Set_Status_GoToWorkout();
		Reset_ERG();
	}

	// Update is called once per frame
	void Update() {
		Get_Speed();
	}

	public static void initTime() {
		startTime = DateTime.Now;
	}

	// static function to call to get the raw values
	public static void getRawValues(out double time, out double distance, out double cadence, out double speed, out double power) {

		prevTime = rawTime;
		prevDist = rawDistance;

		//rawTime = Get_Time();
		rawTime = (UINT32_T) ((DateTime.Now - startTime).TotalMilliseconds);
		//rawDistance = Get_Distance();
		//rawCadence = Get_Cadence();
		//rawPower = Get_Power();

		//if ((rawTime - prevTime) > 0) {
		//	rawSpeed = (double)(rawDistance - prevDist) / (rawTime - prevTime);

		//	Debug.Log("Speed (m/s): " + rawSpeed.ToString("N3"));
		//}

		// Output the values
		time = rawTime;
		distance = rawDistance;
		cadence = rawCadence;
		speed = rawSpeed;
		power = rawPower;

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
			Debug.Log("Failure to load: " + error_identifier);
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
		UINT32_T[] cmd_data = new UINT32_T[] { 0xA1, 0, 0, 0 }, rsp_data = new UINT32_T[] { 0, 0, 0, 0 };
		UINT16_T rsp_data_size_val = 64;
		PTR_T rsp_data_size = (PTR_T)rsp_data_size_val;
		ERRCODE_T error = 1;
		error = tkcmdsetCSAFE_command(unit_address, cmd_data_size, cmd_data, ref rsp_data_size, rsp_data);
		Handle_error(error, "tkcmdsetCSAFE_command: CSAFE_PM_GET_WORKDISTANCE");
		return rsp_data[2] + (rsp_data[3] * 255);	
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
			if (new_Speed < 0) {
				speed_List.AddFirst(0.0);
				speed_List.RemoveLast();
			} else {
				speed_List.AddFirst(new_Speed);
				speed_List.RemoveLast();
			}
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

	// Sets the PM5 Status to the Finished state - Only runs if PM5 is currently InUse state - 0x86
	// Added the InUse state to the beginning of this code to eliminate a recurring bug - 0x85
	private void Set_Status_Finished() {
		UINT16_T unit_address = 0, cmd_data_size = 2;
		UINT32_T[] cmd_data = new UINT32_T[] { 0x85, 0x86, 0, 0, 0 }, rsp_data = new UINT32_T[] { 0, 0, 0, 0, 0 };
		UINT16_T rsp_data_size_val = 64;
		PTR_T rsp_data_size = (PTR_T)rsp_data_size_val;
		ERRCODE_T error = 1;
		error = tkcmdsetCSAFE_command(unit_address, cmd_data_size, cmd_data, ref rsp_data_size, rsp_data);
		Handle_error(error, "tkcmdsetCSAFE_command: CSAFE_GOFINISHED_CMD");
		return;
	}

	// Sets the PM5 Status to the Idle state to allow for ID Entry - 0x82
	// Added extra command for GoHaveID state - 0x83
	private void Set_Status_GoIdle() {
		UINT16_T unit_address = 0, cmd_data_size = 2;
		UINT32_T[] cmd_data = new UINT32_T[] { 0x82, 0x83, 0, 0, 0 }, rsp_data = new UINT32_T[] { 0, 0, 0, 0, 0 };
		UINT16_T rsp_data_size_val = 64;
		PTR_T rsp_data_size = (PTR_T)rsp_data_size_val;
		ERRCODE_T error = 1;
		error = tkcmdsetCSAFE_command(unit_address, cmd_data_size, cmd_data, ref rsp_data_size, rsp_data);
		Handle_error(error, "tkcmdsetCSAFE_command: CSAFE_GOIDLE_CMD");
		return;
	}

	// Resets the PM5 - Only runs if status is in Finished state
	// Currently unused as it was not required in the current working format 08/09/2017
	private void Set_Status_Reset() {
		UINT16_T unit_address = 0, cmd_data_size = 1;
		UINT32_T[] cmd_data = new UINT32_T[] { 0x81, 0, 0, 0, 0 }, rsp_data = new UINT32_T[] { 0, 0, 0, 0, 0 };
		UINT16_T rsp_data_size_val = 64;
		PTR_T rsp_data_size = (PTR_T)rsp_data_size_val;
		ERRCODE_T error = 1;
		error = tkcmdsetCSAFE_command(unit_address, cmd_data_size, cmd_data, ref rsp_data_size, rsp_data);
		Handle_error(error, "tkcmdsetCSAFE_command: CSAFE_RESET_CMD");
		return;
	}

	// Sets the PM5 ID - Only runs if status is in Idle state
	private void Set_Status_GoHaveID() {
		UINT16_T unit_address = 0, cmd_data_size = 1;
		UINT32_T[] cmd_data = new UINT32_T[] { 0x83, 0, 0, 0, 0 }, rsp_data = new UINT32_T[] { 0, 0, 0, 0, 0 };
		UINT16_T rsp_data_size_val = 64;
		PTR_T rsp_data_size = (PTR_T)rsp_data_size_val;
		ERRCODE_T error = 1;
		error = tkcmdsetCSAFE_command(unit_address, cmd_data_size, cmd_data, ref rsp_data_size, rsp_data);
		Handle_error(error, "tkcmdsetCSAFE_command: CSAFE_GOHAVEID_CMD");
		return;
	}

	// Sets the PM5 to the In Use State 
	private void Set_Status_GoInUse() {
		UINT16_T unit_address = 0, cmd_data_size = 1;
		UINT32_T[] cmd_data = new UINT32_T[] { 0x85, 0, 0, 0, 0 }, rsp_data = new UINT32_T[] { 0, 0, 0, 0, 0 };
		UINT16_T rsp_data_size_val = 64;
		PTR_T rsp_data_size = (PTR_T)rsp_data_size_val;
		ERRCODE_T error = 1;
		error = tkcmdsetCSAFE_command(unit_address, cmd_data_size, cmd_data, ref rsp_data_size, rsp_data);
		Handle_error(error, "tkcmdsetCSAFE_command: CSAFE_GOINUSE_CMD");
		return;
	}

	// Sets the PM5 to the workout screen, currently set to program 1 - 2000m  
	private void Set_Status_GoToWorkout() {
		UINT16_T unit_address = 0, cmd_data_size = 4;
		UINT32_T[] cmd_data = new UINT32_T[] { 0x24, 2, 1, 0, 0 }, rsp_data = new UINT32_T[] { 0, 0, 0, 0, 0 };
		UINT16_T rsp_data_size_val = 64;
		PTR_T rsp_data_size = (PTR_T)rsp_data_size_val;
		ERRCODE_T error = 1;
		error = tkcmdsetCSAFE_command(unit_address, cmd_data_size, cmd_data, ref rsp_data_size, rsp_data);
		Handle_error(error, "tkcmdsetCSAFE_command: CSAFE_SETPROGRAM_CMD");
		return;
	}

	// Resets the PM5 and all variables
	private void Reset_ERG() {
		/* Kept this incase we had issues with a recurring bug in getting the PM5 to reset
		Set_Status_GoInUse(); //0x85
		Set_Status_Finished(); //0x86
		Set_Status_Reset(); //0x81'
		Set_Status_GoIdle(); //0x82
		Set_Status_GoInUse(); //0x85
		Set_Status_Finished(); //0x86
		Set_Status_Reset(); //0x81'
		Set_Status_GoIdle(); //0x82
		Set_Status_GoHaveID(); //0x83
		*/

		Set_Status_Finished();
		Set_Status_GoIdle();
		Set_Status_GoHaveID();
		Set_Status_GoInUse();
		Set_Status_GoToWorkout();
		
		// Set all variables to zero for reset
		current_Speed = 0;
		current_Power = 0;
		current_MinPer500m = 0;
		current_Time = 0; 
		current_Distance = 0; 
		current_Cadence = 0;
		idle_Counter = 0;
		last_Distance = 0;
		last_Time = 0;

		// Cleared speed and power lists and re-initialized with zeros
		speed_List.Clear();
		power_List.Clear();
		for (int i = 1; i <= 5; i++) {
			speed_List.AddFirst(0.0);
		}
		for (int i = 1; i <= 2; i++) {
			power_List.AddFirst(0.0);
		}
		return;
	}
}
