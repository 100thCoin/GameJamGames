using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PloogyPower : MonoBehaviour {
	public Sprite on;

	public Sprite off;
	public bool Powered;
	public bool cool;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{


		if (other.gameObject.tag == "Laser" && !Powered) {
			gameObject.GetComponent<SpriteRenderer> ().sprite = on;
			cool = true;
		}

		if (other.gameObject.tag == "Laser" && Powered) {
			Powered = false;
			gameObject.GetComponent<SpriteRenderer> ().sprite = off;

		}

		if (cool) {
			Powered = true;
			cool = false;

		}


	}

}
