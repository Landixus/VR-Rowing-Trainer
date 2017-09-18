/*  Author: Benjamin Ferguson
    Date: 18/09/17
    Purpose: To change the value of the appropriate settings slider when the user clicks the plus or minus buttons.
*/
using UnityEngine;
using UnityEngine.UI;

public class ChangeSliderValue : MonoBehaviour {

	private Slider slider; // The slider object which is associated with the button pressed

    // Gets the slider object so the value can be changed
	void Start() {
		slider = GetComponent<Slider>();
	}

    // Used to decrement the appropriate slider value by 1 when the user clicks or looks at the minus button
    public void SliderValueMinus() {
		slider.value = slider.value - 1;
	}

    // Used to increment the appropriate slider value by 1 when the user clicks or looks at the plus button
    public void SliderValuePlus() {
		slider.value = slider.value + 1;
	}
}
