/*  Author: Benjamin Ferguson
    Date: 07/09/17
    Purpose: To store the data set by the user for their custom session 
	so it can be used when changing	between scenes. To use that stored 
	data when loading the settings each time they come back to the menu.
*/
using UnityEngine;
using UnityEngine.UI;

public class GetSessionSettings : MonoBehaviour {

	private Slider raceLength; // Used to get the user input for the length of the race
	private Slider minSpeed; // Used to get the value set by the user for the minimum speed indicator
	private Slider maxSpeed; // Used to get the value set by the user for the maximum speed indicator
	private SceneData sceneData; //Used to store the data for transfer between scenes
	private Text lengthValue;
	private Text minValue; // Used to display to the chosen minimum speed value to the user
	private Text maxValue; // Used to display to the chosen maximum speed value to the user
	private Slider targetSlider; // Used to get the value set by the user for the target speed
	private Text valueText; // Used to display to the chosen target speed value to the user

	// Used to set the values of the settings to the previously set values by the user
	public void SettingsValues() {
		// Get the required objects that need to be updated
		raceLength = GameObject.Find("RaceLengthSlider").GetComponent<Slider>();
		minSpeed = GameObject.Find("MinSpeedSlider").GetComponent<Slider>();
		maxSpeed = GameObject.Find("MaxSpeedSlider").GetComponent<Slider>();
		targetSlider = GameObject.Find("TargetSpeedSlider").GetComponent<Slider>();
		// Used to get the data stored from previous user selections
		sceneData = GameObject.Find("SceneDataManager").GetComponent<SceneData>();
		// Used to check if the user has set the data prior
		// If the user has, it updates the value of the objects 
		if (sceneData.length != 0) {
			raceLength.value = (float) sceneData.length / 100;
			minSpeed.value = (float) sceneData.minSpeed;
			maxSpeed.value = (float) sceneData.maxSpeed;
			targetSlider.value = sceneData.targetSpeed;
		}
	}

	// Used to change the value of the race length text when the user moves the slider
	public void RaceLengthSlider() {
		raceLength = GameObject.Find("RaceLengthSlider").GetComponent<Slider>();
		lengthValue = GameObject.Find("RaceLengthValue").GetComponent<Text>();
		lengthValue.text = "Race Length: " + (raceLength.value * 100).ToString() + " m";
	}

	// Used to change the value of the minimum speed text when the user moves the slider
	public void MinSlider() {
		minSpeed = GameObject.Find("MinSpeedSlider").GetComponent<Slider>();
		minValue = GameObject.Find("MinSpeedValue").GetComponent<Text>();
		minValue.text = "Minimum Speed: " + minSpeed.value.ToString() + " m/s";
		// Checks if the minimum value is greater than or equal to the maximum value
		// If it is the maximum slider increases so it will always be greater
		if(minSpeed.value >= maxSpeed.value) {
			maxSpeed.value = minSpeed.value + 1;
			MaxSlider();
		}
	}

	// Used to change the value of the minimum speed text when the user moves the slider
	public void MaxSlider() {
		maxSpeed = GameObject.Find("MaxSpeedSlider").GetComponent<Slider>();
		maxValue = GameObject.Find("MaxSpeedValue").GetComponent<Text>();
		maxValue.text = "Maximum Speed: " + maxSpeed.value.ToString() + " m/s";
		// Checks if the maximum value is less than or equal to the minimum value
		// If it is the minimum slider decreases so it will always be lower
		if (maxSpeed.value <= minSpeed.value) {
			minSpeed.value = maxSpeed.value - 1;
			MinSlider();
		}
	}
	// Used to change the value of the target speed text when the user moves the slider
	public void TargetSpeed() {
		targetSlider = GameObject.Find("TargetSpeedSlider").GetComponent<Slider>();
		valueText = GameObject.Find("TargetSpeedValue").GetComponent<Text>();
		valueText.text = "Target Speed: " + targetSlider.value.ToString() + " m/s";
	}

	// Used to update the settings data stored to be used when transfering between scenes
	public void UpdateSettings() {
		raceLength = GameObject.Find("RaceLengthSlider").GetComponent<Slider>();
		minSpeed = GameObject.Find("MinSpeedSlider").GetComponent<Slider>();
		maxSpeed = GameObject.Find("MaxSpeedSlider").GetComponent<Slider>();
		targetSlider = GameObject.Find("TargetSpeedSlider").GetComponent<Slider>();
		sceneData = GameObject.Find("SceneDataManager").GetComponent<SceneData>();
		// Copies the values of the sliders to their appropriate variable for use between scenes
		sceneData.length = raceLength.value * 100;
		sceneData.minSpeed = minSpeed.value;
		sceneData.maxSpeed = maxSpeed.value;
		sceneData.targetSpeed = targetSlider.value;
	}

	public void SetFreeSession() {
		sceneData = GameObject.Find("SceneDataManager").GetComponent<SceneData>();
		sceneData.freeSession = true;
	}

	public void SetCustomSession() {
		sceneData = GameObject.Find("SceneDataManager").GetComponent<SceneData>();
		sceneData.freeSession = false;
	}
}
