using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternalMonologueZoom : MonoBehaviour {

	public bool DoingZoom;
	public bool Zoom0;
	public float Timer0;
	public bool Zoom1;
	public float Timer1;


	public GameObject Cool;
	public GameObject Uncool;
	public GameObject Banner1;
	public GameObject Banner2;
	public Camera Cam;

	public SpriteRenderer UncoolSR;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKey (KeyCode.Space)) {
			Zoom0 = true;
		}

		if (Zoom0) {
			Timer0 += Time.deltaTime;
			UncoolSR.flipX = (Mathf.Sin (1 / (0.2f * Timer0 + 0.01f)) > 0);
			if (Timer0 > 1) {
				Zoom0 = false;
				Zoom1 = true;
			}
		}

		if (Zoom1) {
			Timer1 += Time.deltaTime;
			if (Timer1 > 1) {
				Timer1 = 1;
				Zoom1 = false;
			}
		}
		Cam.orthographicSize = DataHolder.ParabolicLerp (8, 16, 1-Timer1, 1);
		Banner1.transform.position = new Vector3 (0, DataHolder.ParabolicLerp (7, 17, 1-Timer1, 1), 0);
		Banner2.transform.position = new Vector3 (0, DataHolder.ParabolicLerp (-7, -17, 1-Timer1, 1), 0);
		Cool.SetActive (Timer1 > 0.94f);
		Uncool.SetActive (Timer1 <= 0.94f);

	}
}
