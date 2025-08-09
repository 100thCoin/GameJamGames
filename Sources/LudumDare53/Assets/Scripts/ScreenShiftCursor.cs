using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShiftCursor : MonoBehaviour {

	public bool Hover;
	public bool Grab;
	public SpriteRenderer SR;

	public Vector3 Offset;
	public bool DoOnce;

	public float BlinkTimer;
	public SpriteRenderer DragThisAroundSR;
	public bool ShowFirstTimeTutorial;

	public bool BossFight;
	public float BossFightTimer;
	public SpriteRenderer BossCursor;

	public GameObject LeftBoarder;
	public GameObject RightBoarder;
	public float BoarderDistance;
	public SpriteRenderer BossMidLine;
	public GameObject Shockwave;
	public bool WaveOnce;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if ((Global.DataHolder.GameIsPaused || Global.DataHolder.WinGame)) {
			return;
		}

		if (BossFight) {

			if (BossFightTimer == 0) {
				BossCursor.transform.position = Global.DataHolder.BossMan.transform.position + new Vector3(0.5f,-1,0);
				BossCursor.color = new Vector4 (1, 1, 1, 1);
				SR.color = new Vector4 (1, 1, 1, 1);

			}
			BossFightTimer += Time.deltaTime;

			if (BossFightTimer > 0.5 && BossFightTimer < 2.5) {

				float XPos = DataHolder.TwoCurveLerp (BossCursor.transform.position.x, transform.position.x + 0.5f, BossFightTimer - 0.5f, 2);
				float YPos = DataHolder.TwoCurveLerp (BossCursor.transform.position.y, transform.position.y - 1, BossFightTimer - 0.5f, 2);

				BossCursor.transform.position = new Vector3 (XPos, YPos, 0);

				if (BossFightTimer > 2f) {
					SR.color = Color.green;
				}

			}
			if (BossFightTimer > 1 && BossFightTimer < 3) {

				float Board = DataHolder.TwoCurveLerp (BoarderDistance, BoarderDistance - 4,BossFightTimer-1, 2);
				LeftBoarder.transform.localPosition = new Vector3 (-Board, 0, 0);
				RightBoarder.transform.localPosition = new Vector3 (Board, 0, 0);

				BossMidLine.color = new Vector4 (1, 1, 1, BossFightTimer - 2);
			}

			if (BossFightTimer > 3 && BossFightTimer < 5) {

				if (BossFightTimer > 2f) {
					SR.color = new Vector4 (0, 169f / 255f, 1, 1);
					if (!WaveOnce) {
						WaveOnce = true;
						Instantiate (Shockwave, transform.position, transform.rotation, transform.parent);

					}
				}

				float PrevX = BossCursor.transform.position.x;
				float XPos = DataHolder.TwoCurveLerp (BossCursor.transform.position.x, Global.DataHolder.BossMan.transform.position.x+0.5f, BossFightTimer - 3, 2);
				float YPos = BossCursor.transform.position.y;

				BossCursor.transform.position = new Vector3 (XPos, YPos, 0);
				float Diff = BossCursor.transform.position.x - PrevX;
				LeftBoarder.transform.parent.position += new Vector3 (Diff, 0, 0);
				transform.position += new Vector3 (Diff, 0, 0);
				BossCursor.transform.position -= new Vector3 (Diff, 0, 0);

			}

			if (BossFightTimer > 4 && BossFightTimer < 4.5f) {

				BossMidLine.color = new Vector4 (1, 1, 1, 9- BossFightTimer*2);
				BossCursor.color = new Vector4 (1, 1, 1, 9- BossFightTimer*2);


			}
			if (BossFightTimer > 4.5f) {
				//Instantiate (Shockwave, transform.position, transform.rotation, transform.parent);

				BossMidLine.color = new Vector4 (1, 1, 1, 0);
				Global.DataHolder.DeactivateScreenShiftMode ();
				transform.position = new Vector3 (0, 0, -500);
				Global.DataHolder.BossMan.DoOnce = false;
				WaveOnce = false;
			}



		}




		if (!BossFight) {

			if (Global.DataHolder.LevelID <= 1) {
				ShowFirstTimeTutorial = true;
			} else {
				ShowFirstTimeTutorial = false;

			}

			if (Hover) {
				SR.color = Color.green;
				DragThisAroundSR.color = Color.green;

				if (Input.GetKeyDown (KeyCode.Mouse0)) {
					Grab = true;
					Instantiate (Shockwave, transform.position, transform.rotation, transform.parent);

				}
			}
			if (Grab) {
				SR.color = new Vector4 (0, 169f / 255f, 1, 1);
				DragThisAroundSR.color = new Vector4 (0, 0, 0, 0);

				if (Input.GetKeyUp (KeyCode.Mouse0)) {
					Global.DataHolder.DeactivateScreenShiftMode ();
					Grab = false;
					//Instantiate (Shockwave, transform.position, transform.rotation, transform.parent);

					transform.position = new Vector3 (0, 0, -500);
					return;
				}

				Vector3 MousePos = Global.DataHolder.ForceCamera.ScreenToWorldPoint (Input.mousePosition);
				if (!DoOnce) {
					DoOnce = true;
					Offset = transform.position - MousePos;
				}

				transform.position = MousePos + Offset;
				transform.position = new Vector3 (Mathf.Round (transform.position.x * 16) / 16f, Mathf.Round (transform.position.y * 16) / 16f, 0);

				Global.DataHolder.UpdateScreenBounds ();
			}

			if (Hover == false && Grab == false) {
				BlinkTimer += Time.deltaTime * 8;
				Color Mod = new Vector4 (Mathf.Lerp (1, 1, Mathf.Sin (BlinkTimer) * 0.5f + 0.5f), Mathf.Lerp (1, 0.5f, Mathf.Sin (BlinkTimer) * 0.5f + 0.5f), Mathf.Lerp (1, 0.5f, Mathf.Sin (BlinkTimer) * 0.5f + 0.5f), 1);
				SR.color = Mod;
				DragThisAroundSR.color = Mod;
			}
			if (!ShowFirstTimeTutorial) {
				DragThisAroundSR.color = new Vector4 (0, 0, 0, 0);
			}
			Hover = false;
		}
	}


	void OnMouseOver () {
		if (!BossFight) {
			Hover = true;
		}
	}
}
