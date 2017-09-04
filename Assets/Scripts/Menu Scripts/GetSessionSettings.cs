using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetSessionSettings : MonoBehaviour {

	private InputField raceLength;
	private InputField minSpeed;
	private InputField maxSpeed;
	private SceneData sceneData;

	public void SettingsValues() {
		raceLength = GameObject.Find("RaceLengthInputField").GetComponent<InputField>();
		minSpeed = GameObject.Find("MinSpeedInputField").GetComponent<InputField>();
		maxSpeed = GameObject.Find("MaxSpeedInputField").GetComponent<InputField>();
		sceneData = GameObject.Find("SceneDataManager").GetComponent<SceneData>();
		if (sceneData.length != 0) {
			raceLength.text = sceneData.length.ToString();
			minSpeed.text = sceneData.minSpeed.ToString();
			maxSpeed.text = sceneData.maxSpeed.ToString();
		}
	}

	public void UpdateSettings() {
		raceLength = GameObject.Find("RaceLengthInputField").GetComponent<InputField>();
		minSpeed = GameObject.Find("MinSpeedInputField").GetComponent<InputField>();
		maxSpeed = GameObject.Find("MaxSpeedInputField").GetComponent<InputField>();

		sceneData = GameObject.Find("SceneDataManager").GetComponent<SceneData>();
		sceneData.length = double.Parse(raceLength.text);
		sceneData.minSpeed = double.Parse(minSpeed.text);
		sceneData.maxSpeed = double.Parse(maxSpeed.text);
	}
}
