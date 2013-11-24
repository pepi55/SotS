using UnityEngine;
using System.Collections;

public class PlayerMove : WalkingChar {
	private bool canJump;
	public int jumpForce = 800;
	public GameObject[] particles;
	Vector3 rayDisplacment = new Vector3(0.4f,0,0);
	bool grounded;
	bool grdcheck1;
	bool grdcheck2;
	bool grdcheck3;
	
	void Start(){
		//dust.Emit(transform.position,Vector3.left,1,1,new Color(0.5F, 1, 0.5F, 1));
	}
	
	void Update (){
		bool attack = Input.GetButton("Fire1");
		//bool spaceKey = Input.GetKey (KeyCode.Space);
		bool spaceKey = Input.GetButtonDown("Jump");
		//attack

		//print(attack);
		//check if can jump
		Vector3 pos = transform.position;
		grdcheck1 = Physics2D.Linecast(transform.position,(pos+Vector3.down*0.6f),
		        (1 << LayerMask.NameToLayer("Level"))|(1 << LayerMask.NameToLayer("NPC")));
		grdcheck2 = Physics2D.Linecast(transform.position,((pos+rayDisplacment)+Vector3.down*0.6f),
				(1 << LayerMask.NameToLayer("Level"))|(1 << LayerMask.NameToLayer("NPC")));
		grdcheck3 = Physics2D.Linecast(transform.position,((pos-rayDisplacment)+Vector3.down*0.6f),
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
		Debug.DrawLine(pos,transform.position+(Vector3.down*0.6f));
		Debug.DrawLine((pos+rayDisplacment),(pos+rayDisplacment)+(Vector3.down*0.6f));
		Debug.DrawLine((pos-rayDisplacment),(pos-rayDisplacment)+(Vector3.down*0.6f));
	}
	
	int jumpNum;
	
	void FixedUpdate () {
		//get imput
		float h = Input.GetAxis("Horizontal");
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
			jumpNum++;
			//Debug.Log("jump"+jumpNum);
			rigidbody2D.AddForce(new Vector2(0,jumpForce));
			canJump = false;
		}
		SpawnParticles();
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
				GameObject newPart = GameObject.Instantiate(particles[randomPart],transform.position,Quaternion.identity) as GameObject;
				newPart.rigidbody2D.AddForce(partSpeed);
			}
		}
	}

}








