using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteCounter : MonoBehaviour {

	public TextMesh Tm;
	public GameObject Vis;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Global.Dataholder.Level != 2 || Global.Dataholder.Pmov.InCutscene) {
			Vis.SetActive (false);
		} else {
			Vis.SetActive (true);

			Tm.text = "x " + Global.Dataholder.Pmov.Collectibles.ToString ();
		}
	}
}
