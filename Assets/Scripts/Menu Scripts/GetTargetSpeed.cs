using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetTargetSpeed : MonoBehaviour {
    
    public float SliderValue; //value from slider as float
    public Text ValueText;

    public void TargetSpeed()
    {
        SliderValue = GameObject.Find("TargetSpeedSlider").GetComponent<Slider>().value;
        //Debug.Log(SliderValue);
        ValueText.text = "Target Speed: " + SliderValue.ToString();
    }
}
