/*  Author: Benjamin Ferguson
    Date: 11/05/17
    Purpose: To track the training data and display the training summary
             after completion of training.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Training_Summary : MonoBehaviour {
    public static double power; //power of user's training session
    public static double time; //time of user's training session
    public static double split; //average split time of the user's training session
	public static double strokes_pm; //average strokes per mintue of the user's training session

	public static double distance; //distance the user has travelled
	public const int length = 2000; //length of the training session
	

	// Use this for initialization
	void Start () {
        power = 0;
        time = 0;
        split = 0;
	}
	
	// Update is called once per frame
	void Update () {
		calcSummary();
		checkFinish();
	}

	// Used to check if the user has completed the training session
	public static void checkFinish() {
		distance = Rowing_Speed.distance;
		if (distance >= length) {
			displaySummary();
		}
	}

	// Used to calculate the data needed for the training summary
	public static void calcSummary() {

	}

	// Used to display the training summary
	public static void displaySummary() {

	}
}
