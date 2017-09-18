/*  Author: Benjamin Ferguson, Grant Burgess
    Date: 05/09/17
    Purpose: To track the training data during the session and display the training summary
             after completion of training.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Training_Summary : MonoBehaviour {
    public List<double> power; // List of power data of user's whole training session
	public List<double> strokesPerMin; // List of strokes per mintue data of the user's whole training session
	public List<double> speed; // List of speed data of the user's whole training session
	public double avgPower;  // The avg power calculated at the end of the session from the data that was collected
	public double avgStrokesPerMin; // The avg stokes per minute calculated at the end of the session from the data that was collected
	public double avgSpeed; // The avg speed calculated at the end of the session from the data that was collected
	public double time; // Total time of user's training session
    public double[] split; // Split times of the user's training session
    public double splitTime; // Current split time

	public double distance; // Total distance the user has travelled
    private double splitDistance; // The length of a split
    private double length; // Total length of the training session set by the user
	private int count = 1; // Used to track the splits
	public bool finished = false; // Used to determine when the user has finished the session
	private float datarate; // Rate that the data is to be collected
    private float deltatime; // Time since the last set of data was collected

	private Video_Playback videoPlayback; // Used to find out when the user has started
	private SceneData sceneData; // Used to get the length of the session the user has set
	private PM5_Communication pm_com; // Used to get data from erg
	public GameObject Summary; // Used to display the training summary
	public Data_Logging datalog; // Used to log the summary to file

	// These are text objects used for displaying the session data on the training summary 
	public Text TimeText; 
	public Text AverageSplitsText;
	public Text AverageStrokesText;
	public Text AverageSpeedText;
	public Text AveragePowerText;

	// Used for initialisation
	void Start () {
		pm_com = GetComponent<PM5_Communication>();
		videoPlayback = GetComponent<Video_Playback>();
		datalog = GetComponent<Data_Logging>();
		sceneData = GameObject.Find("SceneDataManager").GetComponent<SceneData>();
		length = sceneData.length;
		power = new List<double>();
        strokesPerMin = new List<double>();
        speed = new List<double>();
        time = 0;
        split = new double[4];
        splitTime = 0;
        splitDistance = (length / 4);
        // Used to only get data every 1 second
        datarate = 1f;
        deltatime = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        // Only update once the user has started and stop when the user has finished
		if (videoPlayback.playerStarted && !finished) {
			time = time + Time.deltaTime;
            // Used to check for splits and the end of the session
			CheckSplit();
			deltatime += Time.deltaTime;
            // Used to update the training data every 1 second
			if (deltatime >= datarate) {
				UpdateSummary();
				deltatime = 0.0f;
			}
		}
        
	}

	// Used to check if the user has completed a split
	public void CheckSplit() {
		// Get the current distance travelled from the ERG
		distance = pm_com.current_Distance;
        // Update the time taken for this split
		splitTime = splitTime + Time.deltaTime;
        // Used to check for the first 3 splits and only update the split value once
        if (distance > splitDistance*count && split[count - 1] == 0 && count < 4)
        {
			split[count - 1] = splitTime;
			splitTime = 0;
			count++;
		}  
        //check if the end has been reached
		if (distance > length && !finished) 
        {
			finished = true;
            // Used to tell the video playback script the user has finished
            videoPlayback.finished = true;
			split[count-1] = splitTime;
            // Used to display the training summary
            DisplaySummary();
		}
	}

	// Used to update the data needed for the training summary
	public void UpdateSummary() {
        // Update the lists wih the appropriate data from the ERG
		power.Add(pm_com.current_Power);
		strokesPerMin.Add(pm_com.current_Cadence);
		speed.Add(pm_com.current_Speed);
	}

    // Used to get the average from the lists of data collected
	public double GetAverage(List<double> list) 
	{
		double avg = 0;
		int count = 0;
        // Iterate through the entire list
		foreach (double j in list) 
		{
			avg += j;
			count++;
		}
		avg = avg / count;
		return avg;
	}

	// Used to calculate and display the training summary
	public void DisplaySummary() {
        // Get the averages of the lists of data
		avgPower = GetAverage(power);
		avgStrokesPerMin = GetAverage(strokesPerMin);
		avgSpeed = GetAverage(speed);
        // Get the average of the split
		double avgSplits = 0;
		int count = 0;
		foreach (double j in split) {
			avgSplits += j;
			count++;
		}
		avgSplits = avgSplits / count;
        // Convert total time and average split time into timespan objects
		TimeSpan ts = TimeSpan.FromSeconds(time);
		TimeSpan tsSplit = TimeSpan.FromSeconds(avgSplits);
        // Update the text for all the data needed for the training summary and display in the right format
		TimeText.text =           "Time:              " + ts.Minutes + ":" + ts.Seconds + "." + ts.Milliseconds;
		AverageSplitsText.text =  "Average Splits:  " + tsSplit.Minutes + ":" + tsSplit.Seconds + "." + tsSplit.Milliseconds;
		AverageStrokesText.text = "AverageStrokes: " + Math.Round(avgStrokesPerMin, 2).ToString("F");
		AverageSpeedText.text =   "AverageSpeed:   " + Math.Round(avgSpeed, 2).ToString("F");
		AveragePowerText.text =   "AveragePower:   " + Math.Round(avgPower, 2).ToString("F");
        // Disable the speed display
		videoPlayback.SpeedDisplay.enabled = false;
        // Display the training summary 
		Summary.SetActive(true);
        // Addition by Grant Burgess
        // Used to log the training summary to a text file
		string timeString = (ts.Minutes + ":" + ts.Seconds + "." + ts.Milliseconds);
		string splitsString = (tsSplit.Minutes + ":" + tsSplit.Seconds + "." + tsSplit.Milliseconds);
		datalog.Create_Summary(distance, timeString, splitsString, avgStrokesPerMin, avgSpeed, avgPower);
	}
}
