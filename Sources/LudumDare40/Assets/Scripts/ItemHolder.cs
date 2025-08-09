using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour {


	public int Items;

	public bool Explode;
	public int ExplodePos;
	public bool ExplodeOnce;
	public bool HasBoomed;
	public float BoomCounterLeft;
	public float BoomCounterRight;
	public bool FarRightBoomed;
	public bool FarLeftBoomed;

	public GameObject Explosion;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Explode && !ExplodeOnce) {
			ExplodeOnce = true;
			BoomCounterLeft = ExplodePos;
			BoomCounterRight = ExplodePos;
			StartCoroutine (Kabloomers ());

		}
	}


	IEnumerator Kabloomers()
	{
		yield return new WaitForSeconds (0.2f);

		if (HasBoomed) {

			if (!FarLeftBoomed) {
				gameObject.transform.Find ("Item " + BoomCounterLeft).gameObject.GetComponent<SpriteRenderer>().enabled = false;
				Instantiate (Explosion, gameObject.transform.Find ("Item " + BoomCounterLeft).gameObject.transform.position, gameObject.transform.rotation);

			}
			if (!FarRightBoomed) {
				gameObject.transform.Find ("Item " + BoomCounterRight).gameObject.GetComponent<SpriteRenderer>().enabled = false;
				Instantiate (Explosion, gameObject.transform.Find ("Item " + BoomCounterRight).gameObject.transform.position, gameObject.transform.rotation);

			}


		}
		if (!HasBoomed) {
			Instantiate (Explosion, gameObject.transform.Find ("Item " + ExplodePos).gameObject.transform.position, gameObject.transform.rotation);
			gameObject.transform.Find ("Item " + ExplodePos).gameObject.GetComponent<SpriteRenderer>().enabled = false;
		}
		if (!FarLeftBoomed) {
			BoomCounterLeft = BoomCounterLeft + 1;
		}
		if (!FarRightBoomed) {
			BoomCounterRight = BoomCounterRight - 1;
		}
		if (BoomCounterRight == 0) {
			FarRightBoomed = true;
		}
		if (BoomCounterLeft == Items + 1) {
			FarLeftBoomed = true;
		}


		HasBoomed = true;

		if (!FarLeftBoomed || !FarRightBoomed) {
			StartCoroutine (Kabloomers ());


		} else {

			while (BoomCounterLeft > 1) 
			{
				BoomCounterLeft = BoomCounterLeft - 1;
				Destroy (gameObject.transform.Find ("Item " + BoomCounterLeft).gameObject);



			}




		}


	}




}
