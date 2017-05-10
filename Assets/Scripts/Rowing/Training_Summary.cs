/*  Author: Benjamin Ferguson
    Date: 11/05/17
    Purpose: To track the training data and display the training summary
             after completion of training.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Training_Summary : MonoBehaviour {
    public static bool end_training; //signals the end of training
    public static double power; //power of user's training session
    public static double time; //time of user's training session
    public static double pace; //pace of the user's training session
    public static double distance; //distance the user has travelled

	// Use this for initialization
	void Start () {
        end_training = false;
        power = 0;
        time = 0;
        pace = 0;
	}
	
	// Update is called once per frame
	void Update () {
        distance = Rowing_Speed.distance;
        //calculate training data

        if (distance >= 2000) {
            end_training = true;
        }
		if(end_training == true) {
            //display summary
        }
	}
}
