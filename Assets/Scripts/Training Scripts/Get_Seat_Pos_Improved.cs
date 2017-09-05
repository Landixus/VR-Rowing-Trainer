/*  Author: Benjamin Ferguson
    Date: 05/09/17
    Purpose: To set the position of the user in the room so the scene can then 
	be positioned around that location.
*/

using UnityEngine;

public class Get_Seat_Pos_Improved : MonoBehaviour {
	public SteamVR_TrackedObject headset; //headset object, used to get the rotation which the user will be facing in the room
	public SteamVR_TrackedController controller; //controller object
	public Transform playerPos; //position and rotation of the player on the ERG
	public bool reposition; //tracks if the position has been changed

	// Use this for initialization
	void Start() {
		controller.TriggerClicked += Trigger; //adds Trigger function to the trigger event system
		reposition = false; 
	}

	//set position of the player transform to the position of the headset
	public void SeatPos() {
		playerPos.position = headset.transform.position;
		playerPos.rotation = Quaternion.Euler(new Vector3(0, headset.transform.rotation.eulerAngles.y, 0));
	}

	//checks for trigger press and calls function to move the position of the user
	private void Trigger(object sender, ClickedEventArgs e) {
		//if(!reposition) { //used to only reposition once
		SeatPos();
		//}
	}
}
