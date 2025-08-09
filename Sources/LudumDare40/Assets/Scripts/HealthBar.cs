using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

	public float MaxHp;
	public float Hp;
	public GameObject Explosion;
	public GameObject Item;
	// Use this for initialization
	void Start () {

		MaxHp = 6;
		Hp = 6;

	}
	
	// Update is called once per frame
	void Update () {

		gameObject.transform.localPosition = new Vector3((Hp / MaxHp) * 0.9375f - 0.9375f,0,-0.5f); 
		gameObject.transform.localScale = new Vector3 ((Hp / MaxHp) * 0.9375f, 1, 1); 

		if (Hp <= 0) {

			Instantiate (Explosion, gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.position, gameObject.transform.rotation);
			//Instantiate (Item, gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.position, gameObject.transform.rotation);

			Destroy(gameObject.transform.parent.gameObject.transform.parent.gameObject);


		}

	}
}
