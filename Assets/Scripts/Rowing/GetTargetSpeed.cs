using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetTargetSpeed : MonoBehaviour {

    public Slider targetSpeed; //target speed from slider
    public float SliderValue; //value from slider as float

    public float TargetSpeed(Slider ts)
    {
        SliderValue = ts.value;
        return SliderValue;
    }
}
