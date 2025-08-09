using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour {


	public float Intensity;
	public float Decay;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(Intensity > 0)
		{
			Intensity *= Decay;
			Intensity -= 0.01f;
		}

	}

	void Update()
	{

		if(Intensity > 0)
		{

			Vector2 rand = new Vector3(Random.Range(-Intensity,Intensity),Random.Range(-Intensity,Intensity));
			transform.localPosition = rand;
		}
		else
		{
			transform.localPosition = Vector3.zero;
		}


	}
}
