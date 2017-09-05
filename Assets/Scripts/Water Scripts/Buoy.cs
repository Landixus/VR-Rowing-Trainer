using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This controls the speed of the buoy object
public class Buoy : MonoBehaviour {

	//Gets the video playback speed to set the speed of the buoys
	private Video_Playback vp;
	//Sets speed of buoys
	public double speed;

	// Use this for initialization
	void Start()
    {
		vp = GameObject.Find("SceneController").GetComponent<Video_Playback>();
	}

    // Update is called once per frame
    void Update()
    {
		//This moves the distant buoy at a set speed
		speed = vp.video_playback;
		transform.position += transform.forward * Time.deltaTime * (float)speed;
	}
}
