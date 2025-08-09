using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMoneyFlash : MonoBehaviour {

	public TextMesh sr;

	public float Timer;

	void Update () {

		Timer += Time.deltaTime;
		Color Temp = new Vector4(1,0.5f + Mathf.Sin(Timer)*0.5f,0,1);
		sr.color = Temp;
	}
}
