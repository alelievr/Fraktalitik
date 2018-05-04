using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMove : MonoBehaviour
{
	public float		speed = .5f;

	void Start ()
	{
		
	}

	void Update ()
	{
		float t = Time.time * speed;

		transform.Translate(new Vector3(Mathf.Sin(t), 0, Mathf.Cos(t)) * .1f);
	}
}
