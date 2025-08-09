using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {

	public bool Watch;


	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{

			if(Watch)
			{
				G.Main.UnlockedWatch = true;
				if(!G.Main.ArbitrarilyLargeArrayOfBooleans[6])
				{
					G.Main.ArbitrarilyLargeArrayOfBooleans[6] = true;
					Instantiate(G.Main.VALines[3]);
				}


				Destroy(gameObject);

			}
			else // level goal
			{
				G.Main.LevelClear = true;
				G.Main.LevelClearTimer = -2;

				if(G.Main.CurrentLevel == 5)
				{
					G.Main.VictoryJingle.SetActive(true);
					G.Main.InGameMusic.SetActive(false);
					G.Main.WonTheGame = true;
				}

				Destroy(gameObject);
			}

		}


	}


}
