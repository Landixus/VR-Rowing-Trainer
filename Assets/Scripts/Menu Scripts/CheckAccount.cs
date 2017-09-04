using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class CheckAccount : MonoBehaviour {

	private SceneData sceneData;
	private InputField username;
	public string loggedInUser;
	public string userPath;

	private void Start() {
		username = GameObject.FindWithTag("InputField").GetComponent<InputField>();
	}
	//checks if user exists or creates new user and logs in
	public void Login() {
		//string path = "C:\\Users\\Ben\\Documents\\GitHub\\VR-Rowing-Trainer\\Assets\\Accounts\\";
		string path = "D:\\Projects\\Student Teams\\VR-Rowing-Trainer\\Assets\\Accounts\\";
		try {
			// Determine whether the directory exists.
			if (Directory.Exists(path + username.text)) {
				loggedInUser = username.text;
				userPath = path + loggedInUser;
				Debug.Log("That path exists already.");
				return;
			} else {
				// Try to create the directory.
				Directory.CreateDirectory(path + username.text);
				loggedInUser = username.text;
				userPath = path + loggedInUser;
				Debug.LogFormat("The directory was created successfully at {0}", Directory.GetCreationTime(userPath));
			}
		} catch (Exception e) {
			Debug.LogFormat("The process failed: {0}", e.ToString());
		} finally { }

	}

	public void UpdateAccountData() {
		sceneData = GameObject.Find("SceneDataManager").GetComponent<SceneData>();
		sceneData.loggedInUser = loggedInUser;
		sceneData.userPath = userPath;
	}
}
