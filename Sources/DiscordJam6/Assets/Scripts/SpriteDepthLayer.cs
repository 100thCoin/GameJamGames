using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDepthLayer : MonoBehaviour {

	public SpriteRenderer SR;
	public float YOffset;
	public bool DoOnce;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		SR.sortingOrder = -Mathf.RoundToInt((transform.position.y+YOffset) *100);




		if(DoOnce)
		{
			Destroy(this);
		}


	}
}
