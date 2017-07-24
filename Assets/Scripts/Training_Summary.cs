/*  Author: Benjamin Ferguson
    Date: 11/05/17
    Purpose: To track the training data and display the training summary
             after completion of training.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Training_Summary : MonoBehaviour {
    private List<double> power; //list of power data of user's whole training session
    private List<double> strokes_pm; //list of strokes per mintue data of the user's whole training session
    private List<double> speed; //list of speed data of the user's whole training session
	public double avgPower; 
	public double avgStrokes_pm; 
	public double avgSpeed;
	public double time; //time of user's training session
    public double[] split; //split times of the user's training session
    public double splitTime; //current split time

	public static double distance; //distance the user has travelled
	private const int length = 2000; //length of the training session

    private float datarate; //rate to refresh video playback speed
    private float deltatime; //time since last refresh

    public GameObject Summary;
	private Text TimeText;
	private Text AverageSplitsText;
	private Text AverageStrokesText;
	private Text AverageSpeedText;
	private Text AveragePowerText;

	// Use this for initialization
	void Start () {
        power = new List<double>();
        strokes_pm = new List<double>();
        speed = new List<double>();
        time = 0;
        split = new double[4];
        splitTime = 0;
		//used to only get data every 1s
        datarate = 1f;
        deltatime = 0.0f;

		//text objects for displaying the values to the user
		TimeText = GameObject.Find("TimeText").GetComponent<Text>();
		AverageSplitsText = GameObject.Find("AverageSplitsText").GetComponent<Text>();
		AverageStrokesText = GameObject.Find("AverageStrokesText").GetComponent<Text>();
		AverageSpeedText = GameObject.Find("AverageSpeedText").GetComponent<Text>();
		AveragePowerText = GameObject.Find("AveragePowerText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        time = time + Time.deltaTime;
        checkSplit();
        deltatime += Time.deltaTime;
        // Debug.Log("deltatime:" + deltatime);
        if (deltatime >= datarate)
        {
            updateSummary();
            deltatime = 0.0f;
        }
        
	}

	// Used to check if the user has completed a split
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
		//power.Add (//power from erg)
		//strokes_pm.Add (//strokes_pm from erg)
		//speed.Add (//speed from erg)
	}

	public double GetAverage(List<double> list) 
	{
		double avg = 0;
		int count = 0;
		foreach (double j in list) 
		{
			avg += avg;
			count++;
		}
		avg = avg / count;
		return avg;
	}

	// Used to calculate and display the training summary
	public void displaySummary() {
		avgPower = GetAverage(power);
		avgStrokes_pm = GetAverage(strokes_pm);
		avgSpeed = GetAverage(speed);
		double avgSplits = 0;
		int count = 0;
		foreach (double j in split) {
			avgSplits += avgSplits;
			count++;
		}
		avgSplits = avgSplits / count;
		TimeSpan ts = TimeSpan.FromSeconds(time);
		TimeSpan tsSplit = TimeSpan.FromSeconds(avgSplits);
		TimeText.text = "Time: " + ts.ToString();
		AverageSplitsText.text = "Average Splits: " + tsSplit.ToString();
		AverageStrokesText.text = "AverageStrokes: " + avgStrokes_pm;
		AverageSpeedText.text = "AverageSpeed: " + avgSpeed;
		AveragePowerText.text = "AveragePower: " + avgPower;

		Summary.SetActive(true);
	}
}
