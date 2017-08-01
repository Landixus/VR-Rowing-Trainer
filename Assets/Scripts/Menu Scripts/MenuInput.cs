using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuInput : MonoBehaviour {

    public EventSystem eventSystem;
    public GameObject selectedObject;
	private Button selectedButton;
	public bool pointerOver;

	private StandaloneInputModule sim;
    private bool buttonSelected;
	
	private void Start() 
	{
		sim = eventSystem.GetComponent<StandaloneInputModule>();
		//Debug.Log(sim.input);
	}
	
	void Update()
    {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		Debug.DrawRay(ray.origin, ray.direction * 5, Color.red);
		if(Physics.Raycast(ray.origin, ray.direction, out hit, 10f)) {
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

		if(buttonSelected && Input.GetMouseButtonDown(0)) {
			selectedButton.onClick.Invoke();
		}


		if (Input.GetAxisRaw("Vertical") != 0 && buttonSelected == false)
        {
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

    public void onDisable()
    {
	
        buttonSelected = false;
    }
}
