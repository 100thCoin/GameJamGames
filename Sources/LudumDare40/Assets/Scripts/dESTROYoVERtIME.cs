using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dESTROYoVERtIME : MonoBehaviour {

	public float Delay;

	public bool Radical;

	// Use this for initialization
	void Start () {

		StartCoroutine (kill ());




	}
	
	// Update is called once per frame
	void Update () {
		
	}


	IEnumerator kill ()
	{
		yield return new WaitForSeconds (Delay);

		if (Radical) {

			GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().NextReadyReady = true;

		}


		Destroy (gameObject);


	}



}
