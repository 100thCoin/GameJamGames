using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnComicalHazards : MonoBehaviour {

	public float Delay;
	float timer;
	int Pickone;
	public GameObject[] ComedyList;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		timer = timer + Time.fixedDeltaTime;

		if(timer > Delay)
		{
			timer = timer - Delay;
			Pickone = Random.Range(0,10);

			if(Pickone == 10)
			{
				Pickone = Random.Range(0,11);

			}

			Instantiate(ComedyList[Pickone],new Vector3(gameObject.transform.position.x - 20,0,0),gameObject.transform.rotation,GameObject.Find("Main Camera").gameObject.transform);
		
		}



	}
}
