using UnityEngine;
using UnityEngine.UI;

public class ChangeSliderValue : MonoBehaviour {

	private Slider slider;

	void Start() {
		slider = GetComponent<Slider>();
	}

	public void SliderValueMinus() {
		slider.value = slider.value - 1;
	}

	public void SliderValuePlus() {
		slider.value = slider.value + 1;
	}
}
