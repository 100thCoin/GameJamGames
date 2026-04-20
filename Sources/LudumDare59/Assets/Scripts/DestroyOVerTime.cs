using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOVerTime : MonoBehaviour {

	public float Timer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Timer -= Time.deltaTime;
	}
}
