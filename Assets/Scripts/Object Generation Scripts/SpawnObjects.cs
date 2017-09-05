using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//This allows the creation of marker buoys on startup
public class SpawnObjects : MonoBehaviour {

	//Specifies the object that will spawn the object
    public GameObject spawn;
	//Specifies the buoy prefab to spawn
    public Transform child;
	//Specifies the red buoy prefab that is spawned every 100m and at the last 100m of the session
	public Transform redChild;
	//Specifies the spawner prefab that is instantiated 10m in front of the last buoy spawned
	public Transform spawner;
	
	//Sets the z offset of the marker buoys to align with the front of the boat
	public float offset = 0f;
	//Determines how many rows of marker buoys will be visible in the virtual world
    public int buoyCount = 0;
	//Determines the starting x position of the first marker buoy
    private float xPos = 5f;
	//Determines the starting z position of the first row of marker buoys
    private float zPos = 0f;
	//Checks if the buoys have already been created
    private bool created = false;

	//Saves the z position of the last buoy that was spawned on startup
	//Used for the InGameSpawn script
	public float curZPos = 0f;

	//Gets the distance of the race
	public double length = 0;

	// Use this for initialization
	void Start () {

		//Sets the length of the race which is used to check if the rower is 100m from the finish
		//Determines what buoy is spawned
		length = GameObject.Find("SceneDataManager").GetComponent<SceneData>().length;

        if (!created)
        {
			//If the buoys have not already been created, a number of rows of buoys will spawn
            for (int i = 0; i < buoyCount; i++)
            {
				//Checks whether or not the zPos is at the end of the race
				//If it is, it will stop spawning buoys
				if (Math.Abs(zPos) <= length) {
					//Red buoys will spawn every 100m and in the last 100m of the race
					//Otherwise, yellow buoys will spawn
					if (((zPos % -100f) == 0f) || ((length + zPos) <= 100f)) {
						Instantiate(redChild, new Vector3(xPos, spawn.transform.position.y - 0.88f, zPos - offset), spawn.transform.rotation, spawn.transform);
						Instantiate(redChild, new Vector3(xPos - 10f, spawn.transform.position.y - 0.88f, zPos - offset), spawn.transform.rotation, spawn.transform);
						Instantiate(redChild, new Vector3(xPos - 20f, spawn.transform.position.y - 0.88f, zPos - offset), spawn.transform.rotation, spawn.transform);
					} else {
						Instantiate(child, new Vector3(xPos, spawn.transform.position.y - 0.88f, zPos - offset), spawn.transform.rotation, spawn.transform);
						Instantiate(child, new Vector3(xPos - 10f, spawn.transform.position.y - 0.88f, zPos - offset), spawn.transform.rotation, spawn.transform);
						Instantiate(child, new Vector3(xPos - 20f, spawn.transform.position.y - 0.88f, zPos - offset), spawn.transform.rotation, spawn.transform);
					}

					//Increment the next row's z position by 10
					zPos -= 10f;
				}
            }

			//Saves the current z position to the curZPos variable
			curZPos = zPos;

			Instantiate(spawner, new Vector3(xPos, spawn.transform.position.y - 0.88f, zPos - offset), spawn.transform.rotation, spawn.transform);
			Instantiate(spawner, new Vector3(xPos - 10f, spawn.transform.position.y - 0.88f, zPos - offset), spawn.transform.rotation, spawn.transform);
			Instantiate(spawner, new Vector3(xPos - 20f, spawn.transform.position.y - 0.88f, zPos - offset), spawn.transform.rotation, spawn.transform);

			//Reset the z position for next time the code is called
			zPos = 0;

			//State that the buoys have been created
            created = true;
        }
    }

	// Update is called once per frame
	void Update() {

	}
}
