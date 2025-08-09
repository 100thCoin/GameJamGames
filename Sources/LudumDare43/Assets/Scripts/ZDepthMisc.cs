using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZDepthMisc : MonoBehaviour {

	public float YOffset;

	// Use this for initialization
	void Start () {
		//  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH  Z DEPTH
		gameObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,-1 + ((gameObject.transform.position.y - YOffset) * 0.0001f));

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
