using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolTransition : MonoBehaviour {

	public Camera LastRoomCam;
	public Camera NextRoomCam;
	public Material Mat; 
	public bool Active;
	public float Timer;
	public Camera OverCam;

	public bool Vertical;

	public bool OneFrameDelay;

	void OnEnable()
	{
		LastRoomCam.transform.position = new Vector3 (0, 0, -100);
		Global.DataHolder.MainCamera.transform.position = new Vector3 (0, 0, -100);
		NextRoomCam.transform.position = new Vector3 (0, -200, -100);
		LastRoomCam.Render ();
		NextRoomCam.Render ();
		OverCam.Render ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Active) {
			if (Timer != 0) {
				OverCam.enabled = true;
			}
			Timer += Time.deltaTime*0.3333f;
			float T = DataHolder.TwoCurveLerp (0, 1, Timer, 1);
			if (!OneFrameDelay) {

				if (Vertical) {

					Global.DataHolder.Player.transform.position = new Vector3 (0, 0, -600);

					LastRoomCam.transform.position = new Vector3 (0, T * -16, -100);
					Global.DataHolder.MainCamera.transform.position = new Vector3 (0, T * -16, -100);
					NextRoomCam.transform.position = new Vector3 (0, -200 + 16 - T * 16, -100);

					SuperMain.M.VAMult = 1 - T;
					SuperMain.M.MusicMult = Mathf.Max (SuperMain.M.MusicMult, T);

					Mat.SetFloat ("_Horizontal", 1);
					Mat.SetFloat ("_XOffset", DataHolder.TwoCurveLerp (1.1f, -0.6f, T, 1));


				} else {

					Mat.SetFloat ("_Horizontal", 0);


					LastRoomCam.transform.position = new Vector3 (T * 16, 0, -100);
					Global.DataHolder.MainCamera.transform.position = new Vector3 (T * 16, 0, -100);
					NextRoomCam.transform.position = new Vector3 (-16 + T * 16, -200, -100);

					SuperMain.M.VAMult = 1 - T;
					SuperMain.M.MusicMult = Mathf.Max (SuperMain.M.MusicMult, T);

					Mat.SetFloat ("_XOffset", DataHolder.TwoCurveLerp (1.1f, -0.6f, T, 1));

				}
				if (Timer > 1) {
					Global.DataHolder.MainCamera.transform.position = new Vector3 (0, 0, -100);
					Global.DataHolder.FinalizeRoomTransition ();
					OverCam.enabled = false;
					Global.DataHolder.RemoveCurrentVoiceLine ();
					Global.DataHolder.MainCamera.Render ();
					Global.DataHolder.AltCamera.Render ();
					LastRoomCam.gameObject.SetActive (false);
					NextRoomCam.gameObject.SetActive (false);

					OneFrameDelay = true;
				}
			} else {
				OneFrameDelay = false;
				Active = false;
				Timer = 0;
				Global.DataHolder.TransitionMan.LevelBounds.GetComponent<BoundMatManagerForTransition> ().AssignThisLevelMat ();
				Destroy (Global.DataHolder.TransitionMan.gameObject);


				gameObject.SetActive (false);
				LastRoomCam.gameObject.SetActive (true);
				NextRoomCam.gameObject.SetActive (true);
				Mat.SetFloat ("_XOffset",1.1f);

				OverCam.Render ();

			}
		}
	}
}
