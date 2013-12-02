using UnityEngine;
using System.Collections;

public class dustParticle : MonoBehaviour {
	
	private SpriteRenderer img;
	public float minMaxScale = 1.0f;
	public float maxMaxScale = 2.5f;
	private float Scale;
	private float scaleX = 0;
	private float scaleY = 0;
	private float alpha = 0;
	private float xFlip = 1;

	public float[] Delta = {0.5f,-0.05f};
	private int state = 0;
	private float deltaAlpha;
	
	private void Start () {
		img = GetComponent<SpriteRenderer>();
		Scale = Random.Range(minMaxScale,maxMaxScale);
		img.color = new Color(1f,1f,1f,alpha);
		transform.localScale = new Vector3(scaleX,scaleY,1);
		deltaAlpha = Delta[state]/Scale;
		if(rigidbody2D.velocity.x <0){
			xFlip = -1;
		}
	}
	
	private void FixedUpdate () {
		if(state==0 && scaleX>Scale ){
			state++;
			deltaAlpha = Delta[state]/Scale;
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