using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialise_Position : MonoBehaviour {
    public static Vector3 triggerPosition; //position of the controller when trigger is pressed
	public static Vector3 triggerRotation; //rotation of the contoller
    public static SteamVR_TrackedObject trackedObject; //boat object
    public static SteamVR_Controller.Device controller; //controller 
    public static bool reposition; //tracks if the position has been changed

	// Use this for initialization
	void Start () {
        trackedObject = GetComponent<SteamVR_TrackedObject>().;
	}
	
	// Update is called once per frame
	void Update () {
        controller = SteamVR_Controller.Input((int)trackedObject.index);
        if (controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            changeOrigin();
        }
    }

    //get position of the seat
    public static Vector3 seatPosition()
    {
        triggerPosition = controller.transform.pos;
		triggerRotation = 
        return triggerPosition;
    }

    //repostions the origin point with respect to the seat position
    public static void changeOrigin()
    {
        trackedObject.transform.position = seatPosition();
    }
}
