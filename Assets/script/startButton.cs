using UnityEngine;
using System.Collections;

public class startButton : MonoBehaviour {
	void OnMouseDown () {
		Application.LoadLevel("level1");
	}
}