using UnityEngine;
using System.Collections;

public class dustParticle : MonoBehaviour {
	
	public SpriteRenderer img;
	private float maxScale;
	private float scaleX = 0;
	private float scaleY = 0;
	private float alpha = 0;
	private float xFlip;

	private float[] Delta = {0.5f,-0.05f};
	private int state = 0;
	private float deltaAlpha;
	
	private void Start () {
		maxScale = Random.Range(1.0f,2.5f);
		img.color = new Color(1f,1f,1f,alpha);
		transform.localScale = new Vector3(scaleX,scaleY,1);
		deltaAlpha = Delta[state]/maxScale;
		if(rigidbody2D.velocity.x >0){
			xFlip = 1;
			Debug.Log("scale");
		}else{
			xFlip = -1;
		}
	}
	
	private void FixedUpdate () {
		if(state==0 && scaleX>maxScale ){
			state++;
			deltaAlpha = Delta[state]/maxScale;
		}
		else if(scaleX<0){
			Destroy(gameObject);
		}
		scaleX += Delta[state];
		scaleY += Delta[state];
		alpha += deltaAlpha;
		img.color = new Color(1f,1f,1f,alpha);
		transform.localScale = new Vector3(scaleX*xFlip,scaleY,1);
	}
}