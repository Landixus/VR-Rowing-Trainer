using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Get_Seat_Position : MonoBehaviour {
    public static SteamVR_TrackedController trackedController; //controller object
    public static SteamVR_Controller.Device controller; //controller
    public static Transform sp; //seat position and rotation
    public static bool reposition; //tracks if the position has been changed

	// Use this for initialization
	void Start () {
		GameObject c = GameObject.FindGameObjectWithTag("ControllerTag");
		if( c == null ){
			Debug.Log("gameobject = null");
		}
	
		trackedController = c.GetComponent<SteamVR_TrackedController>();
		Debug.Log("trackedcontroller:" + trackedController);
        controller = SteamVR_Controller.Input((int)trackedController.controllerIndex);
		Debug.Log("controller:" + controller);
		sp = GameObject.Find("PlayerPos").GetComponent<Transform>();
		Debug.Log("controller transform:" + sp);
		reposition = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!reposition)
        {
            triggerPress();
        }
    }

    //set position of the seat
    public static void seatPos()
    {
        sp.position = controller.transform.pos;
        sp.rotation = controller.transform.rot;
		Debug.Log("controller position:" + sp.position);
	}

    //checks for trigger press
    private static void triggerPress()
    {
        if (controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
			Debug.Log("got press down");
            seatPos();
            reposition = true;
        }
    }
}
