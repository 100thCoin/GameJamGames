using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeWhiteEndGame : MonoBehaviour {

	public float Timer;

	public DataHolder Main;

	public SpriteRenderer SR;


	// Update is called once per frame
	void Update () {

		Timer += Time.deltaTime;

		SR.color = new Vector4(1,1,1,Timer);

		if(Timer > 1)
		{
			Main.LoadVictorySequence();

		}

	}
}
