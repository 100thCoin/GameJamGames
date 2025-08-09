using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {


	public AudioClip Boom1;
	public AudioClip Boom2;
	public AudioClip Boom3;
	public AudioClip Boom4;
	public AudioClip Boom5;
	public AudioClip Boom6;
	public AudioClip Boom7;
	public AudioClip Boom8;

	public int RandomInt;




	// Use this for initialization
	void Start () {
		RandomInt = Random.Range (1, 9);

		if (RandomInt == 1) {gameObject.GetComponent<AudioSource> ().clip = Boom1;}
		if (RandomInt == 2) {gameObject.GetComponent<AudioSource> ().clip = Boom2;}
		if (RandomInt == 3) {gameObject.GetComponent<AudioSource> ().clip = Boom3;}
		if (RandomInt == 4) {gameObject.GetComponent<AudioSource> ().clip = Boom4;}
		if (RandomInt == 5) {gameObject.GetComponent<AudioSource> ().clip = Boom5;}
		if (RandomInt == 6) {gameObject.GetComponent<AudioSource> ().clip = Boom6;}
		if (RandomInt == 7) {gameObject.GetComponent<AudioSource> ().clip = Boom7;}
		if (RandomInt == 8) {gameObject.GetComponent<AudioSource> ().clip = Boom8;}
		gameObject.SetActive(false);
		gameObject.SetActive(true);


	}

	void OnEnable()
	{
		StartCoroutine (Ded ());

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator Ded()
	{

		yield return new WaitForSeconds (1.9f);
		Destroy (gameObject);
	}


}
