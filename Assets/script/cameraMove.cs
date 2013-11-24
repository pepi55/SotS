using UnityEngine;
using System.Collections;

public class cameraMove : MonoBehaviour {
	
	private Transform playerTrans;
	// Use this for initialization
	void Start () {
		playerTrans = GameObject.FindWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		float currentPositionX = transform.position.x;
		float currentPositionY = transform.position.y;
		float playerPositionX = playerTrans.position.x;
		float playerPositionY = playerTrans.position.y;
		float deltaX = playerPositionX - currentPositionX;
		float deltaY = playerPositionY - currentPositionY;
		Vector3 move = new Vector3 (deltaX*Mathf.Pow(Time.deltaTime,0.1f),deltaY*Mathf.Pow(Time.deltaTime,0.1f),0);
		//Debug.Log((move));
		transform.Translate(move);
	}
}
