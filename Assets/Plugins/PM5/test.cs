using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

// Data Type Definitions
using INT8_T = System.Char;
using UINT16_T = System.UInt16;
using UINT32_T = System.UInt32;
using ERRCODE_T = System.UInt16;
using PTR_T = System.Text.StringBuilder;

public class test : MonoBehaviour {
    public ERRCODE_T error;

    public UINT16_T address, cmd_data_size, rsp_data_size;
    public UINT32_T[] cmd_data;
    public UINT32_T[] rsp_data;
    public PTR_T ptr;
    public UINT16_T output;



    [DllImport("PM3DDICP.dll")]
    static extern ERRCODE_T tkcmdsetDDI_init();
    [DllImport("PM3CsafeCP.dll")]
    static extern ERRCODE_T tkcmdsetCSAFE_init_protocol(UINT16_T timeout);         // Initializes the device to accept CSAFE input

    [DllImport("PM3DDICP.dll")]
    static extern ERRCODE_T tkcmdsetDDI_discover_pm3s(INT8_T productname, UINT16_T address, UINT16_T num_units);
    [DllImport("PM3CsafeCP.dll")]
    static extern ERRCODE_T tkcmdsetCSAFE_command(UINT16_T address);//, UINT16_T cmd_data_size, UINT32_T[] cmd_data, UINT16_T rsp_data_size, UINT32_T[] rsp_data);

    // Use this for initialization
    void Start () {
        
        error = 1;
        error = tkcmdsetDDI_init(); 
        if (error == 0)
        {
            Debug.Log("PM5 DDI Init Success");
        }
        else
        {
            Debug.Log("PM5 DDI Init Failed");
        }

        error = 1;
		
		error = tkcmdsetCSAFE_init_protocol(10000);               // 10000 = Timeout in milliseconds
        if (error == 0) {
            Debug.Log("PM5 CSAFE Init Success");
        } else {
            Debug.Log("PM5 CSAFE Init Failed");
        }
        /*
        error = 1;
        address = 0;
        
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
        */
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
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
