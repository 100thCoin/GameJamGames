using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuButtons : MonoBehaviour {

	public bool SFX;
	public bool Music;
	public bool VA;
	public bool SubtitleCheckbox;

	public bool ReturnToTitle;

	public float CurentSliderValue;
	public bool CurrentCheckboxValue;

	public GameObject Knob;
	public GameObject Left;
	public GameObject Right;
	public TextMesh PercentageTM;

	public Sprite Unchecked;
	public Sprite Checked;
	public SpriteRenderer Checkbox;

	public bool MouseInside;
	public bool ClickedInsideCol;

	public bool ResumeMusic;

	public Camera Cam;

	public bool TitlePlay;
	public bool TitleQuit;
	public bool TitleCreds;
	public bool TitleBackToTitle;
	public bool TitleVolume;

	void Start()
	{
		if (TitleVolume || Music) {
			float Mousepos = SuperMain.M.MusicVolume;

			Knob.transform.position = new Vector3(Mathf.Lerp(Left.transform.position.x,Right.transform.position.x,Mathf.Clamp01(Mousepos)),Knob.transform.position.y,Knob.transform.position.z);
			Knob.transform.position = new Vector3 (Mathf.Round (Knob.transform.position.x * 16) / 16f, Knob.transform.position.y, Knob.transform.position.z);
			PercentageTM.text = "" + Mathf.Round(Mathf.Clamp01(Mousepos)*100);
		}
		if (SFX) {
			float Mousepos = SuperMain.M.SFXVolume;

			Knob.transform.position = new Vector3(Mathf.Lerp(Left.transform.position.x,Right.transform.position.x,Mathf.Clamp01(Mousepos)),Knob.transform.position.y,Knob.transform.position.z);
			Knob.transform.position = new Vector3 (Mathf.Round (Knob.transform.position.x * 16) / 16f, Knob.transform.position.y, Knob.transform.position.z);
			PercentageTM.text = "" + Mathf.Round(Mathf.Clamp01(Mousepos)*100);
		}
		if (VA) {
			float Mousepos = SuperMain.M.VAVolume;

			Knob.transform.position = new Vector3(Mathf.Lerp(Left.transform.position.x,Right.transform.position.x,Mathf.Clamp01(Mousepos)),Knob.transform.position.y,Knob.transform.position.z);
			Knob.transform.position = new Vector3 (Mathf.Round (Knob.transform.position.x * 16) / 16f, Knob.transform.position.y, Knob.transform.position.z);
			PercentageTM.text = "" + Mathf.Round(Mathf.Clamp01(Mousepos)*100);
		}
		if (SubtitleCheckbox) {
			Checkbox.sprite = !SuperMain.M.DisableSubtitles ? Checked : Unchecked;


		}

	}


	void Update () {

		if (Input.GetKeyUp(KeyCode.Mouse0)) {
			ClickedInsideCol = false;
		}

		if (MouseInside || ClickedInsideCol) {

			if (TitleVolume) {
				if (Input.GetKeyDown(KeyCode.Mouse0)) {
					ClickedInsideCol = true;
				}
				if (ClickedInsideCol && Input.GetKey (KeyCode.Mouse0)) {

					float Mousepos = Input.mousePosition.x;
					float T1x = Cam.WorldToScreenPoint(Left.transform.position).x;
					float T2x = Cam.WorldToScreenPoint(Right.transform.position).x;
					float diff = T2x - T1x;
					Mousepos = (Mousepos-T1x)/diff;
					SuperMain.M.MusicVolume = Mathf.Clamp01(Mousepos);
					SuperMain.M.SFXVolume = Mathf.Clamp01(Mousepos);
					SuperMain.M.VAVolume = Mathf.Clamp01(Mousepos);

					Knob.transform.position = new Vector3(Mathf.Lerp(Left.transform.position.x,Right.transform.position.x,Mathf.Clamp01(Mousepos)),Knob.transform.position.y,Knob.transform.position.z);
					Knob.transform.position = new Vector3 (Mathf.Round (Knob.transform.position.x * 16) / 16f, Knob.transform.position.y, Knob.transform.position.z);
					PercentageTM.text = "" + Mathf.Round(Mathf.Clamp01(Mousepos)*100);

				}

			}

			if (Music) {
				if (Input.GetKeyDown(KeyCode.Mouse0)) {
					ClickedInsideCol = true;
				}
				if (ClickedInsideCol && Input.GetKey (KeyCode.Mouse0)) {

					float Mousepos = Input.mousePosition.x;
					float T1x = Cam.WorldToScreenPoint(Left.transform.position).x;
					float T2x = Cam.WorldToScreenPoint(Right.transform.position).x;
					float diff = T2x - T1x;
					Mousepos = (Mousepos-T1x)/diff;
					SuperMain.M.MusicVolume = Mathf.Clamp01(Mousepos);
					Knob.transform.position = new Vector3(Mathf.Lerp(Left.transform.position.x,Right.transform.position.x,Mathf.Clamp01(Mousepos)),Knob.transform.position.y,Knob.transform.position.z);
					Knob.transform.position = new Vector3 (Mathf.Round (Knob.transform.position.x * 16) / 16f, Knob.transform.position.y, Knob.transform.position.z);
					PercentageTM.text = "" + Mathf.Round(Mathf.Clamp01(Mousepos)*100);

				}

			}
			if (SFX) {
				if (Input.GetKeyDown(KeyCode.Mouse0)) {
					ClickedInsideCol = true;
				}
				if (ClickedInsideCol && Input.GetKey (KeyCode.Mouse0)) {

					float Mousepos = Input.mousePosition.x;
					float T1x = Cam.WorldToScreenPoint(Left.transform.position).x;
					float T2x = Cam.WorldToScreenPoint(Right.transform.position).x;
					float diff = T2x - T1x;
					Mousepos = (Mousepos-T1x)/diff;
					SuperMain.M.SFXVolume = Mathf.Clamp01(Mousepos);
					Knob.transform.position = new Vector3(Mathf.Lerp(Left.transform.position.x,Right.transform.position.x,Mathf.Clamp01(Mousepos)),Knob.transform.position.y,Knob.transform.position.z);
					Knob.transform.position = new Vector3 (Mathf.Round (Knob.transform.position.x * 16) / 16f, Knob.transform.position.y, Knob.transform.position.z);
					PercentageTM.text = "" + Mathf.Round(Mathf.Clamp01(Mousepos)*100);

				}

			}
			else if (VA) {
				if (Input.GetKeyDown(KeyCode.Mouse0)) {
					ClickedInsideCol = true;
				}
				if (ClickedInsideCol && Input.GetKey (KeyCode.Mouse0)) {

					float Mousepos = Input.mousePosition.x;
					float T1x = Cam.WorldToScreenPoint(Left.transform.position).x;
					float T2x = Cam.WorldToScreenPoint(Right.transform.position).x;
					float diff = T2x - T1x;
					Mousepos = (Mousepos-T1x)/diff;
					SuperMain.M.VAVolume = Mathf.Clamp01(Mousepos);
					Knob.transform.position = new Vector3(Mathf.Lerp(Left.transform.position.x,Right.transform.position.x,Mathf.Clamp01(Mousepos)),Knob.transform.position.y,Knob.transform.position.z);
					Knob.transform.position = new Vector3 (Mathf.Round (Knob.transform.position.x * 16) / 16f, Knob.transform.position.y, Knob.transform.position.z);
					PercentageTM.text = "" + Mathf.Round(Mathf.Clamp01(Mousepos)*100);

				}

			}

		}

		if (MouseInside) {

			if (SubtitleCheckbox) {
				if (Input.GetKeyDown (KeyCode.Mouse0)) {
					CurrentCheckboxValue = !CurrentCheckboxValue;
					SuperMain.M.DisableSubtitles = CurrentCheckboxValue;
					Checkbox.sprite = CurrentCheckboxValue ? Checked : Unchecked;
				}
			}
			if (ReturnToTitle) {

				if (Input.GetKeyDown (KeyCode.Mouse0)) {

					SuperMain.M.LoadTitle ();
				}
			}

			if (ResumeMusic) {
				if (Input.GetKeyDown (KeyCode.Mouse0)) {
					
					Global.DataHolder.resumeBossMusic ();
					Destroy (gameObject.transform.parent.gameObject);
				}
			}




			if (TitleCreds) {
				if (Input.GetKeyDown (KeyCode.Mouse0)) {
					Cam.transform.position = new Vector3 (0, -50, -100);

				}
			}
			if (TitleBackToTitle) {
				if (Input.GetKeyDown (KeyCode.Mouse0)) {
					Cam.transform.position = new Vector3 (0, 0, -100);

				}
			}
			if (TitleQuit) {
				if (Input.GetKeyDown (KeyCode.Mouse0)) {
					Application.Quit ();
				}
			}
			if (TitlePlay) {
				if (Input.GetKeyDown (KeyCode.Mouse0)) {
					
					TitlePlay = false;
					Global.DataHolder.BeginGame ();
				}
			}


		}


		MouseInside = false;
	}


	void OnMouseOver () {

		MouseInside = true;
	}
}
