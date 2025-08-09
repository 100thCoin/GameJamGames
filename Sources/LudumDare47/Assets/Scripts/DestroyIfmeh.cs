using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIfmeh : MonoBehaviour {

	public GameObject Target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Target == null)
		{
			Destroy(gameObject);
		}

	}
}
