using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PloogyBox : MonoBehaviour {

	public int PloogiesSaved;
	public Sprite Glow;
	public bool NextLevelReady;
	public bool Powered;
	public GameObject Source;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (PloogiesSaved >= 10) {

			gameObject.GetComponent<SpriteRenderer> ().sprite = Glow;
			NextLevelReady = true;
		}

		if (Source.gameObject.GetComponent<PloogyPower> ().Powered && NextLevelReady) {

			GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().NopMoving = true;
			GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().Win = true;
			GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().RemainingPloogies = PloogiesSaved;


		}


	}
}
