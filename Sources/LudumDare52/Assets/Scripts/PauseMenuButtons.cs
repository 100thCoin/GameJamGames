using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuButtons : MonoBehaviour {

	public bool MusicVolSlider;
	public bool VoiceVolSlider;

	public Transform Knob;
	public TextMesh PercentageText;
	public Transform T1;
	public Transform T2;

	public bool MouseInside;
	public bool ClickedInsideCol;
	public Camera Cam;

	public bool DisableSubs;
	public bool InvertMousewheel;
	public bool CheckActive;

	public SpriteRenderer CheckSR;
	public Sprite Enabled;
	public Sprite Disabled;

	public bool BackToTitle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyUp(KeyCode.Mouse0)) {
			ClickedInsideCol = false;
		}

		if (MouseInside || ClickedInsideCol) {

			if (MusicVolSlider) {
				if (Input.GetKeyDown(KeyCode.Mouse0)) {
					ClickedInsideCol = true;
				}
				if (ClickedInsideCol && Input.GetKey (KeyCode.Mouse0)) {

					float Mousepos = Input.mousePosition.x;
					float T1x = Cam.WorldToScreenPoint(T1.transform.position).x;
					float T2x = Cam.WorldToScreenPoint(T2.transform.position).x;
					float diff = T2x - T1x;
					Mousepos = (Mousepos-T1x)/diff;
					Main.Data.MusicVolume = Mathf.Clamp01(Mousepos);
					Knob.transform.position = new Vector3(Mathf.Lerp(T1.transform.position.x,T2.transform.position.x,Mathf.Clamp01(Mousepos)),Knob.transform.position.y,Knob.transform.position.z);
					PercentageText.text = "" + Mathf.Round(Mathf.Clamp01(Mousepos)*100) + "%";

				}

			}
			else if (VoiceVolSlider) {
				if (Input.GetKeyDown(KeyCode.Mouse0)) {
					ClickedInsideCol = true;
				}
				if (ClickedInsideCol && Input.GetKey (KeyCode.Mouse0)) {

					float Mousepos = Input.mousePosition.x;
					float T1x = Cam.WorldToScreenPoint(T1.transform.position).x;
					float T2x = Cam.WorldToScreenPoint(T2.transform.position).x;
					float diff = T2x - T1x;
					Mousepos = (Mousepos-T1x)/diff;
					Main.Data.VOVolume = Mathf.Clamp01(Mousepos);
					Knob.transform.position = new Vector3(Mathf.Lerp(T1.transform.position.x,T2.transform.position.x,Mathf.Clamp01(Mousepos)),Knob.transform.position.y,Knob.transform.position.z);
					PercentageText.text = "" + Mathf.Round(Mathf.Clamp01(Mousepos)*100) + "%";

				}

			}

		}

		if (MouseInside) {

			if (DisableSubs) {
				if (Input.GetKeyDown (KeyCode.Mouse0)) {
					CheckActive = !CheckActive;
					Main.Data.DisableSubtitles = CheckActive;
					CheckSR.sprite = CheckActive ? Enabled : Disabled;
				}
			}
			if (InvertMousewheel) {
				if (Input.GetKeyDown (KeyCode.Mouse0)) {
					CheckActive = !CheckActive;
					Main.Data.InvertMousewheel = CheckActive;
					CheckSR.sprite = CheckActive ? Enabled : Disabled;

				}
			}
			if (BackToTitle) {

				if (Input.GetKeyDown (KeyCode.Mouse0)) {
					SuperMain SM = GameObject.Find ("SuperMain").GetComponent<SuperMain> ();

					SM.LoadTitle ();
				}
			}

		}


		MouseInside = false;
	}


	void OnMouseOver () {

		MouseInside = true;
	}

}
