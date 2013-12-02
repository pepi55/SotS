using UnityEngine;
using System.Collections;

public class spawner : MonoBehaviour {


	public GameObject spawnOject;
	public int spawnSec = 150;
	private int timer = 0;
	void FixedUpdate () {
		timer++;
		if(timer%spawnSec==0){
			for (int i = 0; i<1 ;i++){
				Instantiate(spawnOject, transform.position, Quaternion.identity) ;
			}
		}
	}
}
