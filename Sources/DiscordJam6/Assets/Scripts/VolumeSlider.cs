using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSlider : MonoBehaviour {

	public Dataholder Main;

	public Transform Slider;

	public Camera Cam;

	// Use this for initialization
	void Start () {
		Main = GameObject.Find("Main").GetComponent<Dataholder>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDrag()
	{

		Vector3 MousePos = Cam.ScreenToWorldPoint(Input.mousePosition);
		MousePos = new Vector3(MousePos.x,MousePos.y,0);

		Slider.transform.position = new Vector3(MousePos.x,0,0);
		Slider.transform.localPosition = new Vector3(Slider.transform.localPosition.x,0,0);

		if(Slider.localPosition.x >1)
		{
			Slider.transform.localPosition = new Vector3(1,0,0);
		}
		if(Slider.localPosition.x <0)
		{
			Slider.transform.localPosition = new Vector3(0,0,0);
		}

		Main.AudioVolume = Slider.localPosition.x;
	}

}
