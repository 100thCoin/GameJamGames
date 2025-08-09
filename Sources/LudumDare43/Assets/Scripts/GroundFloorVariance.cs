using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFloorVariance : MonoBehaviour {

	public Sprite[] Floor;


	// Use this for initialization
	void Start () {

		int r = Random.Range(0,Floor.Length);

		gameObject.GetComponent<SpriteRenderer>().sprite = Floor[r];

	}

}
