using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseVolumeSlider : MonoBehaviour {

	public Transform Right;
	public Transform Left;
	public Transform Slider;
	public TextMesh TM;

	public bool Dragging;

	public bool MouseOver;
	public int Grace;

	public Camera Cam;

	public bool SFX;


	void OnMouseOver () {
		MouseOver = true;
		Grace = 3;
	}

	// Use this for initialization
	void Start () {

		float vol = SFX ? Global.Dataholder.SFXVolume : Global.Dataholder.MusicVolume;
		Vector3 Mid = new Vector3(Mathf.Lerp(Left.position.x,Right.position.x,vol),Slider.position.y,Slider.position.z);

		Slider.transform.position = Mid;
		TM.text = "" + Mathf.Round (vol*100) + "%";

	}

	// Update is called once per frame
	void Update () {





		if (MouseOver) {
			if (Input.GetKeyDown (KeyCode.Mouse0)) {
				Dragging = true;
			}
		} 

		if (Dragging) {
			if (!Input.GetKey (KeyCode.Mouse0)) {

				Dragging = false;
				return;

			}

			Vector2 MousePos = Cam.ScreenToWorldPoint (Input.mousePosition);
			Slider.transform.position = new Vector3 (MousePos.x, Slider.transform.position.y, Slider.transform.position.z);
			if (Slider.transform.position.x > Right.transform.position.x) {
				Slider.transform.position = new Vector3 (Right.transform.position.x, Slider.transform.position.y, Slider.transform.position.z);
			}
			if (Slider.transform.position.x < Left.transform.position.x) {
				Slider.transform.position = new Vector3 (Left.transform.position.x, Slider.transform.position.y, Slider.transform.position.z);
			}

			float Tval = Mathf.Clamp01((Slider.transform.position.x - Left.transform.position.x) / (Right.transform.position.x - Left.transform.position.x));

			TM.text = "" + Mathf.Round (Tval*100) + "%";

			if (SFX) {
				
				Global.Dataholder.SFXVolume = Tval;
			} else {
				Global.Dataholder.MusicVolume = Tval;

			}
		}


		if(Grace< 0)
		{
			MouseOver = false;
		}
		Grace--;
	}
}