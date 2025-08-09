using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public bool InLargeLevel;
	public float TargetXpos;
	public float PrevPos;
	public float Lerped;

	public float levelEditorScaleTimer;
	public Camera Cam;

	public bool InCinematic;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


	}

	void FixedUpdate()
	{
		if (InCinematic) {
			return; //trust the cinematic will move the camera if needed.
		}
		float dest = 0;
		float destY = 0;
		float scale = 16;
		if (InLargeLevel) {
			float pos = (TargetXpos * 5 + Global.DataHolder.PMov.transform.position.x) / 6f;
			pos = (PrevPos * 5 + pos) / 6;
			PrevPos = pos;

			if (pos < -10) {
				pos = -10;
			}
			if (pos > 30) {
				pos = 30;
			}

			pos /= 10f;
			pos += 0.25f;

			pos = Mathf.Clamp01 (pos);

			dest = DataHolder.TwoCurveLerp (0, 40, pos, 1);
		}

		if (Global.DataHolder.InLevelEditor) {
			levelEditorScaleTimer += Time.deltaTime;
		} else {
			levelEditorScaleTimer -= Time.deltaTime;
		}
		levelEditorScaleTimer = Mathf.Clamp01 (levelEditorScaleTimer);

		destY = DataHolder.TwoCurveLerp (0, 12, levelEditorScaleTimer, 1);

		dest = DataHolder.TwoCurveLerp (dest, 20, levelEditorScaleTimer, 1);

		scale = DataHolder.TwoCurveLerp (16, 32, levelEditorScaleTimer, 1);
		Cam.orthographicSize = scale;
		transform.position = new Vector3 (dest, destY, -10);


	}
}
