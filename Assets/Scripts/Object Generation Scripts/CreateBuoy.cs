using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBuoy : MonoBehaviour {

    public Transform buoyObj;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.A))
        {
            Instantiate(buoyObj, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
	}
}
