using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakTheseTilesTooltip : MonoBehaviour {

	public TextMesh TM;
	[TextArea(5,5)]
	public string DefaultMSG;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Global.Dataholder.NecromooncerDefeated && !Global.Dataholder.Upgrade_StardustRemover) {
			TM.text = DefaultMSG;
		} else {
			TM.text = "";
		}
	}
}
