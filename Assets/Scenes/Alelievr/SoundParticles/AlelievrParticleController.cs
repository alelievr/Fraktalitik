using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Random = UnityEngine.Random;

public enum ParticleControllerType
{
	Life,
	FireWorks,
	Bubble,
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
	
	float								originalEmission;
	float								originalStrength;

	new ParticleSystem					particleSystem;

    ParticleSystem.Particle[]			particles;

	Dictionary< ParticleControllerType, Action > particleControllers = new Dictionary< ParticleControllerType, Action >();

	void Start ()
	{
		particleSystem = GetComponent< ParticleSystem >();

		emission = particleSystem.emission;
		originalEmission = emission.rateOverTime.constant;

		noise = particleSystem.noise;
		originalStrength = noise.strength.constant;

		particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];

		//Init controllers:
		particleControllers[ParticleControllerType.FireWorks] = UpdateFireworks;
		particleControllers[ParticleControllerType.Life] = UpdateLife;
		particleControllers[ParticleControllerType.Bubble] = UpdateBubble;
	}
	
	void Update ()
	{
		smoothedThrottle = Mathf.SmoothDamp(smoothedThrottle, throttle, ref velocity, smoothTime, maxSpeed);

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

	void UpdateBubble()
	{
		float minDetectionPeak = .5f;

		if (throttle < minDetectionPeak)
			return ;

		int particleCount = particleSystem.GetParticles(particles);

		for (int i = 0; i < particleCount; i++)
			if (Random.value < Mathf.InverseLerp(minDetectionPeak, 3, throttle))
				particles[i].remainingLifetime = 0;
		
		particleSystem.SetParticles(particles, particleCount);
	}
}