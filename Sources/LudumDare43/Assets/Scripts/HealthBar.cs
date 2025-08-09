using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

	public Enemy Boss;


	public GameObject RedBar;

	public float BossMax;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		if(Boss != null)
		{

			gameObject.GetComponent<SpriteRenderer>().enabled = true;

			RedBar.SetActive(true);
			RedBar.transform.localPosition = new Vector3(-5 + (Boss.HP / BossMax)*5 ,RedBar.transform.localPosition.y,RedBar.transform.localPosition.z);
			RedBar.transform.localScale = new Vector3(Boss.HP / BossMax ,RedBar.transform.localScale.y,RedBar.transform.localScale.z);





		}else
		{
			RedBar.SetActive(false);

			gameObject.GetComponent<SpriteRenderer>().enabled = false;


		}




	}
}
