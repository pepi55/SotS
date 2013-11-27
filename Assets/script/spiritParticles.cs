using UnityEngine;
using System.Collections;

public class spiritParticles : MonoBehaviour {
	private Transform target;
	public float maxSpeed = 10;
	public float mass = 20;

	// Use this for initialization
	void Start () {
		rigidbody2D.velocity = new Vector2(0, 1) * maxSpeed;
		target = GameObject.FindWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		Seek();
	}

	void Seek () {
		float followSpeedY = target.position.y - rigidbody2D.transform.position.y;
		float followSpeedX = target.position.x - rigidbody2D.transform.position.x;

		Vector2 followSpeed = new Vector2(followSpeedX, followSpeedY);
		Vector2 velocity = followSpeed * maxSpeed;
		Vector2 steering = velocity - rigidbody2D.velocity;

		followSpeed.Normalize();
		rigidbody2D.velocity = rigidbody2D.velocity + steering / mass;
	}
}