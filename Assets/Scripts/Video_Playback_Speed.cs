/*  Author: Benjamin Ferguson
    Date: 10/05/17
    Purpose: To match the playback speed of the 360 video with the rowing speed.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class Video_Playback_Speed : MonoBehaviour {
    public double boat_speed; //speed of the boat
    public const double video_speed = 0.875; //speed the video was recorded at in m/s
    public  double normalise_multiplier; //video playback speed at 1m/s
    public double video_playback; //the video playback speed to match the boat speed
    public VideoPlayer video; //video object
	public Text SpeedDisplay; //text object to display speed
    public float framerate; //rate to refresh video playback speed
    public float deltatime; //time since last refresh

	// Use this for initialization
	void Start () {
        video = GameObject.Find("VideoSphere").GetComponent<VideoPlayer>();
		boat_speed = 5; ;
		refreshVideoSpeed();
        normalise_multiplier = 1 / video_speed;
        //framerate = 0.05f;
        //deltatime = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {
        deltatime += Time.deltaTime;
		//boat_speed = Rowing_Speed.speed;
		SpeedDisplay.text = convertSpeed().ToString() + "km/h";
		// Debug.Log("deltatime:" + deltatime);
		refreshVideoSpeed();
		/* if (deltatime >= framerate)
		 {
			 refreshVideoSpeed();
			 deltatime = 0.0f;
		 }
		 */
	}

	// Used to update the speed of the environment
    public void refreshVideoSpeed() {
        video_playback = boat_speed * normalise_multiplier;
        video.playbackSpeed = (float) video_playback;
        //Debug.Log("boat speed:" + boat_speed);
        //Debug.Log("video playback speed:" + video_playback);
    }

	public double convertSpeed() 
	{
		double convertedSpeed = boat_speed * 3600/ 1000; //convert m/s to km/h
		return convertedSpeed;
	}
}