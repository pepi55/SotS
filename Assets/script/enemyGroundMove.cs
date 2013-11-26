using UnityEngine;
using System.Collections;

public class enemyGroundMove :WalkingChar {

	private Animator anim;
	private bool facingLeft = true;
	private Transform playerTrans;
	private GameObject player;
	private int agroDistance = 5;
	
	public bool walkLeft = true;
	float hitExitTimer = 0;
	void Start () {
		anim = GetComponent<Animator>();
		playerTrans = GameObject.FindWithTag ("Player").transform;
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void FixedUpdate () {
	    // get distance player
		float distPlayer = Vector3.Distance(playerTrans.position, transform.position);
		float xDistance = playerTrans.position.x - transform.position.x;
		//animation
		float speed = rigidbody2D.velocity.x;
		if(speed==0){
			anim.SetBool("walking",false);
		}else{
			anim.SetBool("walking",true);
		}


		//flip check
		if(distPlayer>agroDistance){
			if(speed>0.1f && facingLeft){
				facingLeft = false;
				Flip(false);
			}
			if (speed<-0.1f && !facingLeft){
				facingLeft = true;
				Flip(true);
			}
		}else{
			if(playerTrans.position.x>transform.position.x){
				facingLeft = false;
				Flip(false);
			}else if(playerTrans.position.x<transform.position.x){
				facingLeft = true;
				Flip(true);
			}
		}

		//movement
		if (hitExitTimer < 0) {
			if (distPlayer > agroDistance) {
				if (walkLeft) {
					Walk (1);
				} else {
					Walk (-1);
				}
			} else if (xDistance < 1.0f && xDistance > -1.0f && distPlayer < 2) {
				anim.SetTrigger ("hit");
				hitExitTimer = 0.75f;
				Object hitObject = Physics2D.OverlapCircle(rigidbody2D.transform.position,1,(1 << LayerMask.NameToLayer("Player")));
				if(hitObject){
					if(hitObject.name=="Player"){
						if(xDistance>0){
							player.rigidbody2D.AddForce(new Vector2(500,0));
						}else{
							player.rigidbody2D.AddForce(new Vector2(-500,0));
						}
					}
				}
				//hitObject. .AddForce(new Vector2(transform.position.x-playerTrans.position.x),0);
				Debug.DrawLine(transform.position,transform.position+new Vector3(1,0,0));
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

	void OnDestroy () {
		Resources.Load("spiritParticle");
	}
}
