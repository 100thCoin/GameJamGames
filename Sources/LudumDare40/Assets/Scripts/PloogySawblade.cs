using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PloogySawblade : MonoBehaviour {

	public bool Timer;

	public bool Power;

	public GameObject PowerSource;

	public Sprite Stopped;
	public RuntimeAnimatorController Go;

	public bool DoOnce;

	public bool Inverted;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (!Inverted) {
			if (Power && !PowerSource.gameObject.GetComponent<PloogyPower> ().Powered) {
				gameObject.GetComponent<Animator> ().runtimeAnimatorController = null;
				gameObject.GetComponent<SpriteRenderer> ().sprite = Stopped;
				gameObject.tag = "Untagged";
				DoOnce = true;
			}

			if (Power && PowerSource.gameObject.GetComponent<PloogyPower> ().Powered && DoOnce) {
				DoOnce = false;
				gameObject.GetComponent<Animator> ().runtimeAnimatorController = Go;
				//gameObject.GetComponent<SpriteRenderer> ().sprite = Stopped;
				gameObject.tag = "SawBlade";
			}
		} else {


			if (Power && PowerSource.gameObject.GetComponent<PloogyPower> ().Powered) {
				gameObject.GetComponent<Animator> ().runtimeAnimatorController = null;
				gameObject.GetComponent<SpriteRenderer> ().sprite = Stopped;
				gameObject.tag = "Untagged";
				DoOnce = true;
			}

			if (Power && !PowerSource.gameObject.GetComponent<PloogyPower> ().Powered && DoOnce) {
				DoOnce = false;
				gameObject.GetComponent<Animator> ().runtimeAnimatorController = Go;
				//gameObject.GetComponent<SpriteRenderer> ().sprite = Stopped;
				gameObject.tag = "SawBlade";
			}

		}


	}
}
