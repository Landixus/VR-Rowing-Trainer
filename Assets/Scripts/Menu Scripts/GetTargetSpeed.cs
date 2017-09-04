using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetTargetSpeed : MonoBehaviour {

	private Slider slider;
	public float targetSpeed; //value from slider as float
    private Text valueText;
	private SceneData sceneData;

	public void SliderValue() {
		slider = GameObject.Find("TargetSpeedSlider").GetComponent<Slider>();
		valueText = GameObject.Find("TargetSpeedValue").GetComponent<Text>();
		sceneData = GameObject.Find("SceneDataManager").GetComponent<SceneData>();
		if (sceneData.targetSpeed != 0) {
			slider.value = sceneData.targetSpeed;
			valueText.text = "Target Speed: " + sceneData.targetSpeed.ToString() + " m/s";
		}
	}

	public void TargetSpeed()
    {
		slider = GameObject.Find("TargetSpeedSlider").GetComponent<Slider>();
		valueText = GameObject.Find("TargetSpeedValue").GetComponent<Text>();
		targetSpeed = slider.value;
		valueText.text = "Target Speed: " + targetSpeed.ToString() + " m/s";
    }

	public void UpdateSpeedData() {
		sceneData = GameObject.Find("SceneDataManager").GetComponent<SceneData>();
		sceneData.targetSpeed = targetSpeed;
	}
}
