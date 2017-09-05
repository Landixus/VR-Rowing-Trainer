/*  Author: Benjamin Ferguson
    Date: 05/09/17
    Purpose: To get the speed from the slider and to set the slider to a previous
	value if there is one and also update the game object that is uses the data in all scenes.
*/

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
