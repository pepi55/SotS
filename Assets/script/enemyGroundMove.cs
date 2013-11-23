using UnityEngine;
using System.Collections;

public class enemyGroundMove :WalkingChar {

	private Animator anim;
	private bool facingLeft = true;
	void Start () {
		anim = GetComponent<Animator>();
	}

	void FixedUpdate () {
		//animation
		float speed = rigidbody2D.velocity.x;
		if(speed==0){
			anim.SetBool("walking",false);
		}else{
			anim.SetBool("walking",true);
		}
		//anim.SetFloat("Speed", Mathf.Abs(speed));

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
}
