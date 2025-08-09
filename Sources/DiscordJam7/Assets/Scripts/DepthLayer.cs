using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthLayer : MonoBehaviour {

	public float Height;
	public SpriteRenderer SR;


	// Update is called once per frame
	void Update () {
		SR.sortingOrder = Mathf.RoundToInt(-transform.position.y*16 - Height*16);
	}
}
