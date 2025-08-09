using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDoorCol : MonoBehaviour {

	public DataHolder Main;

	public GameObject OtherSide;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		if( Main.PostTUT)
		{
			OtherSide.SetActive(true);
			Destroy(gameObject);

		}


	}
}
