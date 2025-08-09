using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public bool InLargeLevel;
	public float TargetXpos;
	public float PrevPos;
	public float Lerped;

	public float RoomSize;

	public float levelEditorScaleTimer;
	public Camera Cam;

	public bool InCinematic;

	public float pos;

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
			float off = 14 - RoomSize;
			pos = (Global.Dataholder.PMov.transform.position.x+6)/(((RoomSize)*2)+off);
			pos = Mathf.Clamp01 (pos);

			dest = DataHolder.TwoCurveLerp (0, RoomSize, pos, 1);
		}
			
		levelEditorScaleTimer = Mathf.Clamp01 (levelEditorScaleTimer);

		destY = DataHolder.TwoCurveLerp (0, 12, levelEditorScaleTimer, 1);

		dest = DataHolder.TwoCurveLerp (dest, 20, levelEditorScaleTimer, 1);
		transform.position = new Vector3 (dest, 4, -10);


	}
}
