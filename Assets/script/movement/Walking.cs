
using UnityEngine;
using System.Collections;

public class WalkingChar :Living
{
	public float slowDownHorGround = 0.1f;
	public int maxSpeed = 4;
	public int moveHorForce = 20;
	
	private float xForceToApply = 0;
	private float updatesToApplyForce = 0;

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
	
	public void Flip(bool left){
		Vector3 newScale = transform.localScale;
		if(left){
			newScale.x = 1;
		}else{
			newScale.x = -1;
		}
		transform.localScale = newScale;
	}
	
	public void applyXforceOverTime(float force,float updates){
		xForceToApply += force;
		updatesToApplyForce = updates;
	}
	
	public void applyXUpdate(){
		if(xForceToApply>0||xForceToApply<0){
			float xForce = Mathf.Lerp(xForceToApply,0,updatesToApplyForce);
			xForceToApply -= xForce;
			updatesToApplyForce -=0.02f;
			rigidbody2D.AddForce( new Vector2( xForce,0));
		}
	}
}

