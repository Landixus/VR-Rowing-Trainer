using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Get_Seat_Position : MonoBehaviour {
    public static SteamVR_TrackedController controller; //controller object
    public static Transform sp; //seat position and rotation
    public static bool reposition; //tracks if the position has been changed

	// Use this for initialization
	void Start () {
		controller = GetComponent<SteamVR_TrackedController>();
        controller.TriggerClicked += trigger;
		sp = GameObject.Find("PlayerPos").GetComponent<Transform>();
		reposition = false;
	}

    //set position of the seat
    public static void seatPos()
    {
        sp.position = controller.transform.position;
        sp.rotation = controller.transform.rotation;
		Debug.Log("controller position:" + sp.position);
        reposition = true;
        controller.enabled = false;
    }

    //checks for trigger press
    private static void trigger(object sender, ClickedEventArgs e)
    {
       Debug.Log("got press down");
       seatPos();
    }
}
