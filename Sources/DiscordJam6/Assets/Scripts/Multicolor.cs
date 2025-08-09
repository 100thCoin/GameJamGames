using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multicolor : MonoBehaviour {

	public SpriteRenderer SR;
	public SpriteRenderer Target;

	
	// Update is called once per frame
	void LateUpdate () {
		SR.color = Target.color;
	}
}
