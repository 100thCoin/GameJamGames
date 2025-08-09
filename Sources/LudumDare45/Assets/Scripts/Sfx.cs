using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sfx : MonoBehaviour {

	public GameObject[] Sound;

	// Use this for initialization
	void Start () {

		int i = Random.Range(0,Sound.Length);
		Instantiate(Sound[i],gameObject.transform.position,gameObject.transform.rotation,gameObject.transform);

	}

}
