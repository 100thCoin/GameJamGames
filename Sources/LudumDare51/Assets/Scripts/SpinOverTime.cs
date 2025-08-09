using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinOverTime : MonoBehaviour {

	public float Speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.eulerAngles = new Vector3(0,0,transform.eulerAngles.z + Speed *Time.deltaTime);
	}
}
