using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
//This script is applied to the buoy spawners which continue to spawn buoys until the end of the session 
public class InGameSpawn : MonoBehaviour {

	//Gets the transform component of the buoy prefab
    public Transform buoy;
	//Gets the transform component of the red buoy prefab
	public Transform redBuoy;
	//Sets the transform of the buoy that was recently spawned
	//Used to check distance between it and the player boat
    private Transform curBuoy;

	//Gets the current z position to check if the rower has reached the end of the race
	private float zPos = 0f;
	//Gets the full length of the race
	private double length = 0f;

	void Start() {
		zPos = GameObject.Find("ScenePos").GetComponent<SpawnObjects>().curZPos;
		length = GameObject.Find("SceneDataManager").GetComponent<SceneData>().length;
	}

	// Update is called once per frame
	void Update () {
		//Checks whether or not the zPos is at the end of the race
		//If it is, it will stop spawning buoys
		if (Math.Abs(zPos) <= length) {
			//Checks if the spawner had not already spawned a buoy or if the last buoy it spawned is 10m away from the spawner
			if (curBuoy == null || Vector3.Distance(curBuoy.position, transform.position) > 10.0f) {
				//Red buoys will spawn every 100m and in the last 100m of the race
				//Otherwise, yellow buoys will spawn
				if (((zPos % -100f) == 0f) || ((length + zPos) <= 100f)) {
					curBuoy = Instantiate(redBuoy, transform.position, transform.rotation, GameObject.Find("ScenePos").GetComponent<Transform>());
				} else {
					curBuoy = Instantiate(buoy, transform.position, transform.rotation, GameObject.Find("ScenePos").GetComponent<Transform>());
				}
				zPos -= 10f;
			}
		}
	}
}
