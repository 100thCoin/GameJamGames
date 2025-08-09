using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTimer : MonoBehaviour {

	public float DeathClock;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		DeathClock-=Time.deltaTime;
		if(DeathClock < 0)
		{
			Destroy(gameObject);
		}
	}
}
