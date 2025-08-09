using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour {

	public float precent;

	public bool Player;
	public bool Big;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (!Big && !Player) {
			gameObject.transform.localScale = new Vector2 (precent * 0.01f, 1);
			gameObject.transform.localPosition = new Vector3 ((0.875f / 100) * precent - 0.875f, 0.03f,-1);
		}

		if (!Big && Player) {
			gameObject.transform.localScale = new Vector2 (precent * 0.01f, 1);
			gameObject.transform.localPosition = new Vector3 ((2.93763f / 100) * precent - 2.93763f, 0,1);
		}

	}
}
