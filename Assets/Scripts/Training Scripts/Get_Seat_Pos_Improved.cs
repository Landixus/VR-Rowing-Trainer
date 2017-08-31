using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Get_Seat_Pos_Improved : MonoBehaviour {
	public SteamVR_TrackedObject headset;
	public SteamVR_TrackedController controller; //controller object
	public Transform sp; //seat position and rotation
	//public Transform datapos;
	//public Transform pb; //pacing boat position and rotation
	//public Transform pbp; //position and rotation of the empty game object that dictates pacing boat position and rotation
	public bool reposition; //tracks if the position has been changed

	// Use this for initialization
	void Start() {
		//controller = GameObject.Find("Controller(right)").GetComponent<SteamVR_TrackedController>();
		controller.TriggerClicked += Trigger;
		//sp = GameObject.Find("PlayerPos").GetComponent<Transform>();
		reposition = false;
	}

	//set position of the seat
	public void SeatPos() {
		sp.position = headset.transform.position;
		sp.rotation = Quaternion.Euler(new Vector3(0, headset.transform.rotation.eulerAngles.y, 0));
		//datapos.position = headset.transform.position;
		//datapos.rotation = Quaternion.Euler(new Vector3(0, headset.transform.rotation.eulerAngles.y, 0));
		//Debug.Log("controller position:" + sp.position);
		//Debug.Log("controller rotation:" + sp.rotation);
		//reposition = true;
		//controller.enabled = false;

		//Adjusts position and rotation of pacing boat
		//pb.position = pbp.position;
		//pb.rotation = Quaternion.Euler(new Vector3(sp.rotation.eulerAngles.x, sp.rotation.eulerAngles.y, sp.rotation.eulerAngles.z));
	}

	//checks for trigger press
	private void Trigger(object sender, ClickedEventArgs e) {
		//if(!reposition) {
		SeatPos();
		//}
	}
}
