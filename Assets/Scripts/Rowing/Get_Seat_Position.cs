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
        trackedController = GameObject.Find("Controller (Right)").GetComponent<SteamVR_TrackedController>();
        controller = SteamVR_Controller.Input((int)trackedController.controllerIndex);
        sp = GameObject.Find("PlayerPos").GetComponent<Transform>();
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
    }

    //checks for trigger press
    private static void triggerPress()
    {
        if (controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            seatPos();
            reposition = true;
        }
    }
}
