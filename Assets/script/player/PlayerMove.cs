using UnityEngine;
using System.Collections;

public class PlayerMove : WalkingChar {
	private SpriteRenderer sprite;
	public int jumpForce = 800;
	public GameObject[] particles;
	public float groundCheckLenght;
	//attack
	public float attackAreaHeight = 1;
	public float attackAreaWidth = 2;
	public float attackX = .5f;
	private bool attackToDo = false;

	private bool canJump;
	private Vector3 rayDisplacment = new Vector3(0.4f,0,0);
	private bool grounded;
	private bool grdcheck1;
	private bool grdcheck2;
	private bool grdcheck3;
	private Transform dustSpawn;
	private Animator anim;
	private float animExitTimer;
	private GameObject hitObject;
	private Collider2D[] hitColliders;
	private float currentHorSpeed;
	
	new private void Start(){
		dustSpawn = transform.Find("dustSpawn");
		anim = GetComponent<Animator>();
		sprite =GetComponent<SpriteRenderer>();
		//call Start methode in the base class
		base.Start();

		base.spawnHealtBar();
	}
	
	private void Update (){
		bool spaceKey = Input.GetButtonDown("Jump");

		//check if can jump and if on ground
		Vector3 pos = transform.position;
		grdcheck1 = Physics2D.Linecast(transform.position,(pos+Vector3.down*groundCheckLenght),
		        (1 << LayerMask.NameToLayer("Level"))|(1 << LayerMask.NameToLayer("NPC")));
		grdcheck2 = Physics2D.Linecast(transform.position,((pos+rayDisplacment)+Vector3.down*groundCheckLenght),
				(1 << LayerMask.NameToLayer("Level"))|(1 << LayerMask.NameToLayer("NPC")));
		grdcheck3 = Physics2D.Linecast(transform.position,((pos-rayDisplacment)+Vector3.down*groundCheckLenght),
		        (1 << LayerMask.NameToLayer("Level"))|(1 << LayerMask.NameToLayer("NPC")));
		if(grdcheck1||grdcheck2||grdcheck3){
			grounded = true;
			if(spaceKey)
			{
				canJump = true; 
			}
		}else{
			grounded = false;
		}
		//draw ground check lines
		Debug.DrawLine(pos,transform.position+(Vector3.down*groundCheckLenght));
		Debug.DrawLine((pos+rayDisplacment),(pos+rayDisplacment)+(Vector3.down*groundCheckLenght));
		Debug.DrawLine((pos-rayDisplacment),(pos-rayDisplacment)+(Vector3.down*groundCheckLenght));
	}
	
	private void FixedUpdate () {
		currentHorSpeed = rigidbody2D.velocity.x;
		//get imput
		float h = InputSpeed;
		bool attackKey = Input.GetButton("Fire1");
		//move horizontal
		Walk(h);
		ApplyMaxMoveSpeed();
		applyXUpdate();
		//slowdown horizontal
		if(h==0){
			if(grounded){
				ApplySlowdown();
			}
		}

		if(canJump){
			//jump
			rigidbody2D.AddForce(new Vector2(0,jumpForce));
			animExitTimer = 0.1f;
			anim.SetBool ("jumping",true);
			anim.SetTrigger ("jump");
			canJump = false;
		}
		if(attackKey && animExitTimer < 0){
			attackStart();
		}
		if(attackToDo && animExitTimer<0.2f){
			attackHit();
		}

		if(animExitTimer < 0 && grounded){
			if(anim.GetBool("jumping")){
				anim.SetBool ("jumping",false);
			}
			anim.SetFloat("Speed",currentHorSpeed);
			float loclXScale = transform.localScale.x;
			if(InputSpeed>0&&loclXScale>0||InputSpeed<0&&loclXScale<0){
				if(currentHorSpeed>0.1f){
					Flip(false);
					anim.SetBool("walking",true);
				}else if (currentHorSpeed<-0.1f){
					Flip(true);
					anim.SetBool("walking",true);
				}else{
					anim.SetBool("walking",false);
				}
			}
		}else{
			if(anim.GetBool("jumping")){
				if(currentHorSpeed>0.1f){
					Flip(false);
				}else if (currentHorSpeed<-0.1f){
					Flip(true);
				}
			}
			animExitTimer -= Time.deltaTime;
		}
		SpawnParticles();
	}
	
	private void SpawnParticles(){
		if(grounded){
			int random = Random.Range(0,3);
			if(random==1){
				float speed = Mathf.Abs( rigidbody2D.velocity.x );
				Vector2 partSpeed = new Vector2(-rigidbody2D.velocity.x*2,0 );
				int randomPart = Random.Range(0,particles.Length);
				if(speed>1){
					GameObject newPart = GameObject.Instantiate(particles[randomPart],dustSpawn.position,Quaternion.identity) as GameObject;
					newPart.rigidbody2D.AddForce(partSpeed);
				}
			}
		}
	}

	private float InputSpeed{
		get{
			#if UNITY_IPHONE || UNITY_ANDROID
			Debug.Log ("android");
			if(Input.touchCount > 0 && Input.touchCount < 2){
				return ((Input.GetTouch(0).position.x /Screen.width)*2)-1;
			}else{
				return 0;
			}
			#else
			return Input.GetAxis("Horizontal");
			#endif
			
		}
	}

	private void attackStart(){
		anim.SetTrigger ("hit");
		animExitTimer = 0.75f;
		attackToDo = true;
	}

	private void attackHit(){
		attackToDo = false;
		bool foundHit = true;
		//	calculate hit area
		//while(foundHit){
		float hitLeftTopX = 0;
		float hitLeftTopY = transform.position.y + (attackAreaHeight/2);
		float hitBottomRightX = 0;
		float hitBottomRightY = transform.position.y -(attackAreaHeight/2);
		if(transform.localScale.x > 0){
			hitBottomRightX = transform.position.x - attackAreaWidth - attackX;
			hitLeftTopX = transform.position.x - attackX;
		}else if (transform.localScale.x < 0){
			hitBottomRightX = transform.position.x + attackAreaWidth + attackX;
			hitLeftTopX = transform.position.x + attackX;
		}
		Vector2 hitTopLeft = new Vector2(hitLeftTopX,hitLeftTopY);
		Vector2 hitBottomRight = new Vector2(hitBottomRightX,hitBottomRightY);
		//hit
		hitColliders = Physics2D.OverlapAreaAll(hitTopLeft,hitBottomRight,(1 << LayerMask.NameToLayer("attackColider")));
		for (int i = 0; i < hitColliders.Length; i++){
			GameObject hitObject = hitColliders[i].gameObject.transform.parent.gameObject;
			float hitObjPosX = hitObject.transform.position.x;
			float thisObjPosX = transform.position.x;
			float distHitObjX = thisObjPosX - hitObjPosX; 
			if(distHitObjX>0){
				//hitObject.rigidbody2D.AddForce(new Vector2(-1000,0));
				hitObject.GetComponent<WalkingChar>().applyXforceOverTime(-2500,0.9f);
			}else{
				//hitObject.rigidbody2D.AddForce(new Vector2(1000,0));
				hitObject.GetComponent<WalkingChar>().applyXforceOverTime(2500,0.9f);
			}
			hitObject.GetComponent<Living>().Hit();
		}
		//draw hit area
		if(true){
			if(transform.localScale.x > 0){
				Debug.DrawLine(transform.position + new Vector3(0-attackX,(attackAreaHeight/2),0),
				               transform.position + new Vector3(-attackAreaWidth-attackX,(attackAreaHeight/2),0),
				               Color.red,
				               1
				               );
				Debug.DrawLine(transform.position + new Vector3(0-attackX,-(attackAreaHeight/2),0),
				               transform.position + new Vector3(-attackAreaWidth-attackX,-(attackAreaHeight/2),0),
				               Color.red,
				               1
				               );
			}else if (transform.localScale.x < 0){
				Debug.DrawLine(transform.position + new Vector3(0+attackX,(attackAreaHeight/2),0),
				               transform.position + new Vector3(attackAreaWidth+attackX,(attackAreaHeight/2),0),
				               Color.red,
				               1
				               );
				Debug.DrawLine(transform.position + new Vector3(0+attackX,-(attackAreaHeight/2),0),
				               transform.position + new Vector3(attackAreaWidth+attackX,-(attackAreaHeight/2),0),
				               Color.red,
				               1
				               );
			}
		}
		//}
	}
}








