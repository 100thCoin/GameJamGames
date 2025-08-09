using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtons : MonoBehaviour {

	public Transform Right;
	public Transform Left;
	public Transform Slider;
	public TextMesh TM;

	public bool Dragging;

	public bool MouseOver;
	public int Grace;

	public Camera Cam;

	public bool Music;
	public bool SFX;
	public bool VA;


	void OnMouseOver () {
		MouseOver = true;
		Grace = 3;
	}

	// Use this for initialization
	void Start () {

		float vol = 0;
		if (Music) {
			vol = Super.Dataholder.Volume_Music;
		}
		if (SFX) {
			vol = Super.Dataholder.Volume_SFX;
		}
		if (VA) {
			vol = Super.Dataholder.Volume_Voice;
		}
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


			if (Music) {
				Super.Dataholder.Volume_Music= Tval;
			}
			if (SFX) {
				Super.Dataholder.Volume_SFX= Tval;
			}
			if (VA) {
				Super.Dataholder.Volume_Voice= Tval;
			}
		}


		if(Grace< 0)
		{
			MouseOver = false;
		}
		Grace--;
	}
}