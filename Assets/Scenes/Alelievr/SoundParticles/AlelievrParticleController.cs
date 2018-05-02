using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class AlelievrParticleController : MonoBehaviour
{
	public float	throttle { get; set; }
	
    ParticleSystem.EmissionModule		emission;
    ParticleSystem.ShapeModule			shape;
    ParticleSystem.NoiseModule			noise;
    ParticleSystem.ExternalForcesModule	force;
	
	float								originalEmission;
	float								originalStrength;

	new ParticleSystem					particleSystem;

	void Start ()
	{
		particleSystem = GetComponent< ParticleSystem >();

		emission = particleSystem.emission;
		originalEmission = emission.rateOverTime.constant;

		shape = particleSystem.shape;
		
		noise = particleSystem.noise;
		originalStrength = noise.strength.constant;

		force = particleSystem.externalForces;
	}
	
	void Update ()
	{
		if (throttle > .5f)
			particleSystem.Emit((int)(throttle * 10));

		noise.strength = throttle * originalStrength;
		noise.scrollSpeed = throttle;
	}
}
