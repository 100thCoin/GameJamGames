using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingCounter : MonoBehaviour {



	// Use this for initialization
	void Start () {



	}
	
	// Update is called once per frame
	void Update () {

		gameObject.GetComponent<TextMesh>().text = ""+GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().CurrentPloogies;


	}
}
