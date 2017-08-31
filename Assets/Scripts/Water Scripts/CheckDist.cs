using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDist : MonoBehaviour {

    public Transform other;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.E))
        {
            Debug.Log(Vector3.Distance(other.position, transform.position));
        }
	}
}
