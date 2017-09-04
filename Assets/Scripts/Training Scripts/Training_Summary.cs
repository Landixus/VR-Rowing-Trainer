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
    public List<double> power; //list of power data of user's whole training session
	public List<double> strokes_pm; //list of strokes per mintue data of the user's whole training session
	public List<double> speed; //list of speed data of the user's whole training session
	public double avgPower; 
	public double avgStrokes_pm; 
	public double avgSpeed;
	public double time; //time of user's training session
    public double[] split; //split times of the user's training session
    public double splitTime; //current split time

	public double distance; //distance the user has travelled
	private double length; //length of the training session
	private int count = 1;
	private bool finished = false;
	private float datarate; //rate to refresh video playback speed
    private float deltatime; //time since last refresh

	private Video_Playback vb;
	private SceneData sceneData;

	private PM5_Communication pm_com; //used to get data from erg
	public GameObject Summary;
	public Text TimeText;
	public Text AverageSplitsText;
	public Text AverageStrokesText;
	public Text AverageSpeedText;
	public Text AveragePowerText;

	// Use this for initialization
	void Start () {
		pm_com = GetComponent<PM5_Communication>();
		vb = GetComponent<Video_Playback>();
		sceneData = GameObject.Find("SceneDataManager").GetComponent<SceneData>();
		length = sceneData.length;
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
		//AverageSplitsText = GameObject.Find("AverageSplitsText").GetComponent<Text>();
		//AverageStrokesText = GameObject.Find("AverageStrokesText").GetComponent<Text>();
		//AverageSpeedText = GameObject.Find("AverageSpeedText").GetComponent<Text>();
		//AveragePowerText = GameObject.Find("AveragePowerText").GetComponent<Text>();
		//TimeText = GameObject.Find("TimeText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if (vb.playerstarted && !finished) {
			time = time + Time.deltaTime;
			CheckSplit();
			deltatime += Time.deltaTime;
			// Debug.Log("deltatime:" + deltatime);
			if (deltatime >= datarate) {
				UpdateSummary();
				deltatime = 0.0f;
			}
		}
        
	}

	// Used to check if the user has completed a split
	public void CheckSplit() {
		
		distance = pm_com.current_Distance;
		double splitDistance = (length / 4);
		
		splitTime = splitTime + Time.deltaTime;
        if (distance > splitDistance*count && split[count - 1] == 0 && count < 4)
        {
			//Debug.Log("Adding Split" + distance);
			split[count - 1] = splitTime;
			splitTime = 0;
			count++;
			//Debug.Log("count = " + count);
			//Debug.Log("SplitDistance: " + splitDistance * count);
		}
			   
        //check if the end has been reached
		if (distance > length && !finished) 
        {
			finished = true;
			//Debug.Log("Adding last Split");
			split[count-1] = splitTime;
            DisplaySummary();
		}
	}

	// Used to update the data needed for the training summary
	public void UpdateSummary() {
		//Debug.Log("Updating summary");
		power.Add(pm_com.current_Power);
		strokes_pm.Add(pm_com.current_Cadence);
		speed.Add(pm_com.current_Speed);
	}

	public double GetAverage(List<double> list) 
	{
		Debug.Log("Averaging:" + list);
		double avg = 0;
		int count = 0;
		foreach (double j in list) 
		{
			avg += j;
			count++;
		}
		avg = avg / count;
		//Debug.Log(avg);
		return avg;
	}

	// Used to calculate and display the training summary
	public void DisplaySummary() {
		//Debug.Log("Displaying summary");
		avgPower = GetAverage(power);
		avgStrokes_pm = GetAverage(strokes_pm);
		avgSpeed = GetAverage(speed);
		double avgSplits = 0;
		int count = 0;
		foreach (double j in split) {
			avgSplits += j;
			count++;
		}
		avgSplits = avgSplits / count;
		TimeSpan ts = TimeSpan.FromSeconds(time);
		TimeSpan tsSplit = TimeSpan.FromSeconds(avgSplits);
		TimeText.text =           "Time:              " + ts.Minutes + ":" + ts.Seconds + "." + ts.Milliseconds;
		AverageSplitsText.text =  "Average Splits:  " + tsSplit.Minutes + ":" + tsSplit.Seconds + "." + tsSplit.Milliseconds;
		AverageStrokesText.text = "AverageStrokes: " + Math.Round(avgStrokes_pm, 2);
		AverageSpeedText.text =   "AverageSpeed:   " + Math.Round(avgSpeed, 2);
		AveragePowerText.text =   "AveragePower:   " + Math.Round(avgPower, 2);
		vb.SpeedDisplay.enabled = false;
		Summary.SetActive(true);
	}
}
