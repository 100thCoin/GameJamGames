using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour {

	public Renderer R;
	MaterialPropertyBlock propB;

	public float Timer;

	// Use this for initialization
	void Start () {
		propB = new MaterialPropertyBlock();


	}
	
	// Update is called once per frame
	void Update () {

		Timer += Time.deltaTime*30;

		R.GetPropertyBlock(propB);

		propB.SetFloat("_Temp2",Timer);
		R.SetPropertyBlock(propB);

	}
}
