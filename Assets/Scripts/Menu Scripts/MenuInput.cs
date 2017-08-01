using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuInput : MonoBehaviour {

    public EventSystem eventSystem;
    public GameObject selectedObject;

    public bool buttonSelected;

    void Update()
    {
        if (Input.GetAxisRaw("Vertical") != 0 && buttonSelected == false)
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
        }    
    }

    public void onDisable()
    {
		Debug.Log("Button Unselected");
        buttonSelected = false;
    }
}
