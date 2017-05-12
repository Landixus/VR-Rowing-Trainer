/*  Author: Benjamin Ferguson
    Date: 10/05/17
    Purpose: To match the speed of the 360 video playback with the rowing speed.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment_Speed : MonoBehaviour {
    public static double boat_speed; //speed of the boat
    public const double video_speed = 4; //speed the video was recorded at in knots
    public const double convert = 0.514444; //metres per second for 1 knot
    public static double normalise_multiplier; //video playback speed at 1m/s
    public static double enviro_speed; //the multipler to the video playback to match the boat speed

	// Use this for initialization
	void Start () {
        normalise_multiplier = 1 / (video_speed * convert);
        Debug.Log("normaliser:" + normalise_multiplier);
        refreshEnviroSpeed();
    }
	
	// Update is called once per frame
	void Update () {
        refreshEnviroSpeed();
        Debug.Log("boat speed:" + boat_speed);
        Debug.Log("enviro speed:" + enviro_speed);
    }

	// Used to update the speed of the environment
    public static void refreshEnviroSpeed() {
        boat_speed = Rowing_Speed.speed;
        enviro_speed = boat_speed * normalise_multiplier;
    }
}