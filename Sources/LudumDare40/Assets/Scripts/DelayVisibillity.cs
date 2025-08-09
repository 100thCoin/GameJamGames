using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayVisibillity : MonoBehaviour {


	public float Delay;

	public bool Lose;

	// Use this for initialization
	void Start () {

		StartCoroutine (see ());




	}

	// Update is called once per frame
	void Update () {

	}


	IEnumerator see ()
	{
		yield return new WaitForSeconds (Delay);
		gameObject.GetComponent<SpriteRenderer> ().enabled = true;

		if (!Lose) {
			gameObject.GetComponent<Intermission> ().Dead.gameObject.GetComponent<TextMesh> ().color = new Vector4 (1, 1, 1, 1);
			gameObject.GetComponent<Intermission> ().TotalDead.gameObject.GetComponent<TextMesh> ().color = new Vector4 (1, 1, 1, 1);
			gameObject.GetComponent<Intermission> ().Behind.gameObject.GetComponent<TextMesh> ().color = new Vector4 (1, 1, 1, 1);
			gameObject.GetComponent<Intermission> ().Saved.gameObject.GetComponent<TextMesh> ().color = new Vector4 (1, 1, 1, 1);
		} else {
			gameObject.transform.Find("Cube1").gameObject.SetActive(true);
			gameObject.transform.Find("Cube2").gameObject.SetActive(true);

		}



	}
}
