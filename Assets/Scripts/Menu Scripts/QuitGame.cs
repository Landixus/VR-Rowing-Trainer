/*  Author: Benjamin Ferguson
    Date: 07/09/17
    Purpose: Allows the user to quit the application
*/
using UnityEngine;

public class QuitGame : MonoBehaviour {

	// Allows the user to quit the application
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
 
}
