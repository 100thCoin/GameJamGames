using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballShadow : MonoBehaviour {

	// Use this for initialization
	void Start () {

		transform.position = transform.parent.position - new Vector3(0,1,0);
		transform.eulerAngles = new Vector3(0,0,0);
	}


}
