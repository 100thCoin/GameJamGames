using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerEnd : MonoBehaviour {

	public PlayerMovement P;
	public bool DoOnce;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(P.PerfectDive && !DoOnce)
		{
			DoOnce = true;

			float timm = GameObject.Find("Timer").gameObject.GetComponent<Timer>().Timerr;

			gameObject.GetComponent<TextMesh>().text = "" + timm + " Seconds";

		}




	}
}
