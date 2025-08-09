using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skelegibs : MonoBehaviour {

	public float Deathtimer;


	// Use this for initialization
	void Start () {

		Deathtimer = Random.Range(4f,8f);
		GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-12f,12f),Random.Range(20f,40f),0);
	}
	
	// Update is called once per frame
	void Update () {

		Deathtimer -= Time.deltaTime;
		if(Deathtimer < 0)
		{
			Destroy(gameObject);
		}


	}
}
