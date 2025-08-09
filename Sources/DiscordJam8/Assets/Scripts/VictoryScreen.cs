using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreen : MonoBehaviour {

	public TextMesh TM;

	// Use this for initialization
	void Start () {

		TM.text = "Speedrun Time: " + DataHolder.StringifyTime (Global.DataHolder.SpeedrunTimer) + "\nEvil Points: " + Global.DataHolder.EvilPoints;

		if (Global.DataHolder.EvilPoints == 4300) {
			TM.text += "\nMAXIMUM EVIL!\nWelcome to the Leage of Villains, \"Vilemourn\"!";
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
