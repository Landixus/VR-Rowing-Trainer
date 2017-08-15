using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using VRStandardAssets.Utils;

public class MenuInput : MonoBehaviour {

	public EventSystem eventSystem;
	public GameObject selectedObject;
	private Button selectedButton;
	public bool pointerOver;

	//private StandaloneInputModule sim;
	private bool buttonSelected;

	private VRInteractiveItem interactiveItem;
	public VREyeRaycaster eyeRaycaster;
	//public UnityEvent GazeEnterEvent;
	//public UnityEvent GazeExitEvent;

	private void Start() {
		
		//interactiveItem = GetComponent<VRInteractiveItem>();
		//interactiveItem.OnOver += onGazeEnter;
		//interactiveItem.OnOut += onGazeExit;
		//sim = eventSystem.GetComponent<StandaloneInputModule>();
		//Debug.Log(sim.input);
	}

	void Update() {
		interactiveItem = eyeRaycaster.CurrentInteractible;
		rayHit();
	}

	public void rayHit() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		Debug.DrawRay(ray.origin, ray.direction * 5, Color.red);
		if (Physics.Raycast(ray.origin, ray.direction, out hit, 10f)) {
			if (!buttonSelected) {
				buttonSelected = true;
				if (hit.transform.tag == "Button") {
					selectedButton = hit.transform.GetComponent<Button>();
					selectedButton.Select();

				}
			}
		} else {
			if (buttonSelected) {
				eventSystem.SetSelectedGameObject(null);
				buttonSelected = false;
			}
		}

		if (buttonSelected && Input.GetMouseButtonDown(0)) {
			selectedButton.onClick.Invoke();
		}


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
	/*
	void onGazeEnter() {
		GazeEnterEvent.Invoke();
	}

	void onGazeExit() {
		GazeExitEvent.Invoke();
	}

	public void EnterGaze() {
		Debug.Log("Gaze Enter");
		if (!buttonSelected) {
			buttonSelected = true;
			if (interactiveItem.tag == "Button") {
				selectedButton = interactiveItem.GetComponent<Button>();
				selectedButton.Select();

			}
		}
	}

	public void ExitGaze() {
		Debug.Log("Gaze Exit");
		if (buttonSelected) {
			eventSystem.SetSelectedGameObject(null);
			buttonSelected = false;
		}
	}

	public void onDisable() {
		buttonSelected = false;
	}
	*/
}
