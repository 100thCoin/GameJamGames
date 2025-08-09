using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour {

	public bool Active;
	public Collider Col;

	void OnTriggerStay(Collider other)
	{

		Active = true;
		Col = other;
	}

}
