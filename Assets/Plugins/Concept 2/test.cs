using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class test : MonoBehaviour {
    [DllImport("PM3USBCP.dll")]
    static extern uint TkcmdsetUSB_get_dll_version();
    static extern int TkcmdsetUSB_init();

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
