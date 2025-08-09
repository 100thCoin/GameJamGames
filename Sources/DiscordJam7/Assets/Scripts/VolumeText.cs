using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeText : MonoBehaviour {

	public TextMesh Vol;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Vol.text = "+ / - buttons to\nchange volume:" + Mathf.Round(VolumeMna.Volume*100f) + "%";
		if(Input.GetKey(KeyCode.Equals))
		{
			VolumeMna.Volume+=Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.Minus))
		{
			VolumeMna.Volume-=Time.deltaTime;
		}
		VolumeMna.Volume = Mathf.Clamp01(VolumeMna.Volume);
	}
}
