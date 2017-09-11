/*  Author: Benjamin Ferguson
    Date: 07/09/17
    Purpose: Stores the data set by the user for their session 
	so it can be used by various scripts.
*/
using UnityEngine;

public class SceneData : MonoBehaviour {

	public string loggedInUser; // Stores the name of the logged in user
	public string userPath; // Stores the user's directory path
	public double length; // Stores the length of the training session the user set
	public double minSpeed; // Stores the minimum speed the user set
	public double maxSpeed; // Stores the minimum speed the user set
	public float targetSpeed; // Stores the target speed the user set
	public bool freeSession;
}
