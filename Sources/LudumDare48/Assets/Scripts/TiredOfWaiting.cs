using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiredOfWaiting : MonoBehaviour {

	public string Type;
	public DataHolder Main;

	// Use this for initialization
	void OnMouseOver()
	{
		if(Input.GetKeyDown(KeyCode.Mouse0))
		{
			switch(Type)
			{

			case "XPos" : Main.PlayerPosXTimer = 3; break;
			case "YPos" : Main.PlayerPosYTimer = 3; break;
			case "Held" : Main.PlayerHeldTimer = 3; break;
			case "Dead" : Main.PlayerDeadTimer = 3; break;
			case "Coin" : Main.CoinTimer = 3; break;
			case "Door" : Main.DoorOpenTimer = 3; break;
			case "PBtn" : Main.ButtonTimer = 3; break;
			case "Name" : Main.PlayerNameTimer = 3; break;


			}


		}
	}
}
