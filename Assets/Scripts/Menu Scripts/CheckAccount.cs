/*  Author: Benjamin Ferguson
    Date: 07/09/17
    Purpose: When a user logs in, it check to see if a a directory already 
	exists or a new directory needs	to be created. Once a user logs in it stores
	the path of the user's directory for storing their training data.
*/
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class CheckAccount : MonoBehaviour {

	private SceneData sceneData; // Used to store the logged in user's name and path
	private InputField username; // Used to retreive the text entered by the user
	private Data_Logging datalogging; 
	public string loggedInUser; // Stores the name of the logged in user
	public string userPath; // Stores the directory path of the logged in user

	// Used to initialise the input field object so the text can be retreived
	private void Start() {
		username = GameObject.FindWithTag("InputField").GetComponent<InputField>();
	}

	// Checks if user exists or creates new user and logs in
	public void Login() {
		// This is the path of the accounts folder inside the Unity project
		// **** WARNING **** This will change when the Unity project is on another computer
		string path = "D:\\Projects\\Student Teams\\VR-Rowing-Trainer\\Assets\\Accounts\\";
		// Try catch any errors when finding or creating a directory and handle those errors appropriately
		try {
			// Determine whether the directory exists, then set the username and user path
			if (Directory.Exists(path + username.text)) {
				loggedInUser = username.text;
				userPath = path + loggedInUser;
				return;
			} else {
				//Try to create a new directory, then set the username and user path
				Directory.CreateDirectory(path + username.text);
				loggedInUser = username.text;
				userPath = path + loggedInUser;
			}
		} catch (Exception e) {
			// Logs to console if there were any errors
			Debug.LogFormat("The process failed: {0}", e.ToString());
		} finally { }

	}

	// Stores the user's name and directory path in the scene data manager
	// This allows the user's data to not be destroyed when changing scenes
	public void UpdateAccountData() {
		sceneData = GameObject.Find("SceneDataManager").GetComponent<SceneData>();
		sceneData.loggedInUser = loggedInUser;
		sceneData.userPath = userPath;
	}
}
