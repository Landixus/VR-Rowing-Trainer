using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Get_Seat_Position : MonoBehaviour {
    private SteamVR_TrackedController controller; //controller object
    private Transform sp; //seat position and rotation
    public bool reposition; //tracks if the position has been changed

	// Use this for initialization
	void Start () {
		controller = GetComponent<SteamVR_TrackedController>();
        controller.TriggerClicked += trigger;
		sp = GameObject.Find("PlayerPos").GetComponent<Transform>();
		reposition = false;
	}

    //set position of the seat
    public void seatPos()
    {
        sp.position = controller.transform.position;
		//sp.rotation = controller.transform.rotation;
		sp.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
		Debug.Log("controller position:" + sp.position);
		Debug.Log("controller rotation:" + sp.rotation);
		//reposition = true;
        //controller.enabled = false;
    }

    //checks for trigger press
    private void trigger(object sender, ClickedEventArgs e)
    {
		//if(!reposition) {
		seatPos();
		//}
    }
}
