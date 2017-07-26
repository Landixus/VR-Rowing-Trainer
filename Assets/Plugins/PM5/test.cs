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
	static extern ERRCODE_T tkcmdsetDDI_discover_pm3s(byte[] productname, UINT16_T address, ref UIntPtr num_units); 
	
    [DllImport("PM3CsafeCP.dll")]
	// Sends a CSAFE command to a PM device and returns the response data.
	static extern ERRCODE_T tkcmdsetCSAFE_command(UINT16_T address);		
	
	// Use this for initialization
	void Start() {

		Inititialize();
		/*
        ERRCODE_T error = 500;
			
		string str = "Concept2 Performance Monitor 5 (PM5)";
		byte[] productname = System.Text.Encoding.UTF8.GetBytes(str);
		int testInt = 999;
		UIntPtr testPtr = (UIntPtr)testInt;
		UINT16_T address = 0;

		error = 1;
		error = tkcmdsetDDI_discover_pm3s(productname, address, ref testPtr);
		if (error == 0) {
			Debug.Log("PM5 DDI Discover PM Success");
		} else {
			Debug.Log("PM5 DDI Discover PM Failed");
		}
		string hmm = testPtr.ToString();
		Debug.Log("Devices: " + hmm);
		
		
        string str = "test";
     
        unsafe
        {
            fixed (char * strptr = str)
            {
                error = tkcmdsetDDI_discover_pm3s((INT8_T)strptr, address, output);
            }
        }
        if (error == 0)
        {
            Debug.Log("PM5 DDI Discover PM Success");
        }
        else
        {
            Debug.Log("PM5 DDI Discover PM Failed");
        }
        
        // Get time
        address = 0xA0;
        cmd_data_size = 0;
        //cmd_data = new UINT32_T[5];
        rsp_data_size = 64;
        //rsp_data = new UINT32_T[5];

        

		
		error = 1;

        error = tkcmdsetCSAFE_command(address);//, cmd_data_size, cmd_data, rsp_data_size, rsp_data);
        if (error == 0) {
            Debug.Log("PM5 CSAFE Get Time Success");
        }
        else {
            Debug.Log("PM5 CSAFE Get Time Failed");
        }
		*/
	}

	// Update is called once per frame
	void Update () {
		
	}

	void Inititialize() {

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
