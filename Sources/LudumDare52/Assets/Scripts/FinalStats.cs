using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalStats : MonoBehaviour {
	public bool MouseInside;
	public TextMesh Mesh;

	public SuperMain SM;

	void OnMouseOver () {

		MouseInside = true;
	}

	// Use this for initialization
	void Start () {
		SM = GameObject.Find ("SuperMain").GetComponent<SuperMain> ();
		string SRtime = DataHolder.StringifyTime (Main.Data.SpeedrunTimer);

		Mesh.text = "You win!\nSpeedrun time: " + SRtime + "\nCorn stolen: " + Main.Data.TotalCorn + "\nPeople killed: " + Main.Data.PeopleKilled;

	}
	
	// Update is called once per frame
	void Update () {

		if (MouseInside) {

			if (Input.GetKeyDown (KeyCode.Mouse0)) {

				//TODO RETURN TO TITLE
				SM.LoadTitle();
			}

		}
		MouseInside = false;
	}
}
