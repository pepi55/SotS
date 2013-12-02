using UnityEngine;
using System.Collections;

public class returnToStart : MonoBehaviour {
	void OnMouseDown () {
		Application.LoadLevel(0);
	}
}
