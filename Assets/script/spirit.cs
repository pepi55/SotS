using UnityEngine;
using System.Collections;

public class spirit : WalkingChar {
	public float time;
	public float scaleStart = 0.5f;

	bool timing;
	bool inTrigger = false;
	bool scale = false;

	float countdown = 0;
	float distance;

	Vector3 positionThis;
	Vector3 positionTarget;

	Transform target;
	GameObject spiritParticle;
	Animator anim;

	new void Start () {
		anim = GetComponent<Animator>();
		StartTimer();
	}

	void Update () {
		if (timing) {
			countdown -= Time.deltaTime / 2;

			transform.localScale = new Vector3(-1 * countdown * scaleStart, countdown * scaleStart, 1);
			if (countdown <= 1) {
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
	}

	IEnumerator CoroutineScaleUp () {
		if (scale) {
			countdown += Time.deltaTime * 10;
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
			Walk(countdown);
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
	}

	void OnTriggerExit2D (Collider2D col) {
		if (col.tag == "walkpath") {
			inTrigger = false;
		}
	}
}
