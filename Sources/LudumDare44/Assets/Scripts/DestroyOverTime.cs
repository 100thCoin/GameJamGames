using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour {


	public float Delay;

	void Update () {

		Delay -= Time.deltaTime;
		if(Delay <0)
		{
			Destroy(gameObject);
		}

	}
}
