using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This allows the creation of marker buoys on startup
public class SpawnObjects : MonoBehaviour {

	//Specifies the object that will spawn the object
    public GameObject spawn;
	//Specifies the prefab that is spawned
    public Transform child;

	//Determines how many rows of marker buoys will be visible in the virtual world
    public int buoyCount = 0;
	//Determines the starting x position of the first marker buoy
    private float xPos = 5f;
	//Determines the starting z position of the first row of marker buoys
    private float zPos = 0f;
	//Checks if the buoys have already been created
    private bool created = false;

	// Use this for initialization
	void Start () {

        if (!created)
        {
			//If the buoys have not already been created, a number of rows of buoys will spawn
            for (int i = 0; i < buoyCount; i++)
            {
                Instantiate(child, new Vector3(xPos, spawn.transform.position.y - 0.88f, zPos), spawn.transform.rotation, spawn.transform);
                Instantiate(child, new Vector3(xPos - 10f, spawn.transform.position.y - 0.88f, zPos), spawn.transform.rotation, spawn.transform);
				Instantiate(child, new Vector3(xPos - 20f, spawn.transform.position.y - 0.88f, zPos), spawn.transform.rotation, spawn.transform);

				//Increment the next row's z position by 20
                zPos -= 10f;
            }

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
