using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikePlantHurtPlayerMech : MonoBehaviour {

	public Sprite Disabled;

	void OnTriggerEnter(Collider other)
	{

		if(other.gameObject.name == "Mary")
		{

			other.gameObject.GetComponent<MaryController>().Hurt();
			Destroy(gameObject);
			gameObject.transform.parent.transform.parent.GetComponent<SpriteRenderer>().sprite = Disabled;

		}


	}
}
