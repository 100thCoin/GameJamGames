using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceSlot : MonoBehaviour {
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

	public bool SpawnPlayer;
	public float SpawnTimer;
	public bool WinCloseOnce;
	public SpriteRenderer Void;
	public GameObject BGWalls;
	public bool Level1Delay;

	public bool NoAudio;

	// Use this for initialization
	void Start () {



	}


	// Update is called once per frame
	void Update () {


		float Dist = (transform.position - Global.DataHolder.Player.transform.position).magnitude;

		if (IsOpened) {
			if (Dist > 3.5f) {
				if (!Open) {
					Close = true;
				}
			}
		}


		if (SpawnPlayer) {

			if (SpawnTimer == 0) {
				Open = true;
				Global.DataHolder.Player.RB.isKinematic = true;
				Global.DataHolder.Player.SR.sortingOrder = -11;
				Global.DataHolder.Player.WSR.sortingOrder = -11;
				Global.DataHolder.Player.transform.position = transform.position + new Vector3 (0, 2, 0);
			}
			SpawnTimer += Time.deltaTime;
			if (SpawnTimer > 0.3f) {
				Global.DataHolder.Player.CutsceneLock = false;

				Global.DataHolder.Player.RB.isKinematic = false;
			}
			if (Level1Delay) {
				if (SpawnTimer > 1.34f) {
					Global.DataHolder.Player.SR.sortingOrder = 2;
					Global.DataHolder.Player.WSR.sortingOrder = -1;

				}
				if (SpawnTimer > 1.8f) {
					SpawnPlayer = false;
				}
			} else {
				if (SpawnTimer > 0.64f) {
					Global.DataHolder.Player.SR.sortingOrder = 2;
					Global.DataHolder.Player.WSR.sortingOrder = -1;

				}
				if (SpawnTimer > 1) {
					SpawnPlayer = false;
				}
			}
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
				if (!NoAudio) {
					Instantiate (Global.DataHolder.SFX_OpenChute, transform.position, transform.rotation, transform.parent);
				}
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
				}
			}
			SR.sprite = Sprites [newframe];
			SRAlt.sprite = SpritesAlt [newframe];
		}

	}
}
