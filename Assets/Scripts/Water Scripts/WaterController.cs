using UnityEngine;
using System.Collections;

//Controls the water
public class WaterController : MonoBehaviour
{
	//Sets the renderer component to a variable so that this script can access the water's texture UVW offset
	private Renderer rend;

	//Gets the video playback speed to set the speed of the current
	private Video_Playback vp;

    //River ripple speed
    private float speed = 0;

    void Start()
    {
		rend = GetComponent<Renderer>();
		vp = GameObject.Find("SceneController").GetComponent<Video_Playback>();
	}

	void Update() {
		//speed = (float)vp.video_playback;
		float newOffset = -0.2f * Time.time;
		rend.material.SetTextureOffset("_MainTex", new Vector2(0.0f, newOffset));
	}
}