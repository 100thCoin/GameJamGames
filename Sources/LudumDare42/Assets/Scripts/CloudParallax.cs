using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudParallax : MonoBehaviour {


	public float Depth;

	public float Altitude;
	// Use this for initialization
	public FreeFallHolder Hold;
	public bool MTNS;
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if(!MTNS)
		{
			gameObject.transform.position = new Vector3(0,(Hold.Altitude - Altitude) * (-1 / Depth),gameObject.transform.position.z);

		}
		else
		{
			gameObject.transform.position = new Vector3(0,(Hold.Altitude - Altitude * Depth) * (-1 / Depth),gameObject.transform.position.z);
		}


	}
}
