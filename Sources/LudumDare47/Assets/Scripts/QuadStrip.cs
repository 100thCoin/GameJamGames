using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadStrip : MonoBehaviour {

	public MaterialPropertyBlock PropB;
	public Renderer R;

	public float Offset;
	public float Scale;

	// Use this for initialization
	void OnEnable () {
	
		PropB = new MaterialPropertyBlock();
		R = GetComponent<Renderer>();

	}
	
	// Update is called once per frame
	void Update () {

		R.GetPropertyBlock(PropB);
		PropB.SetVector("_MainTex_ST", new Vector4(Scale,1,Offset,0));
		R.SetPropertyBlock(PropB);

	}
}
