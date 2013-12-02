using UnityEngine;
using System.Collections;

public class Living : MonoBehaviour {

	public int Health = 100;
	private float HealthStart;
	public GameObject liveBar;
	public int damageOnHit = -25;
	private SpriteRenderer lifeUI;
	private Transform thisBar;
	private GameObject Bar;
	float widthBar;
	public void Start () {
		HealthStart = Health;
		if(gameObject.name=="Player"){
			lifeUI = GameObject.Find("LifeBarFill").GetComponent<SpriteRenderer>();
		}
	}

	public void spawnHealtBar(){
		Bar = Instantiate(liveBar,transform.position,Quaternion.identity) as GameObject;
		Bar.GetComponent<follow>().target = gameObject.transform;
		thisBar = Bar.transform.Find("LifeBarFill");
	}

	public void ChangeHealt(int d){
		Health += d;
		if(Bar){
			float barScale = Health/HealthStart;
			if(barScale<0){barScale=0;};
			thisBar.transform.localScale = new Vector3(barScale,1,1);
		}
		if(gameObject.name=="Player"){
			lifeUI.transform.localScale = new Vector3(Health/100.0f,1,1);
		}
	}

	public void Hit(){
		ChangeHealt(damageOnHit);
	}
}
