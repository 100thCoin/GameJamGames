using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firevent : MonoBehaviour {

	public SpriteRenderer Vent;
	public SpriteRenderer SR;
	public Sprite On;
	public Sprite Off;
	public float Timer;
	public float Timer2;

	public bool big;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Timer += Time.deltaTime;
		Timer2 +=Time.deltaTime;

		if (big) {
			if (Timer > 2) {
				SR.sprite = On;
			}
			if (Timer > 3) {
				Vent.gameObject.SetActive (true);
			}
			if (Timer > 4) {
				Vent.gameObject.SetActive (false);
				Timer -= 4;
				SR.sprite = Off;

			}
		} else {
			if (Timer > 1) {
				SR.sprite = On;
			}
			if (Timer > 2) {
				Vent.gameObject.SetActive (true);
			}
			if (Timer > 3) {
				Vent.gameObject.SetActive (false);
				Timer -= 3;
				SR.sprite = Off;

			}
		}

		if (Timer2 > 0) {
			Timer2 = Random.Range (-0.01f, -0.1f);
			Vent.flipX = !Vent.flipX;
		}



	}
}
