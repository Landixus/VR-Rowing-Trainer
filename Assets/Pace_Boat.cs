using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pace_Boat : MonoBehaviour {
    public const double pbspeed = 3.0; //speed of the pace boat in m/s
    public double playerspeed; //speed of the player boat in m/s
    public float deltaspeed; //difference in speed
    public Transform pb; //position of the pace boat

    // Use this for initialization
    void Start () {
		pb = GameObject.Find("pace_boat").GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        Get_Speed_Difference();
        Move_Pace_Boat();
	}

    public void Get_Speed_Difference()
    {
        playerspeed = Rowing_Speed.speed;
        deltaspeed = (float) (playerspeed - pbspeed);
    }

    public void Move_Pace_Boat()
    {
        pb.position += Vector3.right * deltaspeed;
    }
}
