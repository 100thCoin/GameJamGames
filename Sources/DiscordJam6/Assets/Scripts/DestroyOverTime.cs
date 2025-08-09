using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour {

	public float Countdown;
	public Dataholder Main;
	// Use this for initialization
	void Start () {
		Main = GameObject.Find("Main").GetComponent<Dataholder>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Main.Paused)
		{
			return;
		}
		Countdown -= Time.deltaTime;
		if(Countdown < 0)
		{
			Destroy(gameObject);
		}
	}
}
