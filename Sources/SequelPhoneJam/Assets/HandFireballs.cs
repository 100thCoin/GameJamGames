using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandFireballs : MonoBehaviour {

	public GameObject Fireball;
	public float Timer;
	public Environment Env;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Env.HandsReady)
		{
			Timer-=Time.deltaTime;
			if(Timer < 0)
			{
				Timer += Random.Range(0.5f,2f);
				GameObject Cfire  = Instantiate(Fireball,transform.position,transform.rotation);
				Cfire.transform.GetChild(0).GetComponent<FireballMove>().Env = Env;
			}
		}
	}
}
