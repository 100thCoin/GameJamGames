using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZap : MonoBehaviour {

	public bool Big;

	// Use this for initialization
	void Start () {
		if (!Big) {
			gameObject.name = "EnemyZap";	
		}
		if (!Big) {
			gameObject.name = "EnemyZap2";	
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
