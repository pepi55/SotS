using UnityEngine;
using System.Collections;

public class dustParticle : MonoBehaviour {
	
	public SpriteRenderer img;
	private float yForce;
	private float scaleX = 0;
	private float scaleY = 0;
	private float alpha = 1;
	private int lifeTime = 100;
	
	private void Start () {
		img.color = new Color(1f,1f,1f,alpha);
		transform.localScale = new Vector3(scaleX,scaleY,1);
	}
	
	private void FixedUpdate () {
		if(lifeTime<0){
			Destroy(gameObject);
		}
		lifeTime -= 1;
		scaleX += 0.01f;
		scaleY += 0.01f;
		alpha -= 0.01f;
		img.color = new Color(1f,1f,1f,alpha);
		transform.localScale = new Vector3(scaleX,scaleY,1);
		rigidbody2D.AddForce(new Vector2(0,yForce));
	}
}
