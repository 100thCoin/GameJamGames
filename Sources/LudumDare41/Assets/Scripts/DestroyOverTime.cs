using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour {

	public float Delay;

	// Use this for initialization
	void Start () {
		StartCoroutine(Kill());
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	IEnumerator Kill()
	{
		yield return new WaitForSeconds (Delay);
		Destroy(gameObject);



	}




}
