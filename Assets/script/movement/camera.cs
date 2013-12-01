using UnityEngine;
using System.Collections;

public class camera : MonoBehaviour
{
	public GameObject cameraTarget;
	
	public float smoothTime = 0.1f;
	public bool cameraFollowX = true;
	public bool cameraFollowY = true;
	public float cameraHeight = 2.5f;
	
	private Vector2 velocity;
	private Vector3 newPos;
	private float newXPos;
	private float newYPos;
	private Vector3 targetPos;
	
	public float maxXpos;
	public float minXpos;
	public float maxYpos;
	public float minYpos;
	
	void Update()
	{
		newPos = transform.position;
		targetPos = cameraTarget.transform.position;
		
		if (cameraFollowX)
		{
			newXPos = Mathf.SmoothDamp(newPos.x, targetPos.x, ref velocity.x, smoothTime);
		}
		if (cameraFollowY)
		{
			newYPos = Mathf.SmoothDamp(newPos.y, targetPos.y, ref velocity.y, smoothTime);
		}
		
		if(newXPos <maxXpos && newXPos > minXpos){
			//Update new X position
			newPos = new Vector3(newXPos,newPos.y,newPos.z);
		}
		if(newYPos <maxYpos && newYPos > minYpos){
			//Update new Y position
			newPos = new Vector3(newPos.x,newYPos,newPos.z);
		}
		//Update camera position
		transform.position = newPos;
	}
}
