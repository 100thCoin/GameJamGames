using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleFloat : MonoBehaviour {

	public Transform Target;
	public float Speed;
	public float Amplitude;
	float timer = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		Target.transform.localPosition = new Vector3 (0, Mathf.Sin(timer*Speed)*Amplitude, 0);
	}
}
