/*  Author: Grant Burgess
    Date: 09/09/17
    Purpose: To log the training summary from a training session to a text file
*/

using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

public class Data_Logging : MonoBehaviour {

	private SceneData sceneData; // SceneData has the directory path
	public string path; // Complete file path

	// Use this for initialization
	void Start () {
		sceneData = GameObject.Find("SceneDataManager").GetComponent<SceneData>();
		path = sceneData.userPath + "\\" + Filename_Creator();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Creates the file name in the format yyyyMMddhhmm.txt
	private static string Filename_Creator() {
		DateTime date = DateTime.Now;
		int year = date.Year;
		int month = date.Month;
		int day = date.Day;
		int hour = date.Hour;
		int min = date.Minute;
		string filename = year.ToString();
		if (month < 10) {
			filename = filename + ("0" + month.ToString());
		} else {
			filename = filename + (month.ToString());
		}
		if (day < 10) {
			filename = filename + ("0" + day.ToString());
		} else {
			filename = filename + (day.ToString());
		}
		if (hour < 10) {
			filename = filename + ("0" + hour.ToString());
		} else {
			filename = filename + (hour.ToString());
		}
		if (min < 10) {
			filename = filename + ("0" + min.ToString());
		} else {
			filename = filename + (min.ToString());
		}
		filename = filename + (".txt");
		return filename;
	}

	// Writes the training summary to a text file
	public void Create_Summary(double distance, string time, string avgSplits, 
		double avgStrokes, double avgSpeed, double avgPower) {
		TextWriter writer = new StreamWriter(path);
		writer.WriteLine("Completed on: " + DateTime.Now);
		writer.WriteLine("Distance: " + distance);
		writer.WriteLine("Time: " + time);
		writer.WriteLine("Average Splits: " + avgSplits);
		writer.WriteLine("Average Strokes: " + avgStrokes.ToString("N2"));
		writer.WriteLine("Average Speed: " + avgSpeed.ToString("N2"));
		writer.WriteLine("Average Power: " + avgPower.ToString("N2"));
		writer.Close();
		return;
	}
}
