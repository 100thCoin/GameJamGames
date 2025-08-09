using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedBonus : MonoBehaviour {

	public int howmany;
	public TextMesh TM;
	public float Timer;


	// Use this for initialization
	void Start () {

		TM.text = "+"+howmany+" Seeds!";

	}



	// Update is called once per frame
	void Update () {

		transform.position += new Vector3(0,Time.deltaTime*0.5f,0);
		Timer += Time.deltaTime;

		if(Timer > 3)
		{
			Destroy(gameObject);
		}

	}
}
