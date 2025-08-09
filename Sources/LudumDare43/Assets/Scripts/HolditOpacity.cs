using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolditOpacity : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		gameObject.GetComponent<SpriteRenderer>().color = new Vector4(1,1,1,gameObject.GetComponent<SpriteRenderer>().color.a - 0.01f);


	}
}
