using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {
	public GameObject player;
	public GameObject[] backgrounds;

	private int bgLength;
	private float[] bgParallaxValue;
	private float moveDelay = 0.25f;
	private float moveSpeed;

	// Use this for initialization
	void Start () {
		bgLength = backgrounds.Length;
		bgParallaxValue = new float[bgLength];

		for (int i = 0; i < bgLength; i++) {
			bgParallaxValue[i] = backgrounds[i].GetComponent<Values>().parallaxValue;
		}
	}
	
	// Update is called once per frame
	void LateUpdate () {
		float targetX;
		float targetY;
		float thisX = transform.position.x;
		float deltaX = targetX - thisX;

		moveSpeed = moveDelay * Mathf.Pow(Time.deltaTime, 0.2f);
	}
}
