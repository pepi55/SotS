using UnityEngine;
using System.Collections;

public class follow : MonoBehaviour {
	public Transform target;

	private void Update(){
		if(target){
			gameObject.transform.position = target.position+ Vector3.up;
		}else{
			Debug.Log("no target found");
		}
	}
}
