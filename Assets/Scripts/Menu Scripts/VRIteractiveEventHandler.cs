/*  Author: Benjamin Ferguson
    Date: 18/09/17
    Purpose: To handle the gaze events so that the menu input script can perform
    the appropriate function when necessary.
*/

using UnityEngine;
using VRStandardAssets.Utils;
using UnityEngine.Events;

// Requires the object the user is looking at to have an interactive item attached
[RequireComponent(typeof(VRInteractiveItem))]
public class VRIteractiveEventHandler : MonoBehaviour {

	private VRInteractiveItem interactiveItem; // The object that the user is looking at
	public MenuInput menuInput; // Menu input script object 

	// Use this for initialization
	private void Start () {
        // Gets the interactive item of the object the user is looking at
		interactiveItem = GetComponent<VRInteractiveItem>();
        // Adds the function below to the existing function in the interactive item script
		interactiveItem.OnOver += OnGazeEnter;
        // Adds the function below to the existing function in the interactive item script
        interactiveItem.OnOut += OnGazeExit;
	}
	
    // Calls the function inside the menu input script when the user looks at a button
	private void OnGazeEnter() {
		menuInput.EnterGaze();
	}

    // Calls the function inside the menu input script when the user looks away from a button
    private void OnGazeExit() {
		menuInput.ExitGaze();
	}

}
