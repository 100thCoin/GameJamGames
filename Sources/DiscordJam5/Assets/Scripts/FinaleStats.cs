using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinaleStats : MonoBehaviour {

	public TextMesh TM;
	public string Output;
	public DataHolder Main;
	public void UpdateTM()
	{



		int minutes = 0;
		float temp = Main.TimerSeconds;
		//Yeah, this is the method I chose to calculate this. Fight me.
		while(temp > 60)
		{
			temp-=60;
			minutes++;
		}
		float seconds = Main.TimerSeconds%60;
		string secondsS = "" +seconds;
		if (secondsS.Length > 1)
		{
			if (secondsS.Substring(1,1) == ".")
			{
				secondsS = "0" + secondsS;
			}
		}
		secondsS = secondsS.Length > 5 ? secondsS.Substring(0,5) : secondsS.Substring(0,secondsS.Length);

		string SpeedrunTime = "" + minutes + ":" + secondsS;
		Output = "You win!\n\nLevels reset: " + Main.Resets + "\n\nSpeedrun\ntime: " + SpeedrunTime + "\n\nPress ESC to quit.";
		TM.text = Output;
		Main.PressEscToQuit = true;
	}

}
