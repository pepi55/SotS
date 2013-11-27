using UnityEngine;
using System.Collections;

public class follow : MonoBehaviour {
	public Transform target;

	private void Update(){
		if(target){
			gameObject.transform.position = target.position+ Vector3.up+new Vector3(-0.5f,0,0);
		}else{
			Debug.Log("no target found");
		}
	}
}
