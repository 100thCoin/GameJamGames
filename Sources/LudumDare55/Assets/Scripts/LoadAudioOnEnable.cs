using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAudioOnEnable : MonoBehaviour {

	public GameObject AudioClipp;

	// Use this for initialization
	void OnEnable () {

		Instantiate (AudioClipp, transform.position, transform.rotation, Global.Dataholder.transform);

	}
	void OnEnabled() {

		Instantiate (AudioClipp, transform.position, transform.rotation, Global.Dataholder.transform);

	}
}
