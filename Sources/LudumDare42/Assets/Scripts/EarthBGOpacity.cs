using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthBGOpacity : MonoBehaviour {

	public FreeFallHolder Hold;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		gameObject.GetComponent<SpriteRenderer>().color = new Vector4(gameObject.GetComponent<SpriteRenderer>().color.r,gameObject.GetComponent<SpriteRenderer>().color.g,gameObject.GetComponent<SpriteRenderer>().color.b,-Hold.Altitude * 0.001f + 1.3f);



	}
}
