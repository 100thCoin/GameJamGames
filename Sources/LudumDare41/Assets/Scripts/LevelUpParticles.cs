using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpParticles : MonoBehaviour {
	public float timer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		gameObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y+0.05f,0);

		timer = timer + Time.deltaTime;


		if(timer > 3)
		{
			Destroy(gameObject);

		}


	}
}
