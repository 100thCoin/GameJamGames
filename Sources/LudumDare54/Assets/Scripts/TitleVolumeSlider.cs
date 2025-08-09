using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleVolumeSlider : MonoBehaviour {

	public Transform Top;
	public Transform Bottom;
	public Transform Slider;
	public TextMesh TM;

	public bool Dragging;

	public bool MouseOver;
	public int Grace;

	public Camera Cam;

	void OnMouseOver () {
		MouseOver = true;
		Grace = 3;
	}

	// Use this for initialization
	void Start () {

		float vol = Global.Dataholder.MusicVolume;
		Vector3 Mid = new Vector3(Slider.transform.position.x, Mathf.Lerp(Bottom.position.y,Top.position.y,vol),Slider.position.z);

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
			Slider.transform.position = new Vector3 (Slider.transform.position.x, MousePos.y, Slider.transform.position.z);
			if (Slider.transform.position.y > Top.transform.position.y) {
				Slider.transform.position = new Vector3 (Slider.transform.position.x, Top.transform.position.y, Slider.transform.position.z);
			}
			if (Slider.transform.position.y < Bottom.transform.position.y) {
				Slider.transform.position = new Vector3 (Slider.transform.position.x, Bottom.transform.position.y, Slider.transform.position.z);
			}

			float Tval = Mathf.Clamp01((Slider.transform.position.y - Bottom.transform.position.y) / (Top.transform.position.y - Bottom.transform.position.y));

			TM.text = "" + Mathf.Round (Tval*100) + "%";

			Global.Dataholder.MusicVolume = Tval;
			Global.Dataholder.SFXVolume = Tval;

		}


		if(Grace< 0)
		{
			MouseOver = false;
		}
		Grace--;
	}
}
