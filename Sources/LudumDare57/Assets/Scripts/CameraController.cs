using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform Target;
	public bool Boss1Camera;
	public Vector3 BossCam;
	public bool MapView;
	public float MapViewTransitionTimer;
	// Use this for initialization
	void Start () {
		
	}
	public Vector3 TargetPos;

	void FixedUpdate () {

		if (Boss1Camera) {
			BossCam = (BossCam * 10 + (Target.transform.position + new Vector3(0,8,0))) / 11f;
			TargetPos = new Vector3 (BossCam.x/3f, BossCam.y, -100);
		} else {
			TargetPos = (TargetPos * 10 + Target.transform.position) / 11f;
			TargetPos = new Vector3 (TargetPos.x, TargetPos.y, -100);
		}

		Vector3 MapPos = new Vector3 (0, -190, -100);

		if (Input.GetKey (KeyCode.M)) {
			MapViewTransitionTimer += Time.deltaTime*2f;
		} else {
			MapViewTransitionTimer -= Time.deltaTime*2f;
		}
		MapViewTransitionTimer = Mathf.Clamp01 (MapViewTransitionTimer);
		transform.position = new Vector3 (DataHolder.TwoCurveLerp (TargetPos.x, MapPos.x, MapViewTransitionTimer, 1), DataHolder.TwoCurveLerp (TargetPos.y, MapPos.y, MapViewTransitionTimer, 1), -100);
		Global.Dataholder.MainCamera.orthographicSize = DataHolder.TwoCurveLerp (16, 200, MapViewTransitionTimer, 1);
	}
}
