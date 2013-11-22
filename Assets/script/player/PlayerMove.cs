using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

	private bool canJump;
	public int jumpForce = 800;
	public int moveHorForce = 20;
	public int maxSpeed = 10;
	public float slowDownHorGround = 0.9f;
	bool grounded;

	void Update (){
		bool attack = Input.GetButton("Fire1");
		bool rayHit = false;
		bool spaceKey = Input.GetKey (KeyCode.Space);

		//attack

		//print(attack);
		//check if can jump
		grounded = Physics2D.Linecast(transform.position,(transform.position+Vector3.down*0.6f),
		           (1 << LayerMask.NameToLayer("Level"))|(1 << LayerMask.NameToLayer("NPC")));
		if(grounded){
			if(spaceKey)
			{
				rayHit = true; 
			}
		}
		Debug.DrawLine(transform.position,transform.position+(Vector3.down*0.6f));
		if(rayHit){
			canJump = true;
		}
	}
	int jumpNum;
	void FixedUpdate () {
		//get imput
		float h = moveHorForce*Input.GetAxis("Horizontal");
		//maxmove
		if(Mathf.Abs (rigidbody2D.velocity.x)> maxSpeed){
			rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
		}
		//slowdown horizontal
		if(h==0){
			if(grounded){
				rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x * slowDownHorGround, rigidbody2D.velocity.y);
			}
		}
		//move horizontal
		rigidbody2D.AddForce(new Vector2(h,0));

		//jump
		if(canJump){
			jumpNum++;
			Debug.Log("jump"+jumpNum);
			rigidbody2D.AddForce(new Vector2(0,jumpForce));
			canJump = false;
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		//Debug.Log( col.);
	}

}








