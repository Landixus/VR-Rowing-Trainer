
using UnityEngine;

/**
 *
 * TrackingData struct, written by X.Hunt
 * 
 * Stores all the data that we need, for the 4Hz data collection
 * 
 */

[System.Serializable]
public struct TrackingData {

	public double timestamp;

	public Vector3 pos;
	public Vector3 worldPos;
	public Vector3 rot;

	/*
	public float posx;
	public float posy;
	public float posz;
	public float worldx;
	public float worldy;
	public float worldz;
	public float rotx;
	public float roty;
	public float rotz;
	*/

	public uint distance;
	public double power;
	public uint stroke;
	public uint looks;
}

[System.Serializable]
public struct PowerData {

	public double timestamp;
	public double power;
}