using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This controls the speed and existance of the buoy object
//Buoys that are at a distance more than 61 units away from the player will change to the distant version of the object
public class Buoy : MonoBehaviour {

	//Gets the player's transform
    private Transform player;
	//Gets the transform of the distant buoy prefab
    public Transform distantBuoy;
	//Gets the video playback speed to set the speed of the buoys
	private Video_Playback vp;
	//Sets speed of buoys
	public double speed;

	// Use this for initialization
	void Start()
    {
		player = GameObject.Find("ScenePos").GetComponent<Transform>();
		vp = GameObject.Find("SceneController").GetComponent<Video_Playback>();
	}

    // Update is called once per frame
    void Update()
    {
		//If the distance between the player and the buoy exceeds 61 units, the buoy will change into one that is without buoyancy and a rigidbody
		//This is in place to improve the frame rate as the buoyancy code creates performance drops if used for too many objects
		if (Vector3.Distance(player.position, transform.position) > 41.0f)
        {
            Instantiate(distantBuoy, transform.position, transform.rotation, player);
            Destroy(gameObject);
        }

		//This moves the distant buoy at a set speed
		speed = vp.video_playback;
		transform.position += transform.forward * Time.deltaTime * (float)speed;
	}
}
