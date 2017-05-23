/*  Author: Benjamin Ferguson
    Date: 10/05/17
    Purpose: To match the speed of the 360 video playback with the rowing speed.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Video_Playback_Speed : MonoBehaviour {
    public static double boat_speed; //speed of the boat
    public const double video_speed = 0.875; //speed the video was recorded at in m/s
    public static double normalise_multiplier; //video playback speed at 1m/s
    public static double video_playback; //the video playback speed to match the boat speed
    public static VideoPlayer video; //video object

	// Use this for initialization
	void Start () {
        video = GameObject.Find("VideoSphere").GetComponent<VideoPlayer>();
        normalise_multiplier = 1 / video_speed;
        Debug.Log("normaliser:" + normalise_multiplier);
    }
	
	// Update is called once per frame
	void Update () {
        refreshVideoSpeed();
    }

	// Used to update the speed of the environment
    public static void refreshVideoSpeed() {
        boat_speed = Rowing_Speed.speed;
        video_playback = boat_speed * normalise_multiplier;
        video.playbackSpeed = (float) video_playback;
        Debug.Log("boat speed:" + boat_speed);
        Debug.Log("video playback speed:" + video_playback);
    }
}