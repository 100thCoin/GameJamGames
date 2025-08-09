using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDragonDeath : MonoBehaviour {

	public GameObject Dragon;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Dragon == null)
		{
			Destroy(gameObject);
		}

	}
}
