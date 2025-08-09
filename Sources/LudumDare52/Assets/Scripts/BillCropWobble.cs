using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillCropWobble : MonoBehaviour {

	public float Timer;
	public float Speed;
	public float Amp;
	public float AngleRads;

	void Update () {
		Timer += Time.deltaTime;
		Amp -= Time.deltaTime * 7.5f;
		transform.eulerAngles = new Vector3 (0, 0, -(Mathf.Sin (Timer * Speed) * Amp) * Mathf.Cos(AngleRads));
		transform.localScale = new Vector3 (1, 1 - (Mathf.Cos (Timer * Speed) * (Amp*0.01f)) * Mathf.Sin(AngleRads),1);
		if (Amp < 0) {
			transform.eulerAngles = new Vector3 (0, 0, 0);
			transform.localScale = new Vector3 (1, 1 ,1);
			this.enabled = false;
		}

	}
}
