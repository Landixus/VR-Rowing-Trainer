using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script controls the behaviour of the foam that is generated when the oar comes into contact with the water
public class Foam : MonoBehaviour {
	//This gets the Video Playback component from the Video
	private Video_Playback vp;
	//Saves the speed from the Video Playback component
	private double speed;

	//This specifies how long the foam remains before being destroyed
	private int life;

	// Use this for initialization
	void Start () {
		vp = GameObject.Find("SceneController").GetComponent<Video_Playback>();
	}
	
	// Update is called once per frame
	void Update () {
		//Increment its life by 1
		life++;

		//This moves the foam at the speed of the video
		speed = vp.video_playback;
		transform.position += transform.forward * Time.deltaTime * (float)speed;

		//When the life value reaches 30, destroy it
		if (life >= 30) {
			Destroy(gameObject);
		}
	}
}
