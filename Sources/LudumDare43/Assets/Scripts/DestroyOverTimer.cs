using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTimer : MonoBehaviour {

	public float Timer;
	public float Dest;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Timer+= Time.deltaTime;


		if(Timer > Dest)
		{


			Destroy(gameObject);
		}

	}
}
