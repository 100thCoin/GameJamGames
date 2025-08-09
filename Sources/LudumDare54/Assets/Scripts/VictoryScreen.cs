using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreen : MonoBehaviour {

	public TextMesh TM;



	// Use this for initialization
	void Start () {

		int spoolcount = 0;
		int maxSpool = 0;
		int i = 0;
		while (i < Global.Dataholder.ImplementedRibbonsPerType.Length) {
			spoolcount += Global.Dataholder.RibbonCollectibles [i];
			maxSpool += Global.Dataholder.ImplementedRibbonsPerType [i];


			i++;
		}


		TM.text = "Speedrun Time:\n" + DataHolder.StringifyTime (Global.Dataholder.SpeedrunTime) + "\n\nTimes you entered\nthe ribbon: " + Global.Dataholder.TimesEnteredTheRibbon + "\n\nTotal Spools: " + spoolcount + "/" + maxSpool;



	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
