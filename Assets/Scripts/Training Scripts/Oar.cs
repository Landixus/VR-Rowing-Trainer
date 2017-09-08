using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script is for the oars
//It controls where they should be facing in relation to where the hands are and when to emit foam
public class Oar : MonoBehaviour {
	//Gets the transform of the ScenePos object
	public Transform scenePos;
	
	//Gets the transform of the hand that is holding it
	public Transform target;
	
	//Gets the transform of an empty game object attached to the tip of the oar
	public Transform foamTarget;
	//Gets the tranform of the foam prefab
	public Transform foam;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//This ensures the handle of the oar is always in the hands of the rower
		transform.LookAt(target, Vector3.up);
	}
	private void OnCollisionEnter(Collision col) {
		//If the oar collided with the water, emit foam if the oar was not already in the collider
		if (col.gameObject.name == "Water") {
			Instantiate(foam, new Vector3(foamTarget.position.x, col.gameObject.transform.position.y + 0.01f, foamTarget.position.z), scenePos.rotation, scenePos);
			Debug.Log("Foam spawn");
		}
		/*if (col.gameObject.name == "SndTrigger") {

		}*/
	}
}
