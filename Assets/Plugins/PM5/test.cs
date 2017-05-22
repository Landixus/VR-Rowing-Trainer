using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class test : MonoBehaviour {
	public int err;

	[DllImport("PM3DDICP.dll")]
	//[DllImport("PM3USBCP.dll")]
	//[DllImport("PM3CsafeCP.dll")]
	static extern int tkcmdsetDDI_init();
	//static extern int tkcmdsetDDI_serial_number();

	// Use this for initialization
	void Start () {
		err = tkcmdsetDDI_init();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log("PM5 Init" + err);
	}
}
