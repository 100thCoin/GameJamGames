using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOverTime : MonoBehaviour {

	public float Delay;

	// Use this for initialization
	void Start () {
		StartCoroutine (kill ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	IEnumerator kill()
	{
		yield return new WaitForSeconds (Delay);
		Destroy (gameObject);
	}




}
