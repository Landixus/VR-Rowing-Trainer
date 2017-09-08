using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script is for the oars of the pacing boat
//It controls when to emit foam
public class PaceOar : MonoBehaviour {
	//Gets the transform of the ScenePos object
	public Transform scenePos;

	//Gets the transform of an empty game object attached to the tip of the oar
	public Transform foamTarget;
	//Gets the tranform of the foam prefab
	public Transform foam;

	private void OnCollisionEnter(Collision col) {
		//If the oar collided with the water, emit foam if the oar was not already in the collider
		if (col.gameObject.name == "Water") {
			Instantiate(foam, new Vector3(foamTarget.position.x, col.gameObject.transform.position.y + 0.01f, foamTarget.position.z), scenePos.rotation, scenePos);
		}
		/*if (col.gameObject.name == "SndTrigger") {

		}*/
	}
}