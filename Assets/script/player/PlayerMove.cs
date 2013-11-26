using UnityEngine;
using System.Collections;

public class PlayerMove : WalkingChar {
	private SpriteRenderer sprite;
	public int jumpForce = 800;
	public GameObject[] particles;
	public float groundCheckLenght;
	private bool canJump;
	private Vector3 rayDisplacment = new Vector3(0.4f,0,0);
	private bool grounded;
	private bool grdcheck1;
	private bool grdcheck2;
	private bool grdcheck3;
	private Transform dustSpawn;
	private Animator anim;
	private float animExitTimer;
	
	new private void Start(){
		//dust.Emit(transform.position,Vector3.left,1,1,new Color(0.5F, 1, 0.5F, 1));
		dustSpawn = transform.Find("dustSpawn");
		anim = GetComponent<Animator>();
		sprite =GetComponent<SpriteRenderer>();
		//call Start methode in the base class
		base.Start();
		base.spawnHealtBar();
	}
	
	private void Update (){
		//bool spaceKey = Input.GetKey (KeyCode.Space);
		bool spaceKey = Input.GetButtonDown("Jump");

		//check if can jump
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
		Debug.DrawLine(pos,transform.position+(Vector3.down*groundCheckLenght));
		Debug.DrawLine((pos+rayDisplacment),(pos+rayDisplacment)+(Vector3.down*groundCheckLenght));
		Debug.DrawLine((pos-rayDisplacment),(pos-rayDisplacment)+(Vector3.down*groundCheckLenght));
	}
	
	private void FixedUpdate () {
		//get imput
		float h = Input.GetAxis("Horizontal");
		bool attackKey = Input.GetButton("Fire1");
		//move horizontal
		Walk(h);
		ApplyMaxMoveSpeed();
		//slowdown horizontal
		if(h==0){
			if(grounded){
				ApplySlowdown();
			}
		}
		
		//jump
		if(canJump){
			rigidbody2D.AddForce(new Vector2(0,jumpForce));
			canJump = false;
		}
		SpawnParticles();
		//animate
		if(animExitTimer < 0){
			if(attackKey){
				anim.SetTrigger ("hit");
				animExitTimer = 0.75f;
			}
			anim.SetFloat("Speed",rigidbody2D.velocity.x);
			float currentHorSpeed = rigidbody2D.velocity.x;
			if(currentHorSpeed>0.1f){
				Flip(false);
				anim.SetBool("walking",true);
			}else if (currentHorSpeed<-0.1f){
				Flip(true);
				anim.SetBool("walking",true);
			}else{
				anim.SetBool("walking",false);
			}
		}else{
			animExitTimer -= Time.deltaTime;
		}
	}

	private void OnCollisionEnter2D(Collision2D col){
		//Debug.Log( col.collider.collider2D.name);
	}
	
	private void SpawnParticles(){
		if(grounded){
			float speed = Mathf.Abs( rigidbody2D.velocity.x );
			Vector2 partSpeed = new Vector2(-rigidbody2D.velocity.x*10,0 );
			int randomPart = Random.Range(0,particles.Length);
			if(speed>1){
				GameObject newPart = GameObject.Instantiate(particles[randomPart],dustSpawn.position,Quaternion.identity) as GameObject;
				newPart.rigidbody2D.AddForce(partSpeed);
			}
		}
	}
}








