using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pace_Boat : MonoBehaviour {
	public double pbspeed; //speed of the pace boat in m/s
	public double playerspeed; //speed of the player boat in m/s
	public float deltaspeed; //difference in speed
	public Transform pb; //position of the pace boat
	private Video_Playback vb;
	//public GetTargetSpeed ts; //
	//public GameObject slider;

	private void Awake() {
		try {
			pbspeed = GameObject.Find("SceneDataManager").GetComponent<GetTargetSpeed>().SliderValue;
		} catch { };
		//Debug.Log(pbspeed);
	}


	// Use this for initialization
	void Start() {
		pbspeed = 2;
		vb = GetComponent<Video_Playback>();
	}

	// Update is called once per frame
	void Update() {
		Get_Speed_Difference();
		if (vb.playerstarted) {
			Move_Pace_Boat();
		}

	}

	public void Get_Speed_Difference() {
		playerspeed = vb.video_playback;
		deltaspeed = (float)(playerspeed - pbspeed);
	}

	public void Move_Pace_Boat() {
		if (pb.position.z < 10 && pb.position.z > -10) {
			pb.position += Vector3.forward * deltaspeed * Time.deltaTime;
		} else {
			pb.position += Vector3.back * deltaspeed * Time.deltaTime;
		}
	}
}