using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGameTile : MonoBehaviour {

	public Material Glow;
	public Material NotGlow;

	public MeshRenderer MR;

	public bool Active;
	public int grace;
	public bool Victory;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (Victory) {
			MR.material = Glow;
			return;
		}

		MR.material = Active ? Glow : NotGlow;

		if (grace < 0) {

			Active = false;
		}

		grace--;
	}

	void OnTriggerStay(Collider other)
	{

		if (other.CompareTag ("Ribbon") || other.CompareTag ("Bouncy")) {

			grace = 10;
			Active = true;

		}


	}



}
