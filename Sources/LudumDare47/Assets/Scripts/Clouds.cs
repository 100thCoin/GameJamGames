using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour {

	public float Speed;
	public float Rot;
	public float Timer;

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
	


		Timer+= Time.deltaTime * Speed;
		Rot = transform.parent.eulerAngles.y;

		Offset = Timer + Rot/90f;

		PropB.SetVector("_MainTex_ST", new Vector4(Scale,1,Offset,0));
		R.SetPropertyBlock(PropB);

	}
}
