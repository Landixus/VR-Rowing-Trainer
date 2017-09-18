/*  Author: Benjamin Ferguson
    Date: 07/09/17
    Purpose: To load a new scene when switching between the menu and the training session,
	or to reload the current scene when the user wants to restart a session
*/
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

	// Used to load a scene with its index in the build settings
    public void LoadByIndex(int SceneIndex)
    {
        SceneManager.LoadScene(SceneIndex);
    }

	// Used to load the current scene when the user wants to restart a session
	public void LoadCurrentScene() 
	{
		string currentScene = SceneManager.GetActiveScene().name;
		SceneManager.LoadScene(currentScene);
	}

}
