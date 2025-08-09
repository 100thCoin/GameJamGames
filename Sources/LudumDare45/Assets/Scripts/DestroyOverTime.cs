using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour {

	public float DeathTimer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		DeathTimer -= Time.deltaTime;

		if(DeathTimer < 0)
		{
			Destroy(gameObject);
		}

	}
}
