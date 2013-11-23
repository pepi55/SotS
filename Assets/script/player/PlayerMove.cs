using UnityEngine;
using System.Collections;

public class PlayerMove : WalkingChar {

	private bool canJump;
	public int jumpForce = 800;
	public int moveHorForce = 20;
	Vector3 rayDisplacment = new Vector3(0.4f,0,0);
	bool grounded;
	bool grounded2;
	bool grounded3;

	void Update (){
		bool attack = Input.GetButton("Fire1");
		bool rayHit = false;
		//bool spaceKey = Input.GetKey (KeyCode.Space);
		bool spaceKey = Input.GetButtonDown("Jump");
		//attack

		//print(attack);
		//check if can jump
		Vector3 pos = transform.position;
		grounded = Physics2D.Linecast(transform.position,(pos+Vector3.down*0.6f),
		           (1 << LayerMask.NameToLayer("Level"))|(1 << LayerMask.NameToLayer("NPC")));

		grounded2 = Physics2D.Linecast(transform.position,((pos+rayDisplacment)+Vector3.down*0.6f),
		                              (1 << LayerMask.NameToLayer("Level"))|(1 << LayerMask.NameToLayer("NPC")));

		grounded3 = Physics2D.Linecast(transform.position,((pos-rayDisplacment)+Vector3.down*0.6f),
		                              (1 << LayerMask.NameToLayer("Level"))|(1 << LayerMask.NameToLayer("NPC")));
		if(grounded||grounded2||grounded3){
			if(spaceKey)
			{
				rayHit = true; 
			}
		}
		Debug.DrawLine(pos,transform.position+(Vector3.down*0.6f));
		Debug.DrawLine((pos+rayDisplacment),(pos+rayDisplacment)+(Vector3.down*0.6f));
		Debug.DrawLine((pos-rayDisplacment),(pos-rayDisplacment)+(Vector3.down*0.6f));
		if(rayHit){
			canJump = true;
		}
	}
	int jumpNum;
	void FixedUpdate () {
		//get imput
		float h = moveHorForce*Input.GetAxis("Horizontal");

		ApplyMaxMoveSpeed();
		//slowdown horizontal
		if(h==0){
			if(grounded){
				ApplySlowdown();
			}
		}
		//move horizontal
		rigidbody2D.AddForce(new Vector2(h,0));

		//jump
		if(canJump){
			jumpNum++;
			//Debug.Log("jump"+jumpNum);
			rigidbody2D.AddForce(new Vector2(0,jumpForce));
			canJump = false;
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		//Debug.Log( col.collider.collider2D.name);
	}

}








