﻿using UnityEngine;
using System.Collections;

public class spiritParticles : MonoBehaviour {
	private Transform target;
	public float maxSpeed = 10;
	public float mass = 20;

	// Use this for initialization
	void Start () {
		rigidbody.velocity	=	new Vector3(0,1,1) * maxSpeed;
		target = GameObject.FindWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		Seek();
	}

	void Seek () {
		Vector3 followSpeed = target.position - rigidbody.position;
		Vector3 velocity = followSpeed * maxSpeed;
		Vector3 steering = velocity - rigidbody.velocity;

		followSpeed.Normalize();
		rigidbody.velocity = rigidbody.velocity + steering / mass;
	}
}