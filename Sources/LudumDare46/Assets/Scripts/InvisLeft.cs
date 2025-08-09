using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisLeft : MonoBehaviour {

	public DataHolder Main;

	public BoxCollider BC;

	public bool Right;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(!Right)
		{
			BC.enabled = false;

			BC.enabled = BC.enabled || (Main.Progress  < 3.1f);

			BC.enabled = BC.enabled || Main.Progress  == 4f;
		}
		else
		{
			BC.enabled = false;

			BC.enabled = BC.enabled || (Main.Progress  < 3.05f);

			BC.enabled = BC.enabled || Main.Progress  == 4f;
		}


	}
}
