using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSlider : MonoBehaviour {

	public AudioSource AS;
	public float Volume;
	public Camera Cam;
	public DataHolder Main;
	void OnMouseOver()
	{
		if(Input.GetKey(KeyCode.Mouse0))
		{
			transform.position = Cam.ScreenToWorldPoint(Input.mousePosition);
			transform.position = new Vector3(transform.position.x,-6.54f,1);
			if(transform.position.x > 2f)
			{
				transform.position = new Vector3(2f,transform.position.y,1);
			}
			if(transform.position.x < -2f)
			{
				transform.position = new Vector3(-2f,transform.position.y,1);
			}
			Volume = transform.localPosition.x + 0.5f;
			AS.volume = Volume;
			Main.Volume = Volume;
		}

	}
}
