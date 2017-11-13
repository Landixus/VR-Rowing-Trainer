using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System;

/**
 * Data collector, written by X.Hunt
 * 
 * Collects all the data from the person on the rower, and saves it to file
 * 
 */ 

public class DataCollector : MonoBehaviour {

	[Header("Collection Rates. These are in Hz")]
	public int powerCollectionRate = 100;
	public int dataCollectionRate = 4;

	[HideInInspector]
	public List<TrackingData> trackingData = new List<TrackingData>();
	[HideInInspector]
	public List<PowerData> powerData = new List<PowerData>();

	[Header("Reference Objects")]
	public Transform trackedObject;
	public PM5_Communication pm_com;

	[Header("Misc")]
	public bool hasStarted = false;

	private DateTime startTime;
	private float currentTime = 0;

	// Use this for initialization
	void Start () {
		
		if(!trackedObject || ! pm_com) {
			Debug.LogWarning("No tracked object or PM5!");
			this.enabled = false;
			return;
		}

		startTime = DateTime.Now;

		// Invoke ALL the things!

		PM5_Communication.initTime();

		float powerRate = 1f / powerCollectionRate;
		float dataRate = 1f / dataCollectionRate;

		InvokeRepeating("collectPowerData", 0, powerRate);
		InvokeRepeating("collectAllData", 0, dataRate);

		//collectPowerData();
		//collectAllData();

		Invoke("saveData", 10);
		//saveData();

	}

	// Collects just the power data
	public void collectPowerData() {

		double time;
		double distance;
		double cadence;
		double speed;
		double power;

		PM5_Communication.getRawValues(out time, out distance, out cadence, out speed, out power);

		PowerData data = new PowerData();

		data.power = power;
		data.timestamp = time;

	//	data.power = pm_com.current_Power;

	//	data.timestamp = (DateTime.Now - startTime).TotalMilliseconds;


		powerData.Add(data);

	}

	public void collectAllData() {

		TrackingData data = new TrackingData();

		data.timestamp = (DateTime.Now - startTime).TotalMilliseconds;

		data.pos = trackedObject.localPosition;
		data.worldPos = trackedObject.position;
		data.rot = trackedObject.rotation.eulerAngles;

		data.power = pm_com.current_Power;
		data.distance = pm_com.current_Distance;

		data.stroke = 0;
		data.looks = 0;

		trackingData.Add(data);
	}

	public void saveData() {

		string sessionID = DateTime.Now.ToString("yyyyMMddHHmmss");
		string path = Application.dataPath + "/TrainingLogs/" + sessionID + "/";

		if (!Directory.Exists(path)) {
			Debug.Log("Creating directory: " + path);
			Directory.CreateDirectory(path);
		}

		// 100Hz data
		using(StreamWriter sw = new StreamWriter(path + "power.csv")) {
			Debug.Log("Saving data to: " + ((FileStream)(sw.BaseStream)).Name);

			// Header
			sw.WriteLine("Timestamp, Power");

			// Contents
			for(int i = 0; i < powerData.Count; i++) {

				PowerData data = powerData[i];
				string line = string.Format("{0}, {1}", data.timestamp, data.power);
				sw.WriteLine(line);
			}

		}

		// 4Hz data
		using (StreamWriter sw = new StreamWriter(path + "session.csv")) {

			Debug.Log("Saving data to: " + ((FileStream)(sw.BaseStream)).Name);

			// Header
			sw.WriteLine("Timestamp, PosX, PosY, PosZ, WorldX, WorldY, WorldZ, RotX, RotY, RotZ, Distance, Power, StrokeNo, Looks");

			// Contents
			for(int i = 0; i < trackingData.Count; i++) {

				TrackingData data = trackingData[i];

				string line = string.Format("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}",
					data.timestamp,
					data.pos.x, data.pos.y, data.pos.z,
					data.worldPos.x, data.worldPos.y, data.worldPos.z,
					data.rot.x, data.rot.y, data.rot.z,
					data.distance,
					data.power,
					data.stroke,
					data.looks);
				sw.WriteLine(line);

			}

		}

	}

}
