using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour {

	public bool Hexadecimal;
	public bool UpdateScreen;
	public RenderTexture RTScreen;
	public RenderTexture RTScreenSnap;
	public RenderTexture RTScreen_10;
	public RenderTexture RTScreenSnap_10;

	public Material ScreenNormal;
	public Material ScreenBW;
	public Material ScreenSnapshot;

	public Camera Cam;
	public Camera SnapshotCam;
	public Vector2 HexPos;
	public Vector2 DecPos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(UpdateScreen)
		{
			UpdateScreen = false;
			if(Hexadecimal)
			{
				Cam.transform.position = new Vector3(HexPos.x,HexPos.y,-10);
				SnapshotCam.transform.position = new Vector3(HexPos.x,HexPos.y,-10);
				ScreenNormal.SetTexture("_MainTex",RTScreen);
				ScreenBW.SetTexture("_MainTex",RTScreen);
				ScreenSnapshot.SetTexture("_MainTex",RTScreenSnap);
				Cam.targetTexture = RTScreen;
				SnapshotCam.targetTexture = RTScreenSnap;
				Cam.orthographicSize = 8;
				SnapshotCam.orthographicSize = 8;
			}
			else
			{
				Cam.transform.position = new Vector3(DecPos.x,DecPos.y,-10);
				SnapshotCam.transform.position = new Vector3(DecPos.x,DecPos.y,-10);
				ScreenNormal.SetTexture("_MainTex",RTScreen_10);
				ScreenBW.SetTexture("_MainTex",RTScreen_10);
				ScreenSnapshot.SetTexture("_MainTex",RTScreenSnap_10);
				Cam.targetTexture = RTScreen_10;
				SnapshotCam.targetTexture = RTScreenSnap_10;
				Cam.orthographicSize = 5;
				SnapshotCam.orthographicSize = 5;
			}
		}

	}
}
