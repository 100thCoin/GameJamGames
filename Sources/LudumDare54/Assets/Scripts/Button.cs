using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

	public bool Pressed;

	public float PressTimer;

	public Transform ButtonVis;

	public MeshRenderer ButtonFace;
	public Material PressedMat;
	public Material UnpressedMat;
	public float speed = 6;

	public int grace;

	public bool ToggleButton;
	public bool ToggleCheck;
	public bool ON;
	public bool ONCheckForSFX;

	public GameObject AssociatedCollectible;
	public bool OpenWhenCollectibleGone;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		bool alwaysOn = false;
		if (OpenWhenCollectibleGone) {
			if (AssociatedCollectible == null) {
				alwaysOn = true;
				Pressed = true;
			}
		}

		ButtonFace.material = Pressed ? PressedMat : UnpressedMat;

		if (Pressed) {
			PressTimer += Time.deltaTime*speed;
		} else {
			PressTimer -= Time.deltaTime*speed;
		}

		ButtonVis.localPosition = new Vector3 (0, DataHolder.SinLerp(-0.5f,-0.666f,PressTimer,1), 0);

		PressTimer = Mathf.Clamp01 (PressTimer);
	}

	void FixedUpdate () {

		if (ONCheckForSFX != ON) {
			ONCheckForSFX = ON;
			if (ON) {
				Instantiate (Global.Dataholder.SFX_ButtonPress, transform.position, transform.rotation);
			} else {
				Instantiate (Global.Dataholder.SFX_ButtonRelease,transform.position,transform.rotation);
			}


		}


		if (grace < 0) {
			Pressed = false;
			ToggleCheck = true;
			if (!ToggleButton) {
				ON = false;
			}
		}
		grace--;
		ONCheckForSFX = ON;
	}


	void OnTriggerStay(Collider other)
	{
		if (other.CompareTag ("Player")) {

			grace = 16;
			Pressed = true;
			if (ToggleButton) {
				if (ToggleCheck) {
					ToggleCheck = false;
					ON = !ON;

				}
			} else {
				ON = true;
			}

		}

	}


}
