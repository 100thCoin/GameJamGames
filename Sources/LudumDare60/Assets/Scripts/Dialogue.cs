using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour {

	public bool StartVictory;
	public bool StartDoOnce;

	public bool Active;
	public float timer;
	public bool EndWithAnotherDialogue;
	public Dialogue ContinueDialogue;

	public bool EndWithCardUnlock;
	public int UnlockThisCard;

	public bool EndWithHandReveal;
	public Dialogue[] HandTree;


	public AudioClip AC;

	public bool BeenHereBefore;

	public Dialogue ConfusedResponse;

	public bool Generic;

	public int UniqueSillyBusiness_End;

	public Room PostDiaTransition;

	public Animator Anim;
	public RuntimeAnimatorController During_RAC;
	public RuntimeAnimatorController After_RAC;

	public bool Unskippable; // bonk

	public bool Battle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Active) {

			if (StartVictory && !StartDoOnce) {
				StartDoOnce = true;
				Global.Dataholder.LoadRoom (PostDiaTransition);
				Anim.runtimeAnimatorController = null;
				Anim.runtimeAnimatorController = During_RAC;
				Global.Dataholder.WonGame = true;

			}

			if (Anim != null && timer < 0.1f && !StartVictory) {
				Anim.runtimeAnimatorController = During_RAC;
			}

			timer += Time.deltaTime;
			if (AC == null ||  timer > AC.length || (((BeenHereBefore || Super.Dataholder.PreviousVictory_CanSkipCutscenes) && (timer > 0.1f && Input.GetKeyDown(KeyCode.Mouse0))) && !Unskippable && !Global.Dataholder.PAUSED)) {
				EndDialogue ();
			}
		} else {
			timer = 0;
		}

	}

	void EndDialogue()
	{
		Active = false;
		timer = 0;

		if (Anim != null && !StartVictory) {
			Anim.runtimeAnimatorController = After_RAC;
		}

		Global.Dataholder.DialogueSource.clip = null;
		Global.Dataholder.DialogueSource.Stop ();

		Cursor.SetCursor (null, Vector2.zero, CursorMode.ForceSoftware);
		BeenHereBefore = true;


		if (EndWithAnotherDialogue && !EndWithCardUnlock) {
			ContinueDialogue.StartDialogue ();
		} else if (EndWithCardUnlock) {
			Global.Dataholder.PlayTitleCardChime = true;
			Global.Dataholder.CamPosBeforeTitleCard = Global.Dataholder.MainCam.transform.position;
			Global.Dataholder.MainCam.transform.position = new Vector3 (0, 20, -10);
			Global.Dataholder.TitleCardTM.text = Global.Dataholder.CardStringList [UnlockThisCard].Replace ('#', '\n');
			Global.Dataholder.UnlockedCards [UnlockThisCard] = true;
			Global.Dataholder.TitleCardID = UnlockThisCard;
			Global.Dataholder.TitleCardTimer = 0;
			Global.Dataholder.TitleCardVisible = true;
			// TODO play a ding sound effect or something.

			if (EndWithAnotherDialogue) {
				Global.Dataholder.QueuedDialogueAfterTitleCard = ContinueDialogue;
			}

		} else if (EndWithHandReveal && UniqueSillyBusiness_End != 25) {
			if (Battle) {
				Global.Dataholder.HandInBattle = true; // This never needs to be set false afterwards.
				Global.Dataholder.BossMan.NEXT = true;
			}
			Global.Dataholder.DeckMain.CreateCardList ();
			Global.Dataholder.CardsOpen = true;
			Global.Dataholder.HandTree = HandTree;
			Global.Dataholder.ConfusedDialogue = ConfusedResponse;

		} else if (!Generic) {
			// END DIALOGUE MODE
			Global.Dataholder.DialogeMode = false;
		}

		else if (Generic) {
			if (Global.Dataholder.ConfusedDialogue != null) {
				Global.Dataholder.ConfusedDialogue.StartDialogue ();
			} else {
				Global.Dataholder.DialogeMode = false;
			}
		}

		if (UniqueSillyBusiness_End != 0) {
			DoUniqueSillyBusiness (UniqueSillyBusiness_End);
		}

		if (!EndWithAnotherDialogue) {
			Global.Dataholder.Music.gameObject.SetActive (true);

		}

	}

	public void StartDialogue()
	{
		Global.Dataholder.DialogueSource.clip = AC;
		Global.Dataholder.DialogueSource.Play ();
		Active = true;
		Global.Dataholder.DialogeMode = true;
		Cursor.SetCursor (Global.Dataholder.CustomCursors [7].Tex.texture, Global.Dataholder.CustomCursors [7].HotSpot, CursorMode.ForceSoftware);

	}

	void DoUniqueSillyBusiness(int SillyValue)
	{

		switch (SillyValue) {
		case 1:
			{
				// Look over there stun guard.
				Global.Dataholder.Silly_StunlockGuard = true;
				Global.Dataholder.PlayTitleCardChime = true;

			}
			break;
		case 3: // enter barberdoor
			{
				Global.Dataholder.LoadRoom (PostDiaTransition);
				Global.Dataholder.GenericUnlocks [1] = true;
			}
			break;
		case 4: // SCENE MISSING (Boat Crash)
			{
				Global.Dataholder.LoadRoom (PostDiaTransition);
				Global.Dataholder.GenericUnlocks [3] = true;

				Global.Dataholder.CamPosBeforeTitleCard = Global.Dataholder.MainCam.transform.position;
				Global.Dataholder.MainCam.transform.position = new Vector3 (0, 20, -10);
				Global.Dataholder.TitleCardTM.text = "- SCENE MISSING -\n\nBoat Crash";
				Global.Dataholder.TitleCardID = 0;
				Global.Dataholder.TitleCardTimer = 0;
				Global.Dataholder.TitleCardVisible = true;
				// TODO play a ding sound effect or something.
				Global.Dataholder.PlayTitleCardChime = true;

				Global.Dataholder.QueuedDialogueAfterTitleCard = ContinueDialogue; //Are you okay?

			}
			break;
		case 5: // Boat Crash
			{
				Global.Dataholder.LoadRoom (PostDiaTransition);
				Global.Dataholder.CamPosBeforeTitleCard = Global.Dataholder.MainCam.transform.position;
				Global.Dataholder.MainCam.transform.position = new Vector3 (0, 20, -10);
				// TODO play a ding sound effect or something.

			}
			break;
		case 6: // Tutorial knob
			{
				Global.Dataholder.GenericUnlocks [6] = true;
			}
			break;
		case 7: // Tutorial Hall
			{
				Global.Dataholder.LoadRoom (PostDiaTransition);
				Global.Dataholder.GenericUnlocks [7] = true;
			}
			break;

		case 10: // Key1 unlock
			{
				Global.Dataholder.GenericUnlocks [10] = true;
				Global.Dataholder.LoadRoom (PostDiaTransition);
			}
			break;
		case 11: // Key2 unlock
			{
				Global.Dataholder.GenericUnlocks [11] = true;
			}
			break;
		case 12: // Key3 unlock
			{
				Global.Dataholder.GenericUnlocks [12] = true;
			}
			break;
		case 16: // Opening_Look!
			{
				Global.Dataholder.GenericUnlocks [16] = true;
			}
			break;
		case 25: // tutorial first interaction!
			{
				if (!ContinueDialogue.BeenHereBefore) {
					ContinueDialogue.StartDialogue ();
				} else {
					Global.Dataholder.DeckMain.CreateCardList ();
					Global.Dataholder.CardsOpen = true;
					Global.Dataholder.HandTree = HandTree;
					Global.Dataholder.ConfusedDialogue = ConfusedResponse;
				}

			}
			break;
		case 26: // captcha hack
			{
				Global.Dataholder.GenericUnlocks [26] = true;

			}
			break;
		case 35: // Start fight.
			{
				Global.Dataholder.HandInBattle = true;
				Global.Dataholder.LoadRoom (PostDiaTransition);


			}
			break;
		case 99: // Start acapella.
			{
				Global.Dataholder.Acapella.gameObject.SetActive (true);


			}
			break;
		}

	}

}
