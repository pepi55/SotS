using UnityEngine;
using System.Collections;

public class enemyGroundMove :WalkingChar {

	private Animator anim;
	private bool facingLeft = true;
	private Transform playerTrans;
	private int agroDistance = 10;
	
	public bool walkLeft = true;
	float hitExitTimer = 0;
	void Start () {
		anim = GetComponent<Animator>();
		playerTrans = GameObject.FindWithTag ("Player").transform;
	}

	void FixedUpdate () {
	    // get distance player
		float distPlayer = Vector3.Distance(playerTrans.position, transform.position);
		//animation
		float speed = rigidbody2D.velocity.x;
		if(speed==0){
			anim.SetBool("walking",false);
		}else{
			anim.SetBool("walking",true);
		}

		//flip check
		if(speed>0.1f && facingLeft){
			facingLeft = false;
			Flip(false);
		}
		if (speed<-0.1f && !facingLeft){
			facingLeft = true;
			Flip(true);
		}
		//movement
		if (hitExitTimer < 0) {
			if (distPlayer > 5) {
				if (walkLeft) {
					Walk (1);
				} else {
					Walk (-1);
				}
			} else if (distPlayer < 0.8f) {
				anim.SetTrigger ("hit");
				hitExitTimer = 0.75f;
			} else {
				if (playerTrans.position.x > transform.position.x) {
					Walk (3.5f);
				} else {
					Walk (-3.5f);
				}
			}
		} else {
			hitExitTimer-=Time.deltaTime;
		}
		ApplySlowdown();
		ApplyMaxMoveSpeed();
	}

	void Flip(bool left){
		Vector3 newScale = transform.localScale;
		if(left){
			newScale.x = 1;
		}else{
			newScale.x = -1;
		}
		transform.localScale = newScale;
	}
	
	void OnTriggerEnter2D(Collider2D col){
		//Debug.Log( col.collider.collider2D.name);
		if(col.name == "enemyBariar"){
			walkLeft = !walkLeft;
		}
	}
	void OnCollisionEnter2D(Collision2D col){
		if(col.collider.collider2D.tag == "enemy"){
			walkLeft = !walkLeft;
		}
	}
}
