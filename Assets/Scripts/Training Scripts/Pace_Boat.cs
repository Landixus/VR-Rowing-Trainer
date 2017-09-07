/*  Author: Benjamin Ferguson
    Date: 05/09/17
    Purpose: To get the speed value from the user and set the speed of the
	pacing boat. Using the difference in speed of the player and the pacing
	boat to control the movement of the pacing boat.
*/

using UnityEngine;

public class Pace_Boat : MonoBehaviour {
	public double pbspeed; //speed of the pace boat in m/s
	public double playerspeed; //speed of the player boat in m/s
	public float deltaspeed; //difference in speed
	public Transform pb; //position of the pace boat
	private Video_Playback videoPlayback;
	//public GetTargetSpeed ts; //
	//public GameObject slider;

	private void Awake() {
		try {
			pbspeed = GameObject.Find("SceneDataManager").GetComponent<SceneData>().targetSpeed;
		} catch { };
		//Debug.Log(pbspeed);
	}


	// Use this for initialization
	void Start() {
		videoPlayback = GetComponent<Video_Playback>();
	}

	// Update is called once per frame
	void Update() {
		Get_Speed_Difference();
		if (videoPlayback.playerstarted) {
			Move_Pace_Boat();
		}

	}

	public void Get_Speed_Difference() {
		playerspeed = videoPlayback.video_playback;
		deltaspeed = (float)(playerspeed - pbspeed);
	}

	public void Move_Pace_Boat() {
		if (pb.position.z < 20 && pb.position.z > -20) {
			pb.position += pb.forward * deltaspeed * Time.deltaTime;
		} else {
			pb.position -= pb.forward * deltaspeed * Time.deltaTime;
		}
	}
}