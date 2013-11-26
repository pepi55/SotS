using UnityEngine;
using System.Collections;

public class enemyGroundMove :WalkingChar {
	private SpriteRenderer sprite;
	private Animator anim;
	private bool facingLeft = true;
	private Transform playerTrans;
	private GameObject player;
	private int agroDistance = 5;
	private Object hitObject;
	public bool walkLeft = true;
	float hitExitTimer = 0;
	bool hitForceToApply = false;
	new private void Start () {
		sprite =GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
		playerTrans = GameObject.FindWithTag ("Player").transform;
		player = GameObject.FindGameObjectWithTag("Player");
		base.Start();
		base.spawnHealtBar();
	}

	private void FixedUpdate () {
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
				hitObject = Physics2D.OverlapCircle(rigidbody2D.transform.position,1,(1 << LayerMask.NameToLayer("Player")));
				//hitObject. .AddForce(new Vector2(transform.position.x-playerTrans.position.x),0);
				if(hitObject){
					hitForceToApply = true;
				}
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
		//apply hitforce player
		if(hitExitTimer < 0.4){
			if(hitForceToApply){
				hitForceToApply = false;
				if(hitObject.name=="Player"){
					player.GetComponent<PlayerMove>().ChangeHealt(-5);
					if(xDistance>0){
						player.rigidbody2D.AddForce(new Vector2(500,0));
					}else{
						player.rigidbody2D.AddForce(new Vector2(-500,0));
					}
				}
			}
		}

		ApplySlowdown();
		ApplyMaxMoveSpeed();
	}

	private void Flip(bool left){
		Vector3 newScale = sprite.transform.localScale;
		if(left){
			newScale.x = 1;
		}else{
			newScale.x = -1;
		}
		sprite.transform.localScale = newScale;
	}
	
	private void OnTriggerEnter2D(Collider2D col){
		//Debug.Log( col.collider.collider2D.name);
		if(col.name == "enemyBariar"){
			walkLeft = !walkLeft;
		}
	}
	private void OnCollisionEnter2D(Collision2D col){
		if(col.collider.collider2D.tag == "enemy"){
			walkLeft = !walkLeft;
		}
	}
}
