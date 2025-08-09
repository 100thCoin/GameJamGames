using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictorySpoolRainbow : MonoBehaviour {

	public SpriteRenderer SR;
	public float timer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime*2;
		SR.color = new Vector4 (Mathf.Sin(timer)*0.5f+0.65f, Mathf.Sin(timer + Mathf.PI*0.666666f)*0.5f+0.65f, Mathf.Sin(timer + Mathf.PI * 1.333333f)*0.5f+0.65f, 1);

	}
}
