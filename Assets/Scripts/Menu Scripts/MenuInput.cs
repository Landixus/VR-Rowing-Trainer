using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using VRStandardAssets.Utils;
<<<<<<< HEAD

=======
>>>>>>> master

public class MenuInput : MonoBehaviour {

	public EventSystem eventSystem;
	private Button selectedButton;
	private double selectTimer;
	private bool buttonSelected;

<<<<<<< HEAD
	private VRInteractiveItem interactiveItem;
	public VREyeRaycaster eyeRaycaster;

	private void Start() {
		selectTimer = 0.0;
	}

	void Update() {
		rayHit();
	}

	public void rayHit() {
=======
	//private StandaloneInputModule sim;
    private bool buttonSelected;

	private VRInteractiveItem interactiveItem;
	//public UnityEvent GazeEnterEvent;
	//public UnityEvent GazeExitEvent;

	private void Start() 
	{
		
		interactiveItem = GetComponent<VRInteractiveItem>();
		interactiveItem.OnOver += onGazeEnter;
		interactiveItem.OnOver += onGazeExit;
		//sim = eventSystem.GetComponent<StandaloneInputModule>();
		//Debug.Log(sim.input);
}
	
	void Update()
    {
		rayHit();
	}

	public void rayHit() 
	{
>>>>>>> master
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		Debug.DrawRay(ray.origin, ray.direction * 5, Color.red);
		if (Physics.Raycast(ray.origin, ray.direction, out hit, 10f)) {
<<<<<<< HEAD
			if (!buttonSelected && hit.transform.tag == "Button") {
=======
			if (!buttonSelected) {
>>>>>>> master
				buttonSelected = true;
				selectedButton = hit.transform.GetComponent<Button>();
				selectedButton.Select();
			} else if (buttonSelected && hit.transform.tag != "Button") {
				eventSystem.SetSelectedGameObject(null);
				buttonSelected = false;
			}
		} else if (buttonSelected) {
				eventSystem.SetSelectedGameObject(null);
				buttonSelected = false;
		}

		if (buttonSelected && Input.GetMouseButtonDown(0)) {
			selectedButton.onClick.Invoke();
		}
	}

	public void EnterGaze() {
		interactiveItem = eyeRaycaster.CurrentInteractible;
		Debug.Log("Gaze Enter " + interactiveItem.GetComponent<Button>());
		if (!buttonSelected && interactiveItem.tag == "Button") {
			
			buttonSelected = true;
			selectedButton = interactiveItem.GetComponent<Button>();
			selectedButton.Select();
			Debug.Log(interactiveItem.name + " selected");
			//click timer creates crash
			//clickTimer();
		}
	}

<<<<<<< HEAD
	public void ExitGaze() {
		Debug.Log("Gaze Exit " + interactiveItem.GetComponent<Button>());
		if (buttonSelected) {
			eventSystem.SetSelectedGameObject(null);
			buttonSelected = false;
		}
	}

	public void clickTimer() {
		float startTime = Time.time;
		Debug.Log("Start time: " + startTime);
		bool timeElapsed = true;

		while (Time.time - startTime < 3.0f) {
			if (!buttonSelected) {
				timeElapsed = false;
			}
		}
		Debug.Log("End time: " + Time.time);
		if (timeElapsed) {
			//selectedButton.onClick.Invoke();
			Debug.Log("time elapsed");
		}
	}

	public void onDisable() {
		buttonSelected = false;
	}
	
=======
		if (Input.GetAxisRaw("Vertical") != 0 && buttonSelected == false) {
			eventSystem.SetSelectedGameObject(selectedObject);
			buttonSelected = true;
		}
		//Debug.Log("Is the mouse present " + Input.mousePresent);
		//Debug.Log("Mouse button down " + Input.GetMouseButtonDown(0));
		if (eventSystem.IsPointerOverGameObject()) {
			pointerOver = true;
			Debug.Log("Pointer over " + pointerOver);
		} else {
			pointerOver = false;
		}
	}

	void onGazeEnter() {
		//GazeEnterEvent.Invoke();
		Debug.Log("Gaze Enter");
		if (!buttonSelected) {
			buttonSelected = true;
			if (interactiveItem.tag == "Button") {
				selectedButton = interactiveItem.GetComponent<Button>();
				selectedButton.Select();

			}
		}
	}

	void onGazeExit() {
		Debug.Log("Gaze Exit");
		//GazeExitEvent.Invoke();
		if (buttonSelected) {
			eventSystem.SetSelectedGameObject(null);
			buttonSelected = false;
		}
	}

	public void onDisable()
    {
        buttonSelected = false;
    }
>>>>>>> master
}
