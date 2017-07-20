using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetTargetSpeed : MonoBehaviour {
    
    public float SliderValue; //value from slider as float

    public void TargetSpeed()
    {
        SliderValue = GameObject.Find("TargetSpeedSlider").GetComponent<Slider>().value;
        Debug.Log(SliderValue);
    }
}
