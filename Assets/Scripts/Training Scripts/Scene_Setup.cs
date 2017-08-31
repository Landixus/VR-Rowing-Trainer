using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_Setup : MonoBehaviour {
	public GameObject scenepos; //seat position and rotation
	private Transform triggerpos;
	private Transform playerboat;
	private GameObject playerpos;
	// Use this for initialization
	void Awake () {
		scenepos = GameObject.Find("ScenePos");
		playerpos = GameObject.Find("SceneDataManager");
		triggerpos = playerpos.transform;
		scenepos.transform.SetParent(triggerpos, false);
		//scenepos.position = triggerpos.position;
		//scenepos.rotation = triggerpos.rotation;
		//playerboat = GameObject.Find("player_boat").transform;
		//playerboat.position += triggerpos.position;
	}
}
