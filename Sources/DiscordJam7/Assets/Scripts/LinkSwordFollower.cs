using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkSwordFollower : MonoBehaviour {

	public Transform Link;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position = Link.transform.position;
	}
}
