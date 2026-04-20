using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hackFont : MonoBehaviour {

	public Font F;
	// Use this for initialization
	[ContextMenu("Font")]
	void Start () {
		if(F != null)
		{
			F.material.mainTexture.filterMode = FilterMode.Point;
		}
	}
}
