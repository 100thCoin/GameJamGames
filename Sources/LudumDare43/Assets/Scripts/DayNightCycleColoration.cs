using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycleColoration : MonoBehaviour {

	public Color Sunrise;
	public Color Noon;
	public Color Sundown;
	public Color Midnight;

	public float Timer;
	public float Intervals;
	public int State;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		Timer += Time.deltaTime;


		if(Timer > Intervals)
		{
			State++;
			Timer-= Intervals;
			if(State == 4)
			{
				State = 0;
			}

		}


		if(State == 0)
		{
			gameObject.GetComponent<SpriteRenderer>().color = Sunrise *  (1 -(Timer / Intervals)) + Noon *  (Timer / Intervals);
		}
		if(State == 1)
		{
			gameObject.GetComponent<SpriteRenderer>().color = Noon *  (1 -(Timer / Intervals)) + Sundown *  (Timer / Intervals);
		}
		if(State == 2)
		{
			gameObject.GetComponent<SpriteRenderer>().color = Sundown *  (1 -(Timer / Intervals)) + Midnight *  (Timer / Intervals);
		}
		if(State == 3)
		{
			gameObject.GetComponent<SpriteRenderer>().color = Midnight *  (1 -(Timer / Intervals)) + Sunrise *  (Timer / Intervals);
		}





	}
}
