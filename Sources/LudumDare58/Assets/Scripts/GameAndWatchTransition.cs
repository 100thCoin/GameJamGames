using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAndWatchTransition : MonoBehaviour {

	public SpriteRenderer SR;

	// Use this for initialization
	void Start () {
		SR = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {

		SR.color = new Vector4 (1, 1, 1, Global.Dataholder.Watchbutton.AnimTimer);

	}
}
