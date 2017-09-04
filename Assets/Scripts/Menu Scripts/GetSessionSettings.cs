using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetSessionSettings : MonoBehaviour {

	private InputField raceLength;
	private Slider minSpeed;
	private Slider maxSpeed;
	private SceneData sceneData;
	private Text minValue;
	private Text maxValue;
	

	public void SettingsValues() {
		raceLength = GameObject.Find("RaceLengthInputField").GetComponent<InputField>();
		minSpeed = GameObject.Find("MinSpeedSlider").GetComponent<Slider>();
		maxSpeed = GameObject.Find("MaxSpeedSlider").GetComponent<Slider>();
		sceneData = GameObject.Find("SceneDataManager").GetComponent<SceneData>();
		if (sceneData.length != 0) {
			raceLength.text = sceneData.length.ToString();
			minSpeed.value = (float) sceneData.minSpeed;
			maxSpeed.value = (float) sceneData.maxSpeed;
		}
	}

	public void MinSlider() {
		minSpeed = GameObject.Find("MinSpeedSlider").GetComponent<Slider>();
		minValue = GameObject.Find("MinSpeedValue").GetComponent<Text>();
		minValue.text = "Minimum Speed: " + minSpeed.value.ToString() + " m/s";
		if(minSpeed.value >= maxSpeed.value) {
			maxSpeed.value = minSpeed.value + 1;
			MaxSlider();
		}
	}
	
	public void MaxSlider() {
		maxSpeed = GameObject.Find("MaxSpeedSlider").GetComponent<Slider>();
		maxValue = GameObject.Find("MaxSpeedValue").GetComponent<Text>();
		maxValue.text = "Maximum Speed: " + maxSpeed.value.ToString() + " m/s";
	}

	public void UpdateSettings() {
		raceLength = GameObject.Find("RaceLengthInputField").GetComponent<InputField>();
		minSpeed = GameObject.Find("MinSpeedSlider").GetComponent<Slider>();
		maxSpeed = GameObject.Find("MaxSpeedSlider").GetComponent<Slider>();

		sceneData = GameObject.Find("SceneDataManager").GetComponent<SceneData>();
		sceneData.length = double.Parse(raceLength.text);
		sceneData.minSpeed = minSpeed.value;
		sceneData.maxSpeed = maxSpeed.value;
	}
}
