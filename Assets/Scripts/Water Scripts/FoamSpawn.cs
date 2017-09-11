using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script dictates when the foam spawns
public class FoamSpawn : MonoBehaviour {
	//Gets the transform of ScenePos
	public Transform scenePos;
	
	//Gets the transform of the foam spawn object attached to the oar object
	public Transform foamTrans;
	//Gets the transform of the trigger
	public Transform trigTrans;
	//Determines whether or not the foam should spawn
	private bool canSpawn = true;
	//Gets the tranform of the foam prefab in order to spawn it
	public Transform foam;

	private AudioSource audioSource;
	public AudioClip row;

	void Start() {
		audioSource = GameObject.Find("SceneController").GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {
		//Always match the position of the foam spawner with that of the transform position attached to the oar
		transform.position = foamTrans.position;
		
		//If the z position of the foam spawner is past the z position of the trigger and it can spawn, spawn the foam and set canSpawn to false
		if (transform.localPosition.z >= trigTrans.localPosition.z && canSpawn) {
			Instantiate(foam, new Vector3(foamTrans.position.x, -0.916f, foamTrans.position.z), Quaternion.Euler(new Vector3(90f, scenePos.rotation.eulerAngles.y, 0f)), scenePos);
			canSpawn = false;
			audioSource.PlayOneShot(row);
		}
		if (transform.localPosition.z < trigTrans.localPosition.z && !canSpawn) {
			canSpawn = true;
		}
	}
}
