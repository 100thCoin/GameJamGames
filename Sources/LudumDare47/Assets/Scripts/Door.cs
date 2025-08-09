using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	public DataHolder Main;

	public RuntimeAnimatorController Closed;
	public RuntimeAnimatorController Open;

	public Animator Anim;

	// Update is called once per frame
	void Update () {

		Anim.runtimeAnimatorController = Main.DoorsOpen ? Open : Closed;

	}
}
