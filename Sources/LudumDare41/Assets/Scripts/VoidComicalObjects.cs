using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidComicalObjects : MonoBehaviour {


	public float time;
	public float H;
	public float G;
	public float D;
	public float E;
	// Use this for initialization
	void Start () {

		G = 0; //Start time
		H = Random.Range(1,4); // height of parabola
		D = Random.Range(1,4); // duration
		E = Random.Range(-14,14); //elevation
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		time = time + Time.fixedDeltaTime;

		gameObject.transform.position = new Vector3(((-H*4)/Mathf.Pow((D-G),2)) * Mathf.Pow((time-G),2) + ((H*4)/(D-G)) * (time-G) + (GameObject.Find("Main Camera").gameObject.transform.position.x-20),E,0);

		if(time > D)
		{
			Destroy(gameObject);
		}
	}
}
