using UnityEngine;
using System.Collections;

public class spirit : WalkingChar {
	public float time;

	bool timing;
	bool inTrigger = false;

	float countdown = 0;
	float distance;

	Vector3 positionThis;
	Vector3 positionTarget;

	Transform target;
	GameObject spiritParticle;

	new void Start () {
		StartTimer();
	}

	void Update () {
		if (timing) {
			countdown -= Time.deltaTime / 2;

			transform.localScale = new Vector3(countdown, countdown + 2, 1);
			if (countdown <= 1) {
				timing = false;
			}
		}

		if (GameObject.FindWithTag("spiritParticle")) {
			spiritParticle = GameObject.FindWithTag("spiritParticle");
			target = GameObject.FindWithTag("spiritParticle").transform;
			positionThis = transform.position;
			positionTarget = target.position;
			
			distance = Vector3.Distance(positionTarget, positionThis);

			if (distance < 5) {
				countdown += 2;
				Destroy(spiritParticle);
			}
		}
	}

	void FixedUpdate () {
		ApplyMaxMoveSpeed();
		ApplySlowdown();
		if (inTrigger) {
			Walk(countdown);
		}
	}

	void StartTimer () {
		timing = true;
		countdown = time;
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.tag == "walkpath") {
			inTrigger = true;
		} 
	}

	void OnTriggerExit2D (Collider2D col) {
		if (col.tag == "walkpath") {
			inTrigger = false;
		}
	}
}
