/*  Author: Benjamin Ferguson
    Date: 05/09/17
    Purpose: To set the position of the user in the room so the scene can then 
	be positioned around that location.
*/

using UnityEngine;

public class Get_Seat_Pos_Improved : MonoBehaviour {
	public SteamVR_TrackedObject headset; // Headset object, used to get the rotation which the user will be facing in the room
	public SteamVR_TrackedController controller; // Controller object, used to get the trigger press
	public Transform playerPos; // Position and rotation of the player on the ERG

	public float yPos = 0;

	// Used for initialisation
	void Start() {
        // Used to add the Trigger function to the trigger event system
        controller.TriggerClicked += Trigger; 
	}

	// Set position of the player transform to the position and rotation of the headset
	public void SeatPos() {
		playerPos.position = headset.transform.position + new Vector3(0, yPos, 0);
		playerPos.rotation = Quaternion.Euler(new Vector3(0, headset.transform.rotation.eulerAngles.y, 0));
	}

	// Checks for a trigger press and calls the function to move the position of the user
	private void Trigger(object sender, ClickedEventArgs e) {
		SeatPos();
	}
}
