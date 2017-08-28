using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class Free_Video_Playback : MonoBehaviour {

	private double boat_speed; //speed of the boat
	private const double video_speed = 0.875; //speed the video was recorded at in m/s
	private double normalise_multiplier; //video playback speed at 1m/s
	public double video_playback; //the video playback speed to match the boat speed
	public VideoPlayer video; //video object
	public Text SpeedDisplay; //text object to display speed
	
	private PM5_Communication pm_com; //used to get data from erg

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

	private void Awake() {
		pm_com = GameObject.Find("SceneController").GetComponent<PM5_Communication>();
		boat_speed = pm_com.current_Speed;
		playerstarted = false;
	}

	// Use this for initialization
	void Start() {

		normalise_multiplier = 1 / video_speed;
		lastPlayed = 0;
		green = new Color32(0x00, 0xFF, 0x4C, 0xFF);
		red = new Color32(0xFF, 0x00, 0x00, 0xFF);
	}

	// Update is called once per frame
	void Update() {
		deltatime += Time.deltaTime;
		boat_speed = pm_com.current_Speed;
		if (!playerstarted && boat_speed > 0) {
			playerstarted = true;
			//Debug.Log("Player Started");
		}
		// Debug.Log("deltatime:" + deltatime);
		refreshVideoSpeed();
		SpeedDisplay.text = convertSpeed().ToString() + "km/h";
		lastPlayed += Time.deltaTime;
		//Debug.Log(lastPlayed);
		if (lastPlayed > 5) {
			audioController();
		}
	}

	// Used to update the speed of the environment
	public void refreshVideoSpeed() {
		video_playback = boat_speed * normalise_multiplier + 0.5;
		video.playbackSpeed = (float)video_playback;
		//Debug.Log("boat speed:" + boat_speed);
		//Debug.Log("video playback speed:" + video_playback);
	}

	//convert speed from m/s to km/h
	public double convertSpeed() {
		double convertedSpeed = boat_speed * 3600 / 1000; //convert m/s to km/h
		return convertedSpeed;
	}

	public void audioController() {
		if (convertSpeed() < minSpeed) {
			Debug.Log("Speed up");
			GetComponent<AudioSource>().PlayOneShot(speedUp);
			lastPlayed = 0;
		}
		if (convertSpeed() > maxSpeed) {
			Debug.Log("Slow Down");
			GetComponent<AudioSource>().PlayOneShot(slowDown);
			lastPlayed = 0;
		}
	}
}
