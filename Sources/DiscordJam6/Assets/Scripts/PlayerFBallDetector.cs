using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFBallDetector : MonoBehaviour {

	public MoveCharacter Char;
	public GameObject FireballHit;
	void OnTriggerStay(Collider other)
	{
		if(other.tag == "DShot")
		{
			Char.Health--;
			Instantiate(Char.HurtSFX,transform.position,transform.rotation,transform.parent);
			Instantiate(FireballHit,transform.position,transform.rotation,transform.parent);

			Destroy(other.transform.parent.gameObject);

		}
	}

}
