using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

// Data Type Definitions
using UINT16_T = System.UInt16;
using UINT32_T = System.UInt32;
using ERRCODE_T = System.UInt16;

public class test : MonoBehaviour {
	public ERRCODE_T error;
    public UINT16_T address, cmd_data_size, rsp_data_size;
    public UINT32_T cmd_data, rsp_data;
    // Unused dll's and commands
    [DllImport("PM3DDICP.dll")]
    //[DllImport("PM3USBCP.dll")]
    static extern UINT16_T tkcmdsetDDI_init();
    //static extern int tkcmdsetDDI_serial_number();

    [DllImport("PM3CsafeCP.dll")]
    static extern UINT16_T tkcmdsetCSAFE_init_protocol(UINT16_T timeout);         // Initializes the device to accept CSAFE input
    [DllImport("PM3CsafeCP.dll")]
    static extern UINT16_T tkcmdsetCSAFE_command(UINT16_T address, UINT16_T cmd_data_size, UINT32_T cmd_data, UINT16_T rsp_data_size, UINT32_T rsp_data);

    // Use this for initialization
    void Start () {

		error = tkcmdsetDDI_init();
		if (error == 0) {
			Debug.Log("PM5 CSAFE Init Success");
		} else {
			Debug.Log("PM5 CSAFE Init Failed");
		}

		error = tkcmdsetCSAFE_init_protocol(10000);               // 10000 = Timeout in milliseconds
        if (error == 0) {
            Debug.Log("PM5 CSAFE Init Success");
        } else {
            Debug.Log("PM5 CSAFE Init Failed");
        }

        // Get time
        address = 0xA0;
        cmd_data_size = 0x80;
        cmd_data = 0x80;
        rsp_data_size = 0x80;
        rsp_data = 0;
         
        error = tkcmdsetCSAFE_command(address, cmd_data_size, cmd_data, rsp_data_size, rsp_data);
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
