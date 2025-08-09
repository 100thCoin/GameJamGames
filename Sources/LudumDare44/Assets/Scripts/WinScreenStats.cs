using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreenStats : MonoBehaviour {

	public bool Speedrun;
	public bool Heartbeat;
	public bool Money;
	public bool Spend;

	public DataHolder Main;

	// Use this for initialization
	void OnEnable () {

		if(Speedrun)
		{
			int S = Mathf.RoundToInt(Main.TimeElaspsed*100);
			int M = S / 6000;
			float Sec = (S%6000)/100f;
			print(Sec);
			if(Sec < 10)
			{
				GetComponent<TextMesh>().text = "Speedrun Time: " + M + ":0" + Sec;

			}
			else
			{
				GetComponent<TextMesh>().text = "Speedrun Time: " + M + ":" + Sec;

			}


		}

		if(Heartbeat)
		{		
			GetComponent<TextMesh>().text = "Money Lost: " + Main.TotalHeart;
		}

		if(Money)
		{		
			GetComponent<TextMesh>().text = "Money Earned: " + Main.TotalEarned;
		}
		if(Spend)
		{		
			GetComponent<TextMesh>().text = "Money Spent: " + Main.TotalSpent;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
