using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour {

	public int Grace;
	public BoxCollider Box;

	public int CustomCursor;
	public bool GoToRoom;
	public Room RoomToGoTo;

	public bool RequiresGameWatchView;

	public bool Locked;
	public int LockedBehindArbitraryCheckID;
	public GameObject LockedVoiceClipIfInteracted;

	public bool Grab;
	public bool SneakyGrab;
	public int ItemID;
	public SpriteRenderer GameWatchObjectToUpdate;
	public SpriteRenderer ObjectToUpdate;

	public GameObject VoiceClip;
	public bool Talk;

	public bool OneTimeUse;

	public bool SpecialVoiceLineOnFirstUse;
	public GameObject FirstUseVoice;
	public bool ScheduledEvent;

	public bool RequiresItem;
	public int RequiredItem;

	public GameObject[] ItemSpeeches;

	public bool HideAfterItemSpeech;

	public int RemovesItem = -1;
	public int TradeForItem = -1;
	public bool Jump;
	public float JumpCooldown;

	public SpriteRenderer TriviaIdle;
	public Animator TriviaJump;
	public RuntimeAnimatorController RAC_TriviaJump;
	public GameObject JumpAnim;
	public bool jumping;
	public FinalFight FightMan;
	public bool WinGame;
	public bool LoseGame;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Box != null && GoToRoom) {

			Box.enabled = !Global.Dataholder.GameWatchView;

		}

		if (Grace <= 0) {
			if (Grace == 0) {
				Cursor.SetCursor (null, Vector2.zero, CursorMode.ForceSoftware);
				//print ("RemoveCursor");
			}
		} else {

			if (Input.GetKeyDown (KeyCode.Mouse0)) {
				Interact ();
			}

		}
		Grace--;
		JumpCooldown -= Time.deltaTime;
		if (ScheduledEvent && Global.Dataholder.CurrentVoiceClip == null) {
			ScheduledEvent = false;
			Interact ();
		}

		if (jumping) {
			if (JumpCooldown < 0) {
				jumping = false;
				TriviaIdle.enabled = true;
				JumpAnim.SetActive (false);
			}


		}

	}

	void Interact()
	{

		if (Global.Dataholder.ItemInHands != -1) {
			if (ItemSpeeches.Length > 0) {

				if (ItemSpeeches [Global.Dataholder.ItemInHands] != null) {
					Instantiate (ItemSpeeches [Global.Dataholder.ItemInHands], transform.position, transform.rotation, transform.parent);
					if (RemovesItem == Global.Dataholder.ItemInHands) {
						Global.Dataholder.Inventory [Global.Dataholder.ItemInHands] = false;
						if (TradeForItem != -1) {
							Global.Dataholder.Inventory [TradeForItem] = true;
						}
					}
					Global.Dataholder.ItemInHands = -1;
					Global.Dataholder.GameWatchView = false;

					if (HideAfterItemSpeech) {
						Destroy (gameObject);
					}
				} else {
					Instantiate (Global.Dataholder.VA_ThatDidntDoAnything, transform.position, transform.rotation, transform.parent);
				}
			} else {
				Instantiate (Global.Dataholder.VA_ThatDidntDoAnything, transform.position, transform.rotation, transform.parent);
			}
			return;		
		}


		if (Locked) {
			if (Global.Dataholder.ArbitraryChecks [LockedBehindArbitraryCheckID] || Global.Dataholder.DebugMode) {
				Locked = false;
			}
		}

		if (!Locked) {
			if (SpecialVoiceLineOnFirstUse && !Locked) {
				SpecialVoiceLineOnFirstUse = false;
				ScheduledEvent = true;
				VoiceClip V = Instantiate (FirstUseVoice, transform.position, transform.rotation, transform.parent).GetComponent<VoiceClip>();
				Global.Dataholder.CurrentVoiceClip = V;
			} else {
				if (GoToRoom) {
					Global.Dataholder.MainCamera.transform.position = new Vector3 (RoomToGoTo.transform.position.x, RoomToGoTo.transform.position.y, Global.Dataholder.MainCamera.transform.position.z);
					Global.Dataholder.ActiveRoom = RoomToGoTo;
				}
				if (Grab) {
					GameWatchObjectToUpdate.enabled = false;
					ObjectToUpdate.enabled = false;
					Global.Dataholder.Inventory [ItemID] = true;
				}
				if (SneakyGrab) {
					Global.Dataholder.Inventory [ItemID] = true;

				}

				if (WinGame) {
					Global.Dataholder.WinGame ();
				}
				if (LoseGame) {
					Global.Dataholder.LoseGame ();
				}

				if (Talk) {
					Instantiate (VoiceClip, transform.position, transform.rotation, transform.parent);
				}
				if (OneTimeUse) {
					Destroy (gameObject);
				}
				if (Jump && JumpCooldown < 0 && !FightMan.Phase2_Dead) {
					TriviaIdle.enabled = false;
					JumpCooldown = 1.5f;
					JumpAnim.SetActive (true);
					TriviaJump.runtimeAnimatorController = null;
					TriviaJump.runtimeAnimatorController = RAC_TriviaJump;
					jumping = true;
				}

			}
		} else {
			// locked
			Instantiate(LockedVoiceClipIfInteracted, transform.position,transform.rotation,transform.parent);
		}
	}

	void OnMouseOver()
	{
		if (Global.Dataholder.paused) {
			return;
		}

		if (Global.Dataholder.ActiveRoom.gameObject == transform.parent.parent.gameObject && Global.Dataholder.CurrentVoiceClip == null || WinGame || LoseGame) {
			if (Global.Dataholder.GameWatchView && GoToRoom) {
				return;
			}
			if (!Global.Dataholder.GameWatchView && RequiresGameWatchView) {
				return;
			}
			Grace = 2;

			if (RequiresItem && Global.Dataholder.ItemInHands != RequiredItem) {
				return;
			}

			if (!RequiresItem)
			{
				Cursor.SetCursor (Global.Dataholder.CustomCursors [CustomCursor].Tex.texture, Global.Dataholder.CustomCursors [CustomCursor].HotSpot, CursorMode.ForceSoftware);
			}
		}
	}
}
