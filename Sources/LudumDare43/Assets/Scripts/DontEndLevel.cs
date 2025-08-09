using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontEndLevel : MonoBehaviour {

	public MaryController Mary;
	public GameObject NextLevel;

	public bool WinGame;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Mary.HasKey)
		{

			gameObject.name = "EndLevel";


		}



	}
}
