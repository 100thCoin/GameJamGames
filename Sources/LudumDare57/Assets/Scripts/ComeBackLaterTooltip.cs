using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComeBackLaterTooltip : MonoBehaviour {

	public TextMesh TM;
	[TextArea(5,5)]
	public string DefaultMSG;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!Global.Dataholder.NecromooncerDefeated && (Global.Dataholder.PMov.transform.position - transform.position).magnitude < 12) {
			TM.text = DefaultMSG;
		} else {
			TM.text = "";
		}
	}
}
