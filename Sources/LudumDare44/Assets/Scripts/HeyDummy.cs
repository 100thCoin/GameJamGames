using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeyDummy : MonoBehaviour {

	public DataHolder Main;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Main.Outside)
		{
			GetComponent<SpriteRenderer>().enabled = true;
		}
		else
		{
			GetComponent<SpriteRenderer>().enabled = false;
		}

		if(Main.UnlockedPick)
		{
			Destroy(gameObject);

		}

	}
}
