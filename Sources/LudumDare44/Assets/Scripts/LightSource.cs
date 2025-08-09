using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour {



	// Use this for initialization
	void Start () {
	
		GameObject.Find("Main").GetComponent<DataHolder>().AddLightSource(gameObject);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
