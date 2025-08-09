using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelClearText : MonoBehaviour {

	public float timer;
	public bool GetToPlace;
	public bool Shrink;
	public GameObject Particles;
	public bool Win;
	public float winTimer;

	public bool[] AirhornsBlasted;
	public GameObject Airhorns;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
		if(GetToPlace)
		{
			timer+=Time.deltaTime;
			transform.position  = new Vector3(-24.71f,-Mathf.Cos(Mathf.PI*timer*(1))*0.5f*(34-27)+0.5f*(34+27),-3);
			if(timer > 1)
			{
				GetToPlace = false;
				Shrink = true;
				timer = 0;
			}
		}

		if(Shrink)
		{
			timer+=Time.deltaTime;
			transform.localScale  = new Vector3(-Mathf.Cos(Mathf.PI*timer*(8f))*0.5f*(1-1.5f)+0.5f*(1+1.5f),-Mathf.Cos(Mathf.PI*timer*(8f))*0.5f*(1-1.5f)+0.5f*(1+1.5f),1);
			if(timer > 0.125f)
			{
				Particles.SetActive(true);
				Shrink = false;
				timer = 0;
			}
		}

		if(Win)
		{
			winTimer+= Time.deltaTime;




			if(winTimer > 4)
			{
				GameObject.Find("Main").GetComponent<DataHolder>().NextLevel = true;
				Destroy(gameObject);
			}

		}

	}
}
