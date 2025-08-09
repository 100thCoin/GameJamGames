using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadStronky : MonoBehaviour {


	public float Timer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Timer += Time.deltaTime*2;
		transform.eulerAngles = new Vector3(0,0,Mathf.Round(Timer*20 % 4)*90);

		if(Timer > 2.75f)
		{
			Destroy(gameObject);
		}

	}
}
