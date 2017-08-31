using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSpawn : MonoBehaviour {

    public Transform buoy;
    public float xPos = 0.0f;
    private Transform curBuoy;

    private bool canSpawn = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (curBuoy == null || Vector3.Distance(curBuoy.position, transform.position) > 20.0f)
        {
            if (canSpawn )
            {
                curBuoy = Instantiate(buoy, new Vector3(xPos, transform.position.y, transform.position.z), transform.rotation);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (!canSpawn)
            {
                canSpawn = true;
            }
        }
	}
}
