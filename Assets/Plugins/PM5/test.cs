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

public class test : MonoBehaviour {
	
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
	static extern ERRCODE_T tkcmdsetCSAFE_command(UINT16_T unit_address, UINT16_T cmd_data_size, byte[] cmd_data, ref PTR_T rsp_data_size, byte[] rsp_data);

	// Use this for initialization
	void Start() {

		Initialize();

	}

	// Update is called once per frame
	void Update () {
		float time = Get_Time();
		Debug.Log("Time: " + time);
	}

	// Initialize communication protocols with the Concept2 device
	void Initialize() {

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

	// Detects the number of devices connected
	// Note: Currently only looks for PM5 devices but can be rewritten to search for other PM Models
	void Device_counter() {
		
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

	void Handle_error(ERRCODE_T error_value, String error_identifier) {
		if (error_value != 0) {
			Debug.Log("Failure to load " + error_identifier);
		}
		return;
	}

	float Get_Time() {
		UINT16_T unit_address = 0, cmd_data_size = 0;
		byte[] cmd_data = new byte[4], rsp_data = new byte[4];
		cmd_data[0] = 0xA0;
		UINT16_T rsp_data_size_val = 32;
		PTR_T rsp_data_size = (PTR_T)rsp_data_size_val;
		ERRCODE_T error = 1;
		error = tkcmdsetCSAFE_command(unit_address, cmd_data_size, cmd_data, ref rsp_data_size, rsp_data);
		Handle_error(error, "tkcmdsetCSAFE_command: CSAFE_PM_GET_WORKTIME");
		return rsp_data[3];
	}
	/*
	public static void Test_init() {
		
		[DllImport("PM3CsafeCP.dll")]
		static extern UINT16_T tkcmdsetCSAFE_command(UINT16_T address, UINT16_T cmd_data_size, UINT32_T cmd_data, UINT16_T rsp_data_size, UINT32_T rsp_data);
		test.error = tkcmdsetCSAFE_command(address, cmd_data_size, cmd_data, rsp_data_size, rsp_data);
        if (error == 0) {
            Debug.Log("PM5 CSAFE Get Time Success");
        }
        else {
            Debug.Log("PM5 CSAFE Get Time Failed");
        }
	}
	*/
}
