using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackLayer : MonoBehaviour {

	public DataHolder Main;

	public bool Calm;
	public bool Move;
	public bool Forest;

	public AudioSource AudCalm;
	public AudioSource AudMove;
	public AudioSource AudForest;


	public float VolCalm;
	public float VolMove;
	public float VolForest;

	// Use this for initialization
	void Start () {
		
	}



	// Update is called once per frame
	void Update () {

		if(Main.Calm)
		{
			Calm = true;
			Move = false;
			Forest = false;
		}
		if(Main.Move)
		{
			Calm = false;
			Move = true;
			Forest = false;
		}
		if(Main.Forest)
		{
			Calm = false;
			Move = false;
			Forest = true;
		}
		if(!Main.Calm)
		{
			Calm = false;
		}
		if(Calm)
		{
			VolCalm += Time.deltaTime;
		}
		else
		{
			VolCalm -= Time.deltaTime;
		}

		if(Move)
		{
			VolMove += Time.deltaTime;
		}
		else
		{
			VolMove -= Time.deltaTime;
		}


		if(Forest)
		{
			VolForest += Time.deltaTime;
		}
		else
		{
			VolForest -= Time.deltaTime;
		}


		if(VolCalm > 1)
		{
			VolCalm = 1;
		}
		if(VolForest > 1)
		{
			VolCalm = 1;
		}
		if(VolMove > 1)
		{
			VolMove =1;
		}

		if(VolCalm < 0)
		{
			VolCalm = 0;
		}
		if(VolForest < 0)
		{
			VolForest =0;
		}
		if(VolMove < 0)
		{
			VolMove =0;
		}


		AudCalm.volume = VolCalm;
		AudForest.volume = VolForest;
		AudMove.volume = VolMove;

		if(Main.Mute)
		{
			AudCalm.volume = 0;
			AudForest.volume = 0;
			AudMove.volume = 0;

		}

	}
}
