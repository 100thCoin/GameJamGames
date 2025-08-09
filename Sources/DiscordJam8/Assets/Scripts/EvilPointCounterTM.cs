using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilPointCounterTM : MonoBehaviour {

	public TextMesh TM;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!Global.DataHolder.Level2) {
			TM.text = "Evil: " + Global.DataHolder.EvilPoints + " / " + Global.DataHolder.RequiredEvilPoints;
		}
		else
		{
			TM.text = "Evil: " + Global.DataHolder.EvilPoints + " / " + Global.DataHolder.RequiredEvilPoints2;

		}
	}

}
