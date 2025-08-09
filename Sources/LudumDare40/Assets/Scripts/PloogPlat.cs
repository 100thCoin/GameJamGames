using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PloogPlat : MonoBehaviour {

	public bool Timer;

	public bool Power;

	public GameObject PowerSource;

	public Sprite Stopped;
	public Sprite Go;

	public bool DoOnce;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Power && !PowerSource.gameObject.GetComponent<PloogyPower> ().Powered) {
			gameObject.GetComponent<SpriteRenderer> ().sprite = Stopped;
			gameObject.GetComponent<BoxCollider> ().enabled = false;
			DoOnce = true;
		}

		if (Power && PowerSource.gameObject.GetComponent<PloogyPower> ().Powered && DoOnce) {
			DoOnce = false;
			gameObject.GetComponent<SpriteRenderer> ().sprite = Go;
			gameObject.GetComponent<BoxCollider> ().enabled = true;

		}
	}
}
