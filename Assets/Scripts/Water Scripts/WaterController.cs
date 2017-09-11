using UnityEngine;
using System.Collections;

//Controls the water
public class WaterController : MonoBehaviour
{
	//Sets the renderer component to a variable so that this script can access the water's texture UVW offset
	private Renderer rend;

    void Start()
    {
		rend = GetComponent<Renderer>();
	}

	void Update() {
		float newOffset = 0.2f * Time.time;
		rend.material.SetTextureOffset("_MainTex", new Vector2(0.0f, newOffset));
	}
}