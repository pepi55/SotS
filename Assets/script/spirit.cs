using UnityEngine;
using System.Collections;

public class spirit : WalkingChar {
	public float time;
	public float scaleStart = 0.5f;
	public float enemyHit;

	bool timing;
	bool inTrigger = false;
	bool scale = false;

	float countdown = 0;
	float distance;
	float deathTimer;

	Vector3 positionThis;
	Vector3 positionTarget;

	Transform target;
	GameObject spiritParticle;
	Animator anim;

	new void Start () {
		anim = GetComponent<Animator>();
		anim.speed = 2;
		StartTimer();
	}

	void Update () {
		if (timing) {
			if (enemyHit <= 0) {
				countdown -= Time.deltaTime / 2;
			} else {
				enemyHit -= Time.deltaTime;
				countdown -= (Time.deltaTime / 2) * 3;
			}

			transform.localScale = new Vector3(-1 * countdown * scaleStart, countdown * scaleStart, 1);
			if (countdown <= 2) {
				timing = false;
			} else {
				timing = true;
			}
		}

		if (GameObject.FindWithTag("spiritParticle")) {
			spiritParticle = GameObject.FindWithTag("spiritParticle");
			target = GameObject.FindWithTag("spiritParticle").transform;
			positionThis = transform.position;
			positionTarget = target.position;
			
			distance = Vector3.Distance(positionTarget, positionThis);

			if (distance < 3) {
				timing = false;
				scale = true;

				StartCoroutine(CoroutineScaleUp());

				Destroy(spiritParticle);
			}
		}

		if (countdown <= 2 && timing == false) {
			deathTimer += Time.deltaTime;
			if (deathTimer >= 5) {
				Application.LoadLevel(3);
			}
		} else {
			deathTimer = 0;
		}
	}

	IEnumerator CoroutineScaleUp () {
		if (scale) {
			countdown += Time.deltaTime * 100;
			transform.localScale = new Vector3(-1 * countdown * scaleStart, countdown * scaleStart, 1);

			yield return new WaitForSeconds(3f);

			scale = false;
			timing = true;
		}
	}

	void FixedUpdate () {
		ApplyMaxMoveSpeed();
		ApplySlowdown();
		if (inTrigger) {
			Walk(countdown - 2);
		}
		//animate
		anim.SetBool("walking",timing);
	}

	void StartTimer () {
		timing = true;
		countdown = time;
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.tag == "walkpath") {
			inTrigger = true;
		}

		if (col.tag == "tiki") {
			Application.LoadLevel(2);
		}
	}

	void OnTriggerExit2D (Collider2D col) {
		if (col.tag == "walkpath") {
			inTrigger = false;
		}
	}
}
