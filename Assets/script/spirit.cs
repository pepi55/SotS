using UnityEngine;
using System.Collections;

public class spirit : WalkingChar {
	public float time;

	bool timing;
	bool inTrigger = false;

	Transform target;
	float countdown = 0;
	float distance;

	new void Start () {
		target = GameObject.FindWithTag("spirit").transform;
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

		distance = (target.transform.position - transform.position).magnitude;
		Debug.Log(distance);
	}

	void FixedUpdate () {
		ApplyMaxMoveSpeed();
		ApplySlowdown();
		if (inTrigger) {
			Walk(countdown);
		} else {
			//Walk(-1);
		}
		//print("In trigger: " + inTrigger);
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
