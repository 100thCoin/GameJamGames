using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour {

	public Material Mat;
	public float timer;
	public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime*speed;
		Mat.SetFloat ("_Temp2", timer);
		Mat.SetFloat ("_Mult", ((1f)/(timer*timer+1))*0.005f);

	}
}
