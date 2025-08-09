using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TildesGarden : MonoBehaviour {

	public GameObject Child;
	public DataHolder Main;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Main.Progress == 4.59f)
		{
			Child.SetActive(true);
		}
		else
		{
			Child.SetActive(false);

		}

	}
}
