using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombambleTile : MonoBehaviour {

	public bool DoOnce;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if (DoOnce) {
			return;
		}

		if (other.tag == "Bomb") {

			Instantiate (Global.DataHolder.Player.ExplosionPrefab, other.transform.position, transform.rotation, transform.parent.parent);
			//SFX isnt here yet, so...
			if (Global.DataHolder.Player.BombExplosion_SFX != null) {
				Instantiate (Global.DataHolder.Player.BombExplosion_SFX, transform.position, transform.rotation);
			}

			Destroy (other.gameObject);

			DoOnce = true;

			Destroy (gameObject);

		}
	}

}
