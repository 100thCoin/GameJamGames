using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneCam : MonoBehaviour {

	public CubicBezierCurve Curve;
	public float Timer;
	public bool GoGoGo;

	public GameObject Cam;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
		if (GoGoGo) {

			Timer += Time.deltaTime * 1.25f;
			Timer = Mathf.Clamp01 (Timer);
			Super.Dataholder.MusicMultiplier = Timer * 0.5f;
			Cam.transform.position = Curve.GetPoint (DataHolder.ParabolicLerp(0,1,Timer,1));
			Cam.transform.localEulerAngles = new Vector3 (DataHolder.ParabolicLerp (0, 10, Timer, 1), DataHolder.ParabolicLerp (20, -40, Timer, 1), 0);
			Global.Dataholder.MainCamera.GetComponent<Camera> ().fov = DataHolder.ParabolicLerp (60, 90, Timer, 1);

		}

	}

	public void Reset()
	{
		Cam.transform.position = Curve.GetPoint (0);
		Cam.transform.localEulerAngles = new Vector3 (0, 20, 0);
		Global.Dataholder.MainCamera.GetComponent<Camera> ().fov = 60;
	}


}
