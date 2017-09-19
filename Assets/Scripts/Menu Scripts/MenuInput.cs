/*  Author: Benjamin Ferguson
    Date: 07/09/17
    Purpose: To allow the user to use gaze selection on buttons in the menu
*/
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VRStandardAssets.Utils;

public class MenuInput : MonoBehaviour {

	public EventSystem eventSystem; // Used so we can handle the events of the buttons 
	private Button selectedButton; // The current button the user is selecting
	private bool buttonSelected; // Used to determine if a button is selected or not
	private bool startTimer; // Used to determine when the timer has started and when to stop the timer
	private bool timeElapsed; // Used to determine if the timer has expired 
	private float startTime; // The time when the user first looks at a button
	private AudioSource audioSource; // Used to play the menuClick sound
	public AudioClip menuClick; // Played when the user selects a button
	private VRInteractiveItem interactiveItem; // Allows the user to interact with the buttons
	public VREyeRaycaster eyeRaycaster; // Allows the user to interact with the buttons
	private float selectionTime; // The time it takes for the user to select a button

	// Used to initialize some variables
	private void Start() {
		startTimer = false;
		timeElapsed = false;
		selectionTime = 1f;
		audioSource = GameObject.Find("Audio").GetComponent<AudioSource>();
	}

	// Checks every frame if the timer on a button selection has expired
	private void Update() {
		// Checks if the timer has started 
		if (startTimer) {
			timeElapsed = false;
			// Checks if the timer has expired
			if (Time.time - startTime > selectionTime) {
				timeElapsed = true;
			}
			// If the timer expires the button is then clicked
			if (timeElapsed) {
				audioSource.PlayOneShot(menuClick);
				selectedButton.onClick.Invoke();
                // If the selected button is a slider button start the timer again allowing the user to press multiple times without having to look away
				if(selectedButton.tag == "SliderButton") {
					startTime = Time.time;
				} else {
					startTimer = false;
				}
			}
		}
	}

	// Called when the user looks at a button
	public void EnterGaze() {
		// Sets the object the user is looking at
		interactiveItem = eyeRaycaster.CurrentInteractible;
		// Checks if a button is not selected yet and the object the user is looking at is a button
		if (!buttonSelected && (interactiveItem.tag == "Button" || interactiveItem.tag == "SliderButton")) {
			buttonSelected = true;
			selectedButton = interactiveItem.GetComponent<Button>();
			selectedButton.Select();
			startTimer = true;
			startTime = Time.time;
		}
	}

	// Called when the user stops looking at a button
	public void ExitGaze() {
		// Checks if a button is currently selected
		if (buttonSelected) {
			eventSystem.SetSelectedGameObject(null);
			buttonSelected = false;
			startTimer = false;
		}
	}
    // Sets the button selected to false when disabled
	public void OnDisable() {
		buttonSelected = false;
	}
}
