using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePow : MonoBehaviour {

	public float Timer;
	public SpriteRenderer SR;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Timer += Time.deltaTime;
		if (Timer < 0.03f) {
			SR.color = new Vector4 (0, 0, 0, 1);
		} else if (Timer < 0.05f) {
			SR.color = new Vector4 (1, 1, 1, 1);
		} else {
			
			SR.color = new Vector4 (1, 1, 1, DataHolder.ParabolicLerp(1,0,Mathf.Clamp01(Timer-0.05f),1));
			
		}
	}
}
