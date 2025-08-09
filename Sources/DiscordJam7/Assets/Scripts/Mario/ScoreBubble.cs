using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBubble : MonoBehaviour {

	public float Timer;
	public float Speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Timer -=Time.deltaTime;
		transform.position += new Vector3(0,Speed*Time.deltaTime,0);
		if(Timer < 0)
		{
			Destroy(gameObject);
		}
	}
}
