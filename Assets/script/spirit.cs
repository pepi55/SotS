using UnityEngine;
using System.Collections;

public class spirit : WalkingChar {
	public float time;

	bool timing;
	bool inTrigger = false;

	float countdown = 0;
	Vector2 diff;
	float distance;
	Transform target;

	new void Start () {
		target = GameObject.FindWithTag("spiritParticle").transform;
		diff = new Vector2(transform.position.x - target.position.x, transform.position.y - target.position.y); //(this.transform.position - target.position).magnitude;
		distance = Mathf.Sqrt(Mathf.Pow(diff.x, 2f) + Mathf.Pow(diff.y, 2f));
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

		Debug.Log(distance);
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
