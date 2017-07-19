using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pace_Boat : MonoBehaviour {
    public double pbspeed; //speed of the pace boat in m/s
    public double playerspeed; //speed of the player boat in m/s
    public float deltaspeed; //difference in speed
    public Transform pb; //position of the pace boat
    public GetTargetSpeed ts; //
        

    // Use this for initialization
    void Start () {
        pbspeed = (double) ts.TargetSpeed();
    }
	
	// Update is called once per frame
	void Update () {
        Get_Speed_Difference();
        Move_Pace_Boat();
	}

    public void Get_Speed_Difference()
    {
        //playerspeed = Rowing_Speed.speed;
        deltaspeed = (float) (playerspeed - pbspeed);
    }

    public void Move_Pace_Boat()
    {
        pb.position += Vector3.forward * deltaspeed;
    }
}
