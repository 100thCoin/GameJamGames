using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionHurtPlayerMech : MonoBehaviour {

	public bool DoOnce;
	void OnTriggerEnter(Collider other)
	{

		if(other.gameObject.name == "Mary" && !DoOnce)
		{
			DoOnce = true;
			other.gameObject.GetComponent<MaryController>().Hurt();


			StartCoroutine(bulletregen());

		}



	}

	IEnumerator bulletregen()
	{
		yield return new WaitForSeconds(1);

		DoOnce = false;
	}


}