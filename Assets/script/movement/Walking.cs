
using UnityEngine;
using System.Collections;

public class WalkingChar :MonoBehaviour
{
	public float slowDownHorGround = 0.1f;
	public int maxSpeed = 4;
	public int moveHorForce = 20;

	//slowdown
	public void ApplySlowdown ()
	{
		rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x * slowDownHorGround, rigidbody2D.velocity.y);
	}

	// apply max move
	public void ApplyMaxMoveSpeed(){
		if(Mathf.Abs (rigidbody2D.velocity.x)> maxSpeed){
			rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
		}
	}
	
	public void Walk(float s){
		rigidbody2D.AddForce(new Vector2(s*moveHorForce,0));
	}
}

