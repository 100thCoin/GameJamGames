using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicOrbFloat : MonoBehaviour {
	public float speed;
	public float amplitude;
	public float timer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		transform.localPosition = new Vector3(0,Mathf.Sin(timer*speed)*amplitude,0);
	}

}
