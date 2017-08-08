﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour {

	public Text fps;
	public float updateInterval = 0.5f;

	private float accum = 0.0f; // FPS accumulated over the interval
	private float frames = 0.0f; // Frames drawn over the interval
	private float timeleft; // Left time for current interval
 


	// Use this for initialization
	void Start () {

		if (!fps.GetComponent<Text> ()) {
			Debug.Log("FramesPerSecond needs a GUIText component!");
			enabled = false;
			return;
		}
		timeleft = updateInterval;
	}
	
	// Update is called once per frame
	void Update () {
		timeleft -= Time.deltaTime;
		accum += Time.timeScale / Time.deltaTime;
		++frames;

		// Interval ended - update GUI text and start new interval
		if (timeleft <= 0.0f) {
			// display two fractional digits (f2 format)
			fps.text = "FPS: " + (accum / frames).ToString("f2");
			timeleft = updateInterval;
			accum = 0.0f;
			frames = 0.0f;
		}
	}
}