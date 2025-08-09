using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueLines
{
	public string Name; //for editor visibility
	public BTM_Cutscene Line1;
	public BTM_Cutscene Line2;
	public BTM_Cutscene Line3;
	public AudioClip VA;
	public bool LeftFace;
	public Sprite FaceIcon;
	public bool SwapCameras;
	public int SubCutscene = -1;

}

[System.Serializable]
public class DialogueTree
{
	public string Name; //for editor
	public int[] LinesIndex;
	//public float[] Durations; //how long is every voice line?
}

public class CutsceneManager : MonoBehaviour {

	public DialogueLines[] Lines;
	public DialogueTree[] Trees;
	public BetterTextMesh[] Rows;
	public int[] TreesPostLevels;
	public int CurrentTree;
	public int CurrentLines;

	public bool Test;

	public bool MainBoxUp;
	public bool RightPortraitUp;
	public bool LeftPortraitUp;
	public float MainBoxTimer;
	public float RightPortraitTimer;
	public float LeftPortraitTimer;

	public Transform MainBox;
	public Transform RightPortrait;
	public Transform LeftPortrait;

	public bool OopsIExplodedCutscene;
	public SpriteRenderer UIBlackout;
	public float OopsIExplodedTimer;
	public CutsceneCam OopsCam;

	public SpriteRenderer PSR_R;
	public SpriteRenderer PSR_L;

	public Sprite[] Portraits;
	public GameObject[] CutscenesToUnload;
	public bool UnloadOnce;

	public AudioSource DialogueVA;

	public bool DoingDialogue;

	public bool ShowAltCamera;
	public GameObject AltCamera;

	public GameObject[] SubCutscenes;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Test)
		{
			Test = false;
			LoadLines (0);
		}

		MainBoxTimer += Time.deltaTime * (MainBoxUp ? 1 : -1) *2;
		RightPortraitTimer += Time.deltaTime * (RightPortraitUp ? 1 : -1) *2;
		LeftPortraitTimer += Time.deltaTime * (LeftPortraitUp ? 1 : -1) *2;
		MainBoxTimer = Mathf.Clamp01 (MainBoxTimer);
		RightPortraitTimer = Mathf.Clamp01 (RightPortraitTimer);
		LeftPortraitTimer = Mathf.Clamp01 (LeftPortraitTimer);


		MainBox.localPosition = new Vector3 (0, DataHolder.ParabolicLerp(-6.5f,-3.5f,MainBoxTimer,1), 0);
		RightPortrait.localPosition = new Vector3 (7.5f, DataHolder.ParabolicLerp(-6.5f,-3.5f,RightPortraitTimer,1), 0);
		LeftPortrait.localPosition = new Vector3 (-7.5f, DataHolder.ParabolicLerp(-6.5f,-3.5f,LeftPortraitTimer,1), 0);


		if (OopsIExplodedCutscene) {

			OopsIExplodedTimer += Time.deltaTime;
			if (OopsIExplodedTimer < 0.5f) {
				UIBlackout.color = new Vector4 (0, 0, 0, OopsIExplodedTimer * 2);
			}
			if (OopsIExplodedTimer >= 0.5f) {
				if (!UnloadOnce) {
					int m = 0;
					while (m < CutscenesToUnload.Length) {
						CutscenesToUnload [m].SetActive(false);

						m++;
					}

					Global.Dataholder.MainCamera.SetActive (true);
					MainBox.localPosition = new Vector3 (0, -6.5f, 0);
					RightPortrait.localPosition = new Vector3 (7.5f, -6.5f, 0);
					LeftPortrait.localPosition = new Vector3 (-7.5f,-6.5f, 0);
					MainBoxTimer = 0;
					RightPortraitTimer = 0;
					LeftPortraitTimer = 0;
					MainBoxUp = false;
					RightPortraitUp = false;
					LeftPortraitUp = false;
					Rows [0].Clear ();
					Rows [1].Clear ();
					Rows [2].Clear ();
					Global.Dataholder.Pmov.transform.position = new Vector3 (0, 0.5f, -28);
					Global.Dataholder.Pmov.RB.velocity = Vector3.zero;
					Global.Dataholder.Pmov.Anim.runtimeAnimatorController = Global.Dataholder.Pmov.RAC_Idle;
					Global.Dataholder.Pmov.grounded = true;
					Global.Dataholder.Pmov.transform.eulerAngles = new Vector3 (0, 0, 0);
					Global.Dataholder.CamMov.PlayerVis.eulerAngles = new Vector3 (0, 0, 0);
					Global.Dataholder.Pmov.Vis.eulerAngles = new Vector3 (0, 0, 0);
					Global.Dataholder.CamMov.transform.eulerAngles = new Vector3 (0, 0, 0);
					Global.Dataholder.CamMov.rotation = Vector2.zero;
					Global.Dataholder.HeroMov.transform.position = Global.Dataholder.HeroSpawnPoint.transform.position;
					Global.Dataholder.PostBoomDialogue = true;
					OopsCam.Reset ();
					Global.Dataholder.Pmov.AtSpawn = true;
					UnloadOnce = true;
					CurrentTree = TreesPostLevels [Global.Dataholder.Level];
					Global.Dataholder.Level++;
					Global.Dataholder.LoadLevel (Global.Dataholder.Level);
					Global.Dataholder.HeroMov.transform.transform.position = Global.Dataholder.HeroSpawnPoint.transform.position;
					Global.Dataholder.MainCamera.GetComponent<Camera> ().fov = 60;
					CurrentLines = 0;
				}
				UIBlackout.color = new Vector4 (0, 0, 0, 2.5f - OopsIExplodedTimer * 2);
				if (OopsIExplodedTimer < 1.5f) {
					OopsCam.Reset ();
				}

			}
			if (OopsIExplodedTimer > 1.5f) {
				OopsCam.GoGoGo = true;
				MainBoxUp = true;

			}
			if (OopsIExplodedTimer > 2) {
				RightPortraitUp = true;
				PSR_R.sprite = Portraits [0];
			}
			if (OopsIExplodedTimer > 2.5f) {
				LoadLines (0);
				OopsIExplodedCutscene = false;
				OopsIExplodedTimer = 0;
				UnloadOnce = false;
				OopsCam.GoGoGo = false;
				OopsCam.Timer = 0;
				DoingDialogue = true;
				doDialogueTimer = 0;
			}
		}

		if (DoingDialogue) {
			doDialogueTimer += Time.deltaTime;
			if (true){//doDialogueTimer > Trees [CurrentTree].Durations [CurrentLines]) {

				if (Super.Dataholder.GetReboundInputDown (KeyCode.Mouse0) || Super.Dataholder.GetReboundInputDown (KeyCode.Space)) {
					CurrentLines++;
					//advance to next one
					if (CurrentLines >= Trees [CurrentTree].LinesIndex.Length) {
						//the dialogue is over.
						Global.Dataholder.ExitingCutscene = true;
						Global.Dataholder.ExitingCutsceneTimer = 0;
						Global.Dataholder.ExitPosS = Global.Dataholder.CamMov.Cam.transform.localPosition;
						Global.Dataholder.ExitAnglesS = Global.Dataholder.CamMov.Cam.transform.localEulerAngles;
						DoingDialogue = false;
						MainBoxUp = false;
						RightPortraitUp = false;
						LeftPortraitUp = false;
						CurrentLines = 0;
					} else {
						LoadLines (Trees [CurrentTree].LinesIndex [CurrentLines]);
					}
				}
			}

		};

	}

	public float doDialogueTimer;

	public void LoadLines(int id)
	{
		Rows [0].Clear ();
		Rows [1].Clear ();
		Rows [2].Clear ();
		Rows [0].CurrentCutscene = new BTM_Cutscene(Lines [id].Line1.Msg,Lines [id].Line1.delay,Lines [id].Line1.typespeed,Lines [id].Line1.Wave,Lines [id].Line1.Outline,Lines [id].Line1.Color);
		Rows [1].CurrentCutscene = new BTM_Cutscene(Lines [id].Line2.Msg,Lines [id].Line2.delay,Lines [id].Line2.typespeed,Lines [id].Line2.Wave,Lines [id].Line2.Outline,Lines [id].Line2.Color);
		Rows [2].CurrentCutscene = new BTM_Cutscene(Lines [id].Line3.Msg,Lines [id].Line3.delay,Lines [id].Line3.typespeed,Lines [id].Line3.Wave,Lines [id].Line3.Outline,Lines [id].Line3.Color);
		RightPortraitUp = !Lines [id].LeftFace;
		LeftPortraitUp = Lines [id].LeftFace;
		if (Lines [id].LeftFace) {
			PSR_L.sprite = Lines [id].FaceIcon;
		} else {
			PSR_R.sprite = Lines [id].FaceIcon;
		}
		if (Lines [id].SwapCameras) {
			ShowAltCamera = !ShowAltCamera;

			Global.Dataholder.MainCamera.SetActive (!ShowAltCamera);
			AltCamera.SetActive (ShowAltCamera);

		}
		int iter = 0;
		while (iter < SubCutscenes.Length) {

			SubCutscenes [iter].SetActive (false);
			iter++;
		}

		if (Lines [id].SubCutscene != -1) {

			SubCutscenes [Lines [id].SubCutscene].SetActive (true);



		}

		DialogueVA.clip = Lines [id].VA;
		DialogueVA.enabled = false;
		DialogueVA.enabled = true;

	}
}
