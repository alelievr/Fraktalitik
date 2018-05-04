using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMove : MonoBehaviour
{
	void Start ()
	{
		
	}

	void Update ()
	{
		float x = Mathf.PerlinNoise(Time.time, Time.time);
		float z = Mathf.PerlinNoise(Time.time, -Time.time);

		transform.Translate(new Vector3(x, 0, z));
	}
}
