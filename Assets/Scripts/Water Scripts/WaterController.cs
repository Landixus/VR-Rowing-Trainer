using UnityEngine;
using System.Collections;

//Controls the water
public class WaterController : MonoBehaviour
{
    public static WaterController current;

	private Renderer rend;

    public bool isMoving;

    //River ripple speed
    public float speed = 0.5f;

    void Start()
    {
        current = this;
		rend = GetComponent<Renderer>();
    }

	void Update() {
		float newOffset = speed * Time.time;
		rend.material.SetTextureOffset("_MainTex", new Vector2(0.0f, newOffset));
	}

	//Return the height of the water
	public float GetWaveYPos()
	{
		return transform.position.y;
	}

    //Find the distance from a vertice to water
    //Make sure the position is in global coordinates
    //Positive if above water
    //Negative if below water
    public float DistanceToWater(Vector3 position)
    {
		float waterHeight = GetWaveYPos();

        float distanceToWater = position.y - waterHeight;

        return distanceToWater;
    }
}