using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;
using UnityEngine.Events;

[RequireComponent(typeof(VRInteractiveItem))]
public class VRIteractiveEventHandler : MonoBehaviour {

	private VRInteractiveItem interactiveItem;
	//public UnityEvent GazeEnterEvent;
	//public UnityEvent GazeExitEvent;

	public MenuInput menuInput;
	// Use this for initialization
	void Start () {
		interactiveItem = GetComponent<VRInteractiveItem>();
		interactiveItem.OnOver += onGazeEnter;
		interactiveItem.OnOut += onGazeExit;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void onGazeEnter() {
		//GazeEnterEvent.Invoke();
		menuInput.EnterGaze();
	}

	void onGazeExit() {
		//GazeExitEvent.Invoke();
		menuInput.ExitGaze();
	}

}
