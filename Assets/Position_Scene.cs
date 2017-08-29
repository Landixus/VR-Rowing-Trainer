using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position_Scene : MonoBehaviour {
	public Transform scenepos; //seat position and rotation
	private Transform ppos;

	// Use this for initialization
	void Awake () {
		ppos = GameObject.Find("PlayerPos").transform;
		scenepos.position = ppos.position;
		scenepos.rotation = ppos.rotation;
	}
}
