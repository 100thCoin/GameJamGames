using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHolder : MonoBehaviour {

	public GameObject Main;

	// Use this for initialization
	void Start () {
		GameObject NewM = Instantiate(Main,Vector3.zero,transform.rotation);
		NewM.name = "Main";
	}

}
