using UnityEngine;
using System.Collections;

public class camera : MonoBehaviour
{
	
	public GameObject cameraTarget;
	public GameObject player;
	
	public float smoothTime = 0.1f;
	public bool cameraFollowX = true;
	public bool cameraFollowY = true;
	public bool cameraFollowHeight = false;
	public float cameraHeight = 2.5f;
	public Vector2 velocity;
	
	private Transform thisTransform;
	
	// Use this for initialization
	void Start()
	{
		thisTransform = transform;
	}
	
	// Update is called once per frame
	void Update()
	{
		if (cameraFollowX)
		{
			thisTransform.position = new Vector3(Mathf.SmoothDamp(thisTransform.position.x, cameraTarget.transform.position.x, ref velocity.x, smoothTime), thisTransform.position.y, thisTransform.position.z);
		}
		if (cameraFollowY)
		{
			thisTransform.position = new Vector3(thisTransform.position.x, Mathf.SmoothDamp(thisTransform.position.y, cameraTarget.transform.position.y, ref velocity.y, smoothTime), thisTransform.position.z);
		}
	}
}
