using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedrunTime : MonoBehaviour {

	public Dataholder Main;
	public TextMesh[] Messssh;

	// Use this for initialization
	void OnEnable () {

		float minutes = 0;

		float time = Main.SpeedrunTime;

		while(time > 60)
		{
			minutes++;
			time-=60;
		}

		string seconds = "" + Mathf.Round(time*100f)/100f;
		if(time < 10)
		{
			seconds = "0" + seconds;
		}


		Messssh[0].text = "YOU WIN!\nSpeedrun time:\n" + minutes + ":"+ seconds + "\nThanks for playing!";
		Messssh[1].text = "YOU WIN!\nSpeedrun time:\n" + minutes + ":"+ seconds + "\nThanks for playing!";
		Messssh[2].text = "YOU WIN!\nSpeedrun time:\n" + minutes + ":"+ seconds + "\nThanks for playing!";
		Messssh[3].text = "YOU WIN!\nSpeedrun time:\n" + minutes + ":"+ seconds + "\nThanks for playing!";
		Messssh[4].text = "YOU WIN!\nSpeedrun time:\n" + minutes + ":"+ seconds + "\nThanks for playing!";
		Messssh[5].text = "YOU WIN!\nSpeedrun time:\n" + minutes + ":"+ seconds + "\nThanks for playing!";
		Messssh[6].text = "YOU WIN!\nSpeedrun time:\n" + minutes + ":"+ seconds + "\nThanks for playing!";

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
