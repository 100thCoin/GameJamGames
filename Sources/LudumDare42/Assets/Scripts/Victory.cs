using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour {


	public GameObject VicWhoop;
	// Use this for initialization
	void Start () {

		StartCoroutine(SpawnWin());

	}
	
	IEnumerator SpawnWin()
	{
		yield return new WaitForSeconds(2);

		Instantiate(VicWhoop,new Vector3(0,0,-5),gameObject.transform.rotation);

	}
}
