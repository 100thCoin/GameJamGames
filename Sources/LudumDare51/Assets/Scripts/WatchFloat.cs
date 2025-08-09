using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchFloat : MonoBehaviour {

	public float timer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		transform.localPosition = new Vector3(0,Mathf.Sin(timer)*0.5f,0);
	}
}
