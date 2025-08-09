using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PloogSky : MonoBehaviour {

	public Sprite Sky1;
	public Sprite Sky2;




	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (GameObject.Find ("Main Camera").gameObject.transform.position.x > 125) {
			gameObject.GetComponent<SpriteRenderer> ().sprite = Sky2;
		}
		if (GameObject.Find ("Main Camera").gameObject.transform.position.x < 125) {
			gameObject.GetComponent<SpriteRenderer> ().sprite = Sky1;
		}



		
	}
}
