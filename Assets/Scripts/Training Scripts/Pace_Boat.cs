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
	private Video_Playback videoPlayback; //video playback component from video
	private Animation anim; //animation speed of pacing boat
    private double restrictionLength; // Used to give the pacing boat a restriction of how far it can lead or trail the user

    // Use this for initialization
    void Start() {
        pbspeed = GameObject.Find("SceneDataManager").GetComponent<SceneData>().targetSpeed;
        videoPlayback = GetComponent<Video_Playback>();
		anim = pb.GetComponent<Animation>();
        restrictionLength = 20;
        //Sets speed of animation
        foreach (AnimationState state in anim) {
			state.speed = (float)(1 + (pbspeed * 0.125));
		}
	}

	// Update is called once per frame
	void Update() {
        // Only get the speed difference and moves the boat when the user has started to row
		if (videoPlayback.playerStarted) {
            Get_Speed_Difference();
            Move_Pace_Boat();
		}

	}

    // Gets the difference in speed between the user and the pacing boat
	public void Get_Speed_Difference() {
		playerspeed = videoPlayback.playerSpeed;
		deltaspeed = (float)(playerspeed - pbspeed);
	}

    // Used to move the pacing boat in relation to the speed difference
	public void Move_Pace_Boat() {
        // Used to restrict the pacing boat to the set length from the user's boat
		if (pb.position.z < restrictionLength && pb.position.z > -restrictionLength) {
			pb.position += pb.forward * deltaspeed * Time.deltaTime;
		} else {
            // If the pacing boat exceeds the restriction length it moves in the opposite direction bringing it back into the allowed zone
			pb.position -= pb.forward * deltaspeed * Time.deltaTime;
		}
	}
}