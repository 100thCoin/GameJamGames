using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailSlot : MonoBehaviour {

	public bool Open;
	public bool Close;
	public float Timer;
	public Sprite[] Sprites;
	public SpriteRenderer SR;
	public Sprite[] SpritesAlt;
	public SpriteRenderer SRAlt;

	public GameObject StretchHolder;
	public bool Stretch;
	public float StretchTimer;

	public bool IsOpened;

	public bool GrabPlayer;
	public float GrabTimer;
	public bool WinCloseOnce;
	public SpriteRenderer Void;
	public GameObject BGWalls;
	public bool LevelClear;
	public Vector3 FallOffset;
	public float FallSpeed;
	public bool DoOnceFinal;
	// Use this for initialization
	void Start () {
		
	}

	void FixedUpdate()
	{
		if (GrabPlayer) {

			Global.DataHolder.Player.transform.position = (Global.DataHolder.Player.transform.position * 4 + new Vector3(transform.position.x,transform.position.y-1,0) + FallOffset) / 5f;
			//Global.DataHolder.Player.transform.position = new Vector3(transform.position.x,transform.position.y-1,0) + FallOffset;

		}
	}


	// Update is called once per frame
	void Update () {







		float Dist = (transform.position - Global.DataHolder.Player.transform.position).magnitude;

		if (!LevelClear) {
			if (Dist < 8) {
				if (!IsOpened && !Close) {
					Open = true;
				}
			} else {
				if (IsOpened && !Open) {
					Close = true;
				}
			}
		}
		if (Dist < 1) {

			GrabPlayer = true;
			Global.DataHolder.Player.CutsceneLock = true;
			Global.DataHolder.Player.Anim.runtimeAnimatorController = null;
			Global.DataHolder.Player.WAnim.runtimeAnimatorController = null;
			Global.DataHolder.Player.RB.isKinematic = true;
		}

		if (GrabPlayer) {
			Global.DataHolder.Player.DisableCollision ();
			Global.DataHolder.Player.Anim.runtimeAnimatorController = null;
			Global.DataHolder.Player.WAnim.runtimeAnimatorController = null;
			GrabTimer += Time.deltaTime;
			LevelClear = true;

			if (GrabTimer > 1 && !WinCloseOnce) {
				WinCloseOnce = true;
				Close = true;
				Instantiate (Global.DataHolder.SFX_MailChute, transform.position, transform.rotation, transform.parent);
				Global.DataHolder.Player.SR.sortingOrder = -11;
				Global.DataHolder.Player.WSR.sortingOrder = -11;
			}
			if (GrabTimer > 1.3f) {
				//transition to next stage.
				if (!DoOnceFinal) {
					DoOnceFinal = true;
					Global.DataHolder.NextLevel ();
				}
			}
				
			FallSpeed += Time.deltaTime*10;
			FallOffset += new Vector3 (0, -Time.deltaTime*FallSpeed, 0);

		}


		if (Stretch) {
			Void.sortingOrder = -4;
			StretchTimer += Time.deltaTime * 5;
			StretchHolder.transform.localScale = new Vector3 (DataHolder.ParabolicLerp (1.5f, 1f, StretchTimer, 1), DataHolder.ParabolicLerp (0.75f, 1f, StretchTimer, 1), 1);
			if (StretchTimer > 1) {
				StretchHolder.transform.localScale = new Vector3 (1, 1, 1);
				Stretch = false;
				StretchTimer = 0;
			}
		} else {
			Void.sortingOrder = -12;
			BGWalls.SetActive (false);
		}


		if (Open) {
			IsOpened = true;
			if (Timer == 0) {
				StretchTimer = 0;
				Stretch = true;
				Instantiate (Global.DataHolder.SFX_OpenChute, transform.position, transform.rotation, transform.parent);

			}

			Timer += Time.deltaTime*6;

			int Frame = Mathf.RoundToInt(Timer*10f);
			if (Frame > 11) {
				Frame = 8;
				Timer = 0;
				Open = false;
			}
			SR.sprite = Sprites [Frame];
			SRAlt.sprite = SpritesAlt [Frame];
		}
		if (Close) {
			IsOpened = false;
			Timer += Time.deltaTime*6;

			int Frame = 8- Mathf.RoundToInt(Timer*10f);
			int newframe = Frame;
			if (Frame < 0) {
				if (Frame == -1) {
					newframe = 12;
				} else if (Frame == -2) {
					newframe = 13;
				} else if (Frame == -3) {
					newframe = 14;
				} else {
					newframe = 0;
					Close = false;
					Timer = 0;
					StretchTimer = 0;
					Stretch = true;
					if (GrabPlayer) {

						Global.DataHolder.Player.gameObject.SetActive (false);

					}
				}
			}
			SR.sprite = Sprites [newframe];
			SRAlt.sprite = SpritesAlt [newframe];
		}

	}
}
