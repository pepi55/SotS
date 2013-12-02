using UnityEngine;
using System.Collections;

public class follow : MonoBehaviour {
	public Transform target;
	public Vector3 displacement;

	private void Update(){
		if(target){
			gameObject.transform.position = target.position+ Vector3.up+displacement;
		}else{
			Debug.Log("no target found");
			GameObject.Destroy(gameObject);
		}
	}
}
