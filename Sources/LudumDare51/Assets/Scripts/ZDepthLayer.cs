using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZDepthLayer : MonoBehaviour {

	public SpriteRenderer SR;
	public int Bonus;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		SR.sortingOrder = Mathf.RoundToInt(-transform.position.z*16+16000) + Bonus;
	}
}
