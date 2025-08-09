using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour {

	public float CountDown;
	// Update is called once per frame
	void Update () {
		CountDown -= Time.deltaTime;
		if(CountDown < 0)
		{
			Destroy(gameObject);
		}
	}
}
