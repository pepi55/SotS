using UnityEngine;
using System.Collections;

public class Living : MonoBehaviour {

	public int Health = 100;
	private int HealthStart;
	public GameObject liveBar;
	private SpriteRenderer lifeUI;
	private Transform thisBar;
	private GameObject Bar;
	public void Start () {
		HealthStart = Health;
		if(gameObject.name=="Player"){
			lifeUI = GameObject.Find("LifeBarFill").GetComponent<SpriteRenderer>();
		}
	}

	public void spawnHealtBar(){
		Bar = Instantiate(liveBar,transform.position,Quaternion.identity) as GameObject;
		Bar.GetComponent<follow>().target = gameObject.transform;
		//Bar.transform.parent = gameObject.transform;
		//Bar.transform.Translate( new Vector3(0,1,0));
		thisBar = Bar.transform.Find("LifeBarFill");
	}

	public void ChangeHealt(int d){
		Health += d;
		if(Bar){
			float barScale = Health/100.0f;
			thisBar.transform.localScale = new Vector3(barScale,1,1);
		}
		if(gameObject.name=="Player"){
			lifeUI.transform.localScale = new Vector3(Health/100.0f,1,1);
		}
	}
}
