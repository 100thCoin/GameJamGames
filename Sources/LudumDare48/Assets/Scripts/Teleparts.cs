using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleparts : MonoBehaviour {

	public bool Away;
	public Material Mat;
	public float Timer;

	// Use this for initialization
	void OnEnable () {
		if(Away)
		{
			Mat.SetFloat("_pow",0);
		}
		else
		{
			Mat.SetFloat("_pow",20);
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		Timer += Time.deltaTime;
		if(Away)
		{
			Mat.SetFloat("_pow",Timer*10);
		}
		else
		{
			Mat.SetFloat("_pow",Mat.GetFloat("_pow")/1.4f);
		}
		if(Timer > 0.5f)
		{
			if(Away)
			{
				GameObject.Find("Main").GetComponent<DataHolder>().Player.SR.enabled = true;
			}
			Destroy(gameObject);
		}
	}

}
