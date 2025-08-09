using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeExplode : MonoBehaviour {

	public bool DoIt;
	public GameObject Explosion;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(DoIt)
		{

			Instantiate(Explosion,transform.position,transform.rotation,transform.parent);
			transform.position = new Vector3(0,500,0);
			DoIt = false;
		}

	}
}
