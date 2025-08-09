using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialText : MonoBehaviour {


	public string[] Messages;
	public TextMesh TM;

	// Use this for initialization
	void Start () {
		Messages = new string[16];

		Messages [0] = "Click on Professor Dreadd.";
		Messages [1] = "Prof. Dreadd must be placed at the end of the bridge.";
		Messages [2] = "Click on Chadford Starcrusher.";
		Messages [3] = "Chadford must be placed to the left of the bridge.";

		Messages [4] = "Chadford claims there were traps!\nThe more traps you can dodge, the more evil you must be.\nYou need a minimum of " + Global.DataHolder.RequiredEvilPoints + " \"Evil Points\" to continue.";


	}
	
	// Update is called once per frame
	void Update () {
		if (Global.DataHolder.LevEditMain.level == 1) {
			TM.text = "";
			return;
		}

		if (!Global.DataHolder.LevEditMain.CompletedTut1 && Global.DataHolder.LevEditMain.CurrentPick == null) {
			TM.text = Messages [0];
		}
		else if (!Global.DataHolder.LevEditMain.CompletedTut1 && Global.DataHolder.LevEditMain.CurrentPick != null) {
			TM.text = Messages [1];
		}
		else if (Global.DataHolder.LevEditMain.CompletedTut1 && !Global.DataHolder.LevEditMain.CompletedTut2 && Global.DataHolder.LevEditMain.CurrentPick == null) {
			TM.text = Messages [2];
		}
		else if (Global.DataHolder.LevEditMain.CompletedTut1 && !Global.DataHolder.LevEditMain.CompletedTut2 && Global.DataHolder.LevEditMain.CurrentPick != null) {
			TM.text = Messages [3];
		}
		else if (Global.DataHolder.LevEditMain.CompletedTut1 && Global.DataHolder.LevEditMain.CompletedTut2 && Global.DataHolder.LevEditMain.Tut2Timer < 1) {
			TM.text = "";
		}
		else if (!Global.DataHolder.LevEditMain.CompletedTut3) {
			TM.text = Messages [4];
		}


	}
}
