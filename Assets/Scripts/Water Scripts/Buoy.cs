using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This controls the speed of the buoy object
public class Buoy : MonoBehaviour {

	//Gets the video playback speed to set the speed of the buoys
	private Video_Playback vp;
	//Sets speed of buoys
	public double speed;

	//Gets the transform of the ScenePos object
	private Transform scenePos;

	// Use this for initialization
	void Start()
    {
		vp = GameObject.Find("SceneController").GetComponent<Video_Playback>();
		scenePos = GameObject.Find("ScenePos").GetComponent<Transform>();
	}

    // Update is called once per frame
    void Update()
    {
		//This moves the buoy at a set speed
		speed = vp.playerSpeed;
		transform.position += transform.forward * Time.deltaTime * (float)speed;

		//If the buoy is 250m away from the player, it will be destroyed
		if (Vector3.Distance(scenePos.position, transform.position) >= 250f) {
			Destroy(gameObject);
		}
	}
}
