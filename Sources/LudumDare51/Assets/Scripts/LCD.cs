using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LCD : MonoBehaviour {

	public bool On;
	public float Timer;
	// 3  --> -2.5

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(On)
		{
			Timer = Mathf.Clamp01(Timer + Time.deltaTime*4);
		}
		else
		{
			Timer = Mathf.Clamp01(Timer - Time.deltaTime*4);
		}

		transform.localPosition = new Vector3(transform.localPosition.x,transform.localPosition.y,Main.ParabolicLerp(3,-2.5f,Timer,1));
			

	}
}
