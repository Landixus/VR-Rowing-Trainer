using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This controls the speed and existance of the distant buoy object
//Distant buoys that are at a distance within 61 units of the player will change to the original version of the object
public class Distant : MonoBehaviour {

	//Gets the player's transform
    private Transform player;
	//Gets the transform of the buoy prefab
    public Transform buoy;
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
		//If the distant buoy is at a distance within 61 units of the player, it will change into the original buoy object that has buoyancy and a rigidbody
		//This is in place to improve the frame rate as the buoyancy code creates performance drops if used for too many objects
		if (Vector3.Distance(player.position, transform.position) <= 41.0f)
        {
            Instantiate(buoy, transform.position, transform.rotation, player);
            Destroy(gameObject);
        }

		//if the distant buoy is at a distance past 200 units, the instance will disappear
        if (Vector3.Distance(player.position, transform.position) > 200.0f)
        {
            Destroy(gameObject);
        }

		//This moves the distant buoy at a set speed
		speed = vp.video_playback;
		transform.position += transform.forward * Time.deltaTime * (float)speed;
	}
}
