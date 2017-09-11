/*  Author: Benjamin Ferguson
    Date: 07/09/17
    Purpose: An edited version of don't destroy to avoid creating multiple scene data
	manager objects, allowing the user to go between scenes without creating errors.
*/
using UnityEngine;

public class DontDestroy : MonoBehaviour {

	private static bool created = false; // Used to check if the scene data manager object already exists

    private void Awake()
    {
		// If it's the first time the scene has loaded it sets the object to not be destroyed
		// If its not the first time the original object stays intact while the newly created
		// object gets destroyed, avoiding having multiple of the same object
		if (!created) {
			DontDestroyOnLoad(this.gameObject);
			created = true;
		}
		else {
			Destroy(this.gameObject);
		}
        
    }
}
