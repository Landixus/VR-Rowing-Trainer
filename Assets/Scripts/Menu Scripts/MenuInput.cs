using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using VRStandardAssets.Utils;


public class MenuInput : MonoBehaviour {

	public EventSystem eventSystem;
	private Button selectedButton;
	private double selectTimer;
	private bool buttonSelected;
	private bool startTimer;
	private bool timeElapsed;
	private float startTime;
	private AudioSource audioSource;
	public AudioClip menuClick;


	private VRInteractiveItem interactiveItem;
	public VREyeRaycaster eyeRaycaster;

	private void Start() {
		startTimer = false;
		timeElapsed = false;
		audioSource = GameObject.Find("Audio").GetComponent<AudioSource>();
	}

	private void Update() {
		if (startTimer) {
			timeElapsed = false;
			if (Time.time - startTime > 2.0f) {
				timeElapsed = true;
				Debug.Log("End time: " + Time.time);
			}
			if (timeElapsed) {
				audioSource.PlayOneShot(menuClick);
				selectedButton.onClick.Invoke();
				Debug.Log("time elapsed");
				startTimer = false;
			}
		}
	}

	public void EnterGaze() {
		interactiveItem = eyeRaycaster.CurrentInteractible;
		//Debug.Log("Gaze Enter " + interactiveItem.GetComponent<Button>());
		if (!buttonSelected && interactiveItem.tag == "Button") {

			buttonSelected = true;
			selectedButton = interactiveItem.GetComponent<Button>();
			selectedButton.Select();
			//Debug.Log(interactiveItem.name + " selected");
			startTimer = true;
			startTime = Time.time;
			Debug.Log("Start time: " + startTime + "button: " + interactiveItem.GetComponent<Button>());
		}
	}

	public void ExitGaze() {
		//Debug.Log("Gaze Exit " + interactiveItem.GetComponent<Button>());
		if (buttonSelected) {
			eventSystem.SetSelectedGameObject(null);
			buttonSelected = false;
			startTimer = false;
		}
	}

	public void onDisable() {
		buttonSelected = false;
	}

}
