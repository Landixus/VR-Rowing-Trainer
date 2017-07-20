/*  Author: Benjamin Ferguson
    Date: 11/05/17
    Purpose: To track the training data and display the training summary
             after completion of training.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Training_Summary : MonoBehaviour {
    public List<double> power; //power of user's training session
    public List<double> strokes_pm; //strokes per mintue of the user's training session
    public List<double> speed; //strokes per mintue of the user's training session
    public double time; //time of user's training session
    public double[] split; //split times of the user's training session
    public double splitTime; //current split time

	public static double distance; //distance the user has travelled
	private const int length = 2000; //length of the training session

    private float framerate; //rate to refresh video playback speed
    private float deltatime; //time since last refresh

    public GameObject Summary;

    // Use this for initialization
    void Start () {
        power = new List<double>();
        strokes_pm = new List<double>();
        speed = new List<double>();
        time = 0;
        split = new double[4];
        splitTime = 0;
        framerate = 1f;
        deltatime = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {
        time = time + Time.deltaTime;
        checkSplit();
        deltatime += Time.deltaTime;
        // Debug.Log("deltatime:" + deltatime);
        if (deltatime >= framerate)
        {
            updateSummary();
            deltatime = 0.0f;
        }
        
	}

	// Used to check if the user has completed the training session
	public void checkSplit() {
        int count = 0;
		distance = Rowing_Speed.distance;
        splitTime = splitTime + Time.deltaTime;
        if (distance > 500 && count < 3)
        {
            split[count] = splitTime;
            splitTime = 0;
            distance -= 500;
        }
        //check if the end has been reached
		else if (distance > 500 && count == 3) 
        {
            split[count] = splitTime;
            displaySummary();
		}
	}

	// Used to update the data needed for the training summary
	public void updateSummary() {

	}

	// Used to calculate and display the training summary
	public void displaySummary() {
        Summary.SetActive = true;
    }
}
