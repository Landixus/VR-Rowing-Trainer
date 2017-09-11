using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script is for the oars
//It controls where they should be facing in relation to where the hands are and when to emit foam
public class Oar : MonoBehaviour {

	//Gets the transform of the hand that is holding it
	public Transform target;

	// Update is called once per frame
	void Update () {
		//This ensures the handle of the oar is always in the hands of the rower
		transform.LookAt(target, Vector3.up);
	}
}
