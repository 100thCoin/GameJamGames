using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	public bool OpenBool;

	public RuntimeAnimatorController Open;
	public Sprite Closed;

	public float OpenTimer;

	void Update()
	{
		OpenTimer+= Time.deltaTime;
		if(OpenBool && OpenTimer < 1)
		{
			GetComponent<Animator>().runtimeAnimatorController = Open;
		}
		else
		{
			GetComponent<Animator>().runtimeAnimatorController = null;
		}
	}

}
