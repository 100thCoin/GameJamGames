using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelClearArrow : MonoBehaviour {

	public float timer = 0;
	public float timer2 = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		timer = Mathf.Clamp01 (timer);
		transform.position = new Vector3(transform.position.x,DataHolder.ParabolicLerp(-7f,4f,timer,1),0);
		timer2 += Time.deltaTime;
		if (timer2 > 2) {
			Global.Dataholder.ScreenTransitionToNextLevel = true;
			Destroy (this);
		}

	}
}
