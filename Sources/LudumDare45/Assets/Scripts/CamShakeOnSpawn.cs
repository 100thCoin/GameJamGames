using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShakeOnSpawn : MonoBehaviour {


	public float Intensity;

	// Use this for initialization
	void Start () {

		GameObject.Find("Main Camera").GetComponent<CamShake>().Intensity = Intensity;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
