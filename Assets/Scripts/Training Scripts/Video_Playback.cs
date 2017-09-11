/*  Author: Benjamin Ferguson
    Date: 10/05/17
    Purpose: To match the playback speed of the 360 video with the rowing speed.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class Video_Playback : MonoBehaviour {
	private double boat_speed; //speed of the boat
    private const double video_speed = 1; //speed the video was recorded at in m/s
	private double normalise_multiplier; //video playback speed at 1m/s
    public double video_playback; //the video playback speed to match the boat speed
    public VideoPlayer video; //video object
	public Text SpeedDisplay; //text object to display speed
    private Pace_Boat pb; //pace boat object
	private PM5_Communication pm_com; //used to get data from erg
	private SceneData sceneData;

    private float framerate; //rate to refresh video playback speed
	private float deltatime; //time since last refresh

    public double minSpeed; //speed at which the player needs to speed up
    public double maxSpeed; //speed at which the player needs to slow down
    public AudioClip speedUp;
    public AudioClip slowDown;

    private float lastPlayed; //time since audio was last played
    private Color green;
    private Color red;
	public bool playerstarted;
	private AudioSource audioSource;
	private bool finished;

	private bool freeSession;

	private void Awake() {
		pm_com = GetComponent<PM5_Communication>();
		sceneData = GameObject.Find("SceneDataManager").GetComponent<SceneData>();
		
		freeSession = sceneData.freeSession;
		if (!freeSession) {
			pb = GetComponent<Pace_Boat>();
		} else {
			Destroy(GameObject.Find("pacing_boat"));
			Destroy(GameObject.Find("SceneController").GetComponent<Pace_Boat>());
			Debug.Log("Free Session");
		}
		boat_speed = pm_com.current_Speed;
		playerstarted = false;
		video.StepForward();
		finished = false;
	}

	// Use this for initialization
	void Start () {
		

		normalise_multiplier = 1 / video_speed;
		
        lastPlayed = 0;
        green = new Color32(0x00, 0xFF, 0x4C, 0xFF);
		red = new Color32(0xFF, 0x00, 0x00, 0xFF);
		audioSource = GetComponent<AudioSource>();
		minSpeed = sceneData.minSpeed;
		maxSpeed = sceneData.maxSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		finished = GetComponent<Training_Summary>().finished;
		if (!finished) {
			deltatime += Time.deltaTime;
			boat_speed = pm_com.current_Speed;

			if (!playerstarted && boat_speed > 0) {
				playerstarted = true;
				if (!freeSession) {
					GameObject.Find("pacing_boat").GetComponent<Animation>().Play();
				}
			}
			RefreshVideoSpeed();

			if (!freeSession) {
				if (video_playback > pb.pbspeed) {
					SpeedDisplay.color = green;
				} else {
					SpeedDisplay.color = red;
				}
				lastPlayed += Time.deltaTime;
				if (lastPlayed > 5 && playerstarted) {
					AudioController();
				}
			}
			SpeedDisplay.text = Math.Round(video_playback, 2).ToString("F") + " m/s";
		}
	}

	// Used to update the speed of the environment
    public void RefreshVideoSpeed() {
        video_playback = boat_speed * normalise_multiplier;
        video.playbackSpeed = (float) video_playback;
    }

	public void AudioController()
    {
       if (video_playback < minSpeed)
        {
            audioSource.PlayOneShot(speedUp);
            lastPlayed = 0;
        }
       if (video_playback > maxSpeed)
        {
			audioSource.PlayOneShot(slowDown);
            lastPlayed = 0;
        }
    }
}