using UnityEngine;
using System.Collections;

public class floatUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		rigidbody2D.AddForce(new Vector2(0,30));
	}
}
