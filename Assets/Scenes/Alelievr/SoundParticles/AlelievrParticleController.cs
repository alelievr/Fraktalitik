using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ParticleControllerType
{
	Life,
	FireWorks
}

[RequireComponent(typeof(ParticleSystem))]
public class AlelievrParticleController : MonoBehaviour
{
	public ParticleControllerType	controllerType;

	[Space]
	public float					smoothTime = 1;
	public float					maxSpeed = 5;

	[Space]
	public float					multiplier = 2;

	public float	throttle { get; set; }

	float			smoothedThrottle;
	float			velocity;
	
    ParticleSystem.EmissionModule		emission;
    ParticleSystem.NoiseModule			noise;
    ParticleSystem.ExternalForcesModule	force;
	
	float								originalEmission;
	float								originalStrength;
	float								originalScrollSpeed;

	new ParticleSystem					particleSystem;

	Dictionary< ParticleControllerType, Action > particleControllers = new Dictionary< ParticleControllerType, Action >();

	void Start ()
	{
		particleSystem = GetComponent< ParticleSystem >();

		emission = particleSystem.emission;
		originalEmission = emission.rateOverTime.constant;

		noise = particleSystem.noise;
		originalScrollSpeed = noise.scrollSpeed.constant;
		originalStrength = noise.strength.constant;

		force = particleSystem.externalForces;

		//Init controllers:
		particleControllers[ParticleControllerType.FireWorks] = UpdateFireworks;
		particleControllers[ParticleControllerType.Life] = UpdateLife;
	}
	
	void Update ()
	{
		smoothedThrottle = Mathf.SmoothDamp(smoothedThrottle, throttle, ref velocity, smoothTime, maxSpeed);

		Debug.Log("smoothedThrottle: " + smoothedThrottle);

		particleControllers[controllerType]();
	}

	void UpdateFireworks()
	{
		emission.rateOverTime = throttle * originalEmission;
	}

	void UpdateLife()
	{
		noise.strength = Mathf.Max(smoothedThrottle * originalStrength * multiplier, 1);
	}
}
