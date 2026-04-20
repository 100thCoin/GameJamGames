using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketballCheckpoints : MonoBehaviour {

	public BasketballMain BasketMan;
	public bool Ch2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "Basketball_Ball") {
			BasketMan.Ping (Ch2);
		}
	}

}
