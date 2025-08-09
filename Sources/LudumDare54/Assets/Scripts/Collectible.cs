using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {

	public int RibbonType;
	public bool DoOnce;

	public bool Inactive;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if (Inactive) {
			return;
		}

		if (other.CompareTag ("Player")) {

			if (!DoOnce) {
				if (RibbonType == -1) {
					DoOnce = true;
					Global.Dataholder.GameEnded = true;

				} else {
					DoOnce = true;
					Global.Dataholder.RibbonIDForIcon = RibbonType;
					Global.Dataholder.RibbonCollectibles [RibbonType]++;
					Global.Dataholder.TimeSinceLastCollectible = 0;
					Instantiate (Global.Dataholder.Sparkles, transform.position, transform.rotation);
					Instantiate (Global.Dataholder.SFX_SpoolCollect, transform.position, transform.rotation);

					Destroy (gameObject);
				}
			}

		}

	}
}
