/*  Author: Benjamin Ferguson
    Date: 10/05/17
    Purpose: To match the playback speed of the 360 video with the rowing speed.
*/

using System;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class Video_Playback : MonoBehaviour {
	public double playerSpeed; // The speed value retrieved from the ERG
    private const double videoSpeed = 1.2; // The speed the video was recorded at in m/s
	private double normaliseMultiplier; // Used to change the video speed so that it plays at the same rate as the user is rowing
    public double videoPlayback; // The video playback speed to match the boat speed
    public VideoPlayer video; // The video object used to access the video's components
	
    private Pace_Boat pb; // Used to access the pace boat script components
	private PM5_Communication pmCom; // Used to get access the data from pm5 communication script
	private SceneData sceneData; // Used to access the data from the scene data script
    private double minSpeed; // Used to set the speed at which the player needs to speed up
    private double maxSpeed; // Used to set the speed at which the player needs to slow down
    private bool freeSession; // Used to indicate what session the user chose

    private AudioSource audioSource; // Used to allow the use of playing audio clips
    public AudioClip speedUp; // Plays when the user needs to speed up
    public AudioClip slowDown; // Plays when the user needs to slow down
    private float lastPlayed; // Used to get the time since the audio was last played
    public Text SpeedDisplay; // Used to display the user's speed
    private Color green; // Used to store the colour green for use by the speed display
    private Color red; // Used to store the colour red for use by the speed display

    public bool playerStarted; // Used to indicate if the user has started rowing 
	public bool finished; // Used to indicate if the user has finished
	
    // Used to initialise the session depending on the session type
	private void Awake() {
        pmCom = GetComponent<PM5_Communication>();
		sceneData = GameObject.Find("SceneDataManager").GetComponent<SceneData>();
		freeSession = sceneData.freeSession;
        // Check what session was selected
        if (!freeSession) {
            // Custom session. Get the pace boat script component
			pb = GetComponent<Pace_Boat>();
		} else {
            // Free session. Destroy the pacing boat object and the script to control the pacing boat
			Destroy(GameObject.Find("pacing_boat"));
			Destroy(GameObject.Find("SceneController").GetComponent<Pace_Boat>());
		}
		playerSpeed = 0;
		playerStarted = false;
		finished = false;
        // Set the video forward by one frame. This helps eliminate the video not playing straight away
        video.StepForward();
    }

	// Used for more initialisation
	void Start () {
		normaliseMultiplier = 1 / videoSpeed;
        lastPlayed = 0;
        green = new Color32(0x00, 0xFF, 0x4C, 0xFF);
		red = new Color32(0xFF, 0x00, 0x00, 0xFF);
		audioSource = GetComponent<AudioSource>();
		minSpeed = sceneData.minSpeed;
		maxSpeed = sceneData.maxSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        // Continue to update while the user has not finished the race
		if (!finished) {
            // Get the user's speed from the ERG
			playerSpeed = pmCom.current_Speed;
            // Used to check if the user has started rowing
			if (!playerStarted && playerSpeed > 0) {
				playerStarted = true;
                // Used to play the pacing boat animation once the user has started in a custom session
				if (!freeSession) {
					GameObject.Find("pacing_boat").GetComponent<Animation>().Play();
				}
			}
            // Used to update the video speed
			RefreshVideoSpeed();
            // Used to only update the colour of the speed display and play the audio in a custom session 
			if (!freeSession && playerStarted) {
                // Used to change the colour of the speed display 
				if (playerSpeed > pb.pbspeed) {
                    // Change it green if the user is going faster than the pacing boat
                    SpeedDisplay.color = green;
				} else {
                    // Change it red if the user is going slower than the pacing boat
                    SpeedDisplay.color = red;
				}
          		lastPlayed += Time.deltaTime;
                // Used to call the audio controller every 5 seconds 
				if (lastPlayed > 5) {
					AudioController();
				}
			}
            // Used to update the speed display
			SpeedDisplay.text = Math.Round(videoPlayback, 2).ToString("F") + " m/s";
		}
	}

	// Used to update the speed of the 360 video
    public void RefreshVideoSpeed() {
        videoPlayback = playerSpeed * normaliseMultiplier;
        video.playbackSpeed = (float) videoPlayback;
    }

    // Used to play the speed audio when the user falls outside of the speed range set in the menu
	public void AudioController()
    {
        // Used to play the speed up audio if the user is rowing slower than the minimum speed set
        if (playerSpeed < minSpeed) {
            audioSource.PlayOneShot(speedUp);
        }
        // Used to play the slow down audio if the user is rowing faster than the maximum speed set
        if (playerSpeed > maxSpeed) {
            audioSource.PlayOneShot(slowDown);
        }
        // Reset the time since last played
        lastPlayed = 0;
    }
}