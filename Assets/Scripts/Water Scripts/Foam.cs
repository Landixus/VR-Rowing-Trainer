using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script controls the behaviour of the foam that is generated when the oar comes into contact with the water
public class Foam : MonoBehaviour {
	
	//This gets the Video Playback component from the Video
	private Video_Playback vp;
	//Saves the speed from the Video Playback component
	private double speed;

	//This gets the animator component of the splash sprite
	private Animator anim;

	private int time = 0;

	// Use this for initialization
	void Start () {
		vp = GameObject.Find("SceneController").GetComponent<Video_Playback>();

		anim = GetComponent<Animator>();
		//Plays animation
		anim.Play("Splash");
	}
	
	// Update is called once per frame
	void Update () {
		time++;

		//This moves the foam at the speed of the video
		speed = vp.playerSpeed;
		transform.position += transform.up * Time.deltaTime * (float)speed;

		//When the life value reaches 30, destroy it
		if (time >= 30) {
			Destroy(gameObject);
		}
	}
}
