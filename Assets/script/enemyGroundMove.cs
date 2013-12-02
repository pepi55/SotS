using UnityEngine;
using System.Collections;

public class enemyGroundMove :WalkingChar {
	public AudioClip attackSound;

	private SpriteRenderer sprite;
	private Animator anim;
	private bool facingLeft = true;
	private Transform playerTrans;
	private GameObject player;
	private Transform spiritTrans;
	private GameObject spirit;
	private int agroDistance = 5;
	private Object hitObject;
	public bool walkLeft = true;
	private float hitExitTimer = 0;
	private bool hitForceToApply = false;
	private bool hitAudioToPlay = false;
	new private void Start () {
		sprite =GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
		playerTrans = GameObject.Find("Player").transform;
		player = GameObject.Find("Player");
		spiritTrans = GameObject.Find("spirit").transform;
		spirit = GameObject.Find("spirit");

		base.Start();
		base.spawnHealtBar();
	}

	private void FixedUpdate () {
	    // get distance player
		float distPlayer = Vector3.Distance(playerTrans.position, transform.position);
		float xDisPlayer = playerTrans.position.x - transform.position.x;
		//get distance spirit
		float distSpirit = Vector3.Distance(spiritTrans.position, transform.position);
		float xDisSpirit = spiritTrans.position.x - transform.position.x;

		float distTarget;
		float xDistTarget;
		GameObject target;
		Transform targetTrans;

		if(distPlayer<distSpirit){
			distTarget = distPlayer;
			xDistTarget = xDisPlayer;
			target = player;
			targetTrans = playerTrans;
		}else{
			distTarget = distSpirit;
			xDistTarget = xDisSpirit;
			target = spirit;
			targetTrans = spiritTrans;
		}

		//animation
		float speed = rigidbody2D.velocity.x;
		if(speed==0){
			anim.SetBool("walking",false);
		}else{
			anim.SetBool("walking",true);
		}

		//flip check
		if(distTarget>agroDistance){
			if(speed>0.1f && facingLeft){
				facingLeft = false;
				Flip(false);
			}
			if (speed<-0.1f && !facingLeft){
				facingLeft = true;
				Flip(true);
			}
		}else{
			if(targetTrans.position.x>transform.position.x){
				facingLeft = false;
				Flip(false);
			}else if(targetTrans.position.x<transform.position.x){
				facingLeft = true;
				Flip(true);
			}
		}
		float hitdistance;
		if(target.name == player.name){
			hitdistance = 1;
		}else{
			hitdistance = 2;
		}

		//movement
		if (hitExitTimer < 0) {
			if (distTarget > agroDistance) {
				if (walkLeft) {
					Walk (1);
				} else {
					Walk (-1);
				}
			} else if (xDistTarget < hitdistance && xDistTarget > -hitdistance && distTarget < 2*hitdistance) {
				anim.SetTrigger ("hit");
				hitExitTimer = 0.75f;
				hitObject = Physics2D.OverlapCircle(rigidbody2D.transform.position,1,(1 << LayerMask.NameToLayer("Player")));
				//hitObject. .AddForce(new Vector2(transform.position.x-targetTrans.position.x),0);
				hitAudioToPlay = true;
				if(hitObject){
					hitForceToApply = true;
				}
				Debug.DrawLine(transform.position,transform.position+new Vector3(1,0,0));
			} else {
				if (targetTrans.position.x > transform.position.x) {
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
			if(hitAudioToPlay){
				audio.PlayOneShot(attackSound);
				hitAudioToPlay = false;
			}
			if(hitForceToApply){
				hitForceToApply = false;
				if(hitObject.name=="spirit"){
					spirit.GetComponent<spirit>().enemyHit += 30;
				}
				if(hitObject.name=="Player"){
					player.GetComponent<PlayerMove>().ChangeHealt(-5);
					if(xDistTarget>0){
						player.GetComponent<WalkingChar>().applyXforceOverTime(4000,1);
					}else{
						player.GetComponent<WalkingChar>().applyXforceOverTime(-4000,1);
					}
				}
			}
		}

		ApplySlowdown();
		ApplyMaxMoveSpeed();
		applyXUpdate();

		if(GetComponent<Living>().Health<0){
			GameObject.Instantiate( Resources.Load("spiritParticle"),transform.position,Quaternion.identity);
			GameObject deathEffect = (GameObject)GameObject.Instantiate( Resources.Load("deathSmall"),transform.position,Quaternion.identity);
			deathEffect.rigidbody2D.AddForce(new Vector2(0,150));
			GameObject.Destroy(gameObject);
		}
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
