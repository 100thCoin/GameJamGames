using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeterCount : MonoBehaviour {


	public FreeFallHolder Hold;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		gameObject.GetComponent<TextMesh>().text = "" +  Mathf.Round(Hold.Altitude / 30) + "Km";

	}
}
