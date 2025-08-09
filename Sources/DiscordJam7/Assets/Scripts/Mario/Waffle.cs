using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waffle : MonoBehaviour {

	public float Timer;
	public Transform Waf;
	float rad = 8;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(Main.DataHolder.PauseMario)
		{
			return;
		}
		Timer+=Time.fixedDeltaTime*3;
		Waf.transform.localPosition = new Vector3(Mathf.Cos(Timer)*rad,Mathf.Sin(Timer)*rad,0);


	}
}
