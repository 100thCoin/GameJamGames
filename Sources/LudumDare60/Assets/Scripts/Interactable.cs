using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

	public int Grace;
	public Collider Col;
	public int CustomCursor;

	public List<int> RequiredChecksToInteract;
	public List<int> RequiredChecksToHide;

	public bool GoToRoom;
	public Room R;

	public bool StartDialogue;
	public Dialogue Dia;


	public int Silly;
	public Dialogue Alt_Dia1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Global.Dataholder.DialogeMode || Global.Dataholder.PAUSED) {
			return;
		}

		Vector3 MousePos = Global.Dataholder.MainCam.ScreenToWorldPoint (Input.mousePosition);
		RaycastHit Hit;
		if (Physics.Raycast (MousePos, Vector3.forward,out Hit,0x100)) {
			if (Hit.collider == Col) {
				Grace = 2;
				Cursor.SetCursor (Global.Dataholder.CustomCursors [CustomCursor].Tex.texture, Global.Dataholder.CustomCursors [CustomCursor].HotSpot, CursorMode.ForceSoftware);
				if (Input.GetKeyDown (KeyCode.Mouse0) && Global.Dataholder.PostCardTimer > 0.05f) {
					Interact();
				}
			}
		}
		if (Grace <= 0 && Grace >= -5) {
			Cursor.SetCursor (null, Vector2.zero, CursorMode.ForceSoftware);
		}
		Grace--;

	}

	void Interact()
	{
		Global.Dataholder.HasThePlayerDoneAnythingYet = true;
		if (Silly == 0) {
			if (GoToRoom) {
				Global.Dataholder.InsideAcapellaRoom = false;

				Global.Dataholder.LoadRoom (R);
				Grace = 0;
				Cursor.SetCursor (null, Vector2.zero, CursorMode.ForceSoftware);
			}
			if (StartDialogue) {
				Dia.StartDialogue ();
			}
		} else {
			switch (Silly) {
			case 1: // Blind Guard
				{
					if (Global.Dataholder.Silly_StunlockGuard || Global.Dataholder.GenericUnlocks [0] || Super.Dataholder.DEBUGGING) {
						Global.Dataholder.LoadRoom (R);
						Grace = 0;
						Cursor.SetCursor (null, Vector2.zero, CursorMode.ForceSoftware);
						Global.Dataholder.GenericUnlocks [0] = true;
					} else {
						Dia.StartDialogue ();
					}
				}
				break;
			case 2: // Barber's door.
				{
					if (Global.Dataholder.GenericUnlocks [1]  || Super.Dataholder.DEBUGGING) {
						Global.Dataholder.LoadRoom (R);
						Grace = 0;
						Cursor.SetCursor (null, Vector2.zero, CursorMode.ForceSoftware);
					} else {
						if (Dia.BeenHereBefore) {
							Alt_Dia1.StartDialogue ();
						} else {
							Dia.StartDialogue ();
						}
					}
				}
				break;
			
			case 4: // Boat Salesman.
				{
					if (Global.Dataholder.GenericUnlocks [3]  || Super.Dataholder.DEBUGGING) { // Crashed boat.
						Alt_Dia1.StartDialogue ();
					} else {
						Dia.StartDialogue ();
					}
				}
				break;
			case 5: // Boat.
				{
					if (Global.Dataholder.GenericUnlocks [3]  || Super.Dataholder.DEBUGGING) { // Crashed boat.
						Global.Dataholder.LoadRoom (R);
						Grace = 0;
						Cursor.SetCursor (null, Vector2.zero, CursorMode.ForceSoftware);
					} else {
						Dia.StartDialogue ();
					}
				}
				break;
			case 6: // TutorialDoor.
				{
					if (Global.Dataholder.GenericUnlocks [7]) {
						Global.Dataholder.LoadRoom (R);
						Grace = 0;
						Cursor.SetCursor (null, Vector2.zero, CursorMode.ForceSoftware);
					} else {
						Dia.StartDialogue ();
					}
				}
				break;

			case 7: // BossDoor.
				{
					if ((Global.Dataholder.GenericUnlocks [20] && Global.Dataholder.GenericUnlocks [21] && Global.Dataholder.GenericUnlocks [22]) || Super.Dataholder.DEBUGGING) {
						Global.Dataholder.LoadRoom (R);
						Grace = 0;
						Cursor.SetCursor (null, Vector2.zero, CursorMode.ForceSoftware);
						Alt_Dia1.StartDialogue ();
					} else {
						Dia.StartDialogue ();
					}
				}
				break;

			case 10: // Key1
				{
					if (Global.Dataholder.GenericUnlocks [10] || Super.Dataholder.DEBUGGING) { // Can Grab Key1.
						if (!Global.Dataholder.GenericUnlocks [20]) {
							Dia.StartDialogue ();
						}
						Global.Dataholder.GenericUnlocks [20] = true;
					} else {
						Alt_Dia1.StartDialogue ();

						Global.Dataholder.BonkKeyCutscene = true;
						Global.Dataholder.BonkKeyTimer = 0;
						Global.Dataholder.BonkKeyAnim.runtimeAnimatorController = Global.Dataholder.BonkKey_RAC;
						Global.Dataholder.CamPosBeforeTitleCard = Global.Dataholder.MainCam.transform.position;
						Global.Dataholder.MainCam.transform.position = new Vector3 (36, 80, -20);
					}
				}
				break;
			case 11: // Key2
				{
					if (Global.Dataholder.GenericUnlocks [11] || Super.Dataholder.DEBUGGING) { // Can Grab Key2.
						if (!Global.Dataholder.GenericUnlocks [21]) {
							Dia.StartDialogue ();
						}
						Global.Dataholder.GenericUnlocks [21] = true;
					} else {
						Alt_Dia1.StartDialogue ();

						Global.Dataholder.BonkKeyCutscene = true;
						Global.Dataholder.BonkKeyTimer = 0;
						Global.Dataholder.BonkKeyAnim.runtimeAnimatorController = Global.Dataholder.BonkKey_RAC;
						Global.Dataholder.CamPosBeforeTitleCard = Global.Dataholder.MainCam.transform.position;
						Global.Dataholder.MainCam.transform.position = new Vector3 (36, 80, -20);
					}
				}
				break;
			case 12: // Key3
				{
					if (Global.Dataholder.GenericUnlocks [12] || Super.Dataholder.DEBUGGING) { // Can Grab Key3.
						if (!Global.Dataholder.GenericUnlocks [22]) {
							Dia.StartDialogue ();
						}
						Global.Dataholder.GenericUnlocks [22] = true;
					} else {
						Alt_Dia1.StartDialogue ();

						Global.Dataholder.BonkKeyCutscene = true;
						Global.Dataholder.BonkKeyTimer = 0;
						Global.Dataholder.BonkKeyAnim.runtimeAnimatorController = Global.Dataholder.BonkKey_RAC;
						Global.Dataholder.CamPosBeforeTitleCard = Global.Dataholder.MainCam.transform.position;
						Global.Dataholder.MainCam.transform.position = new Vector3 (36, 80, -20);
					}
				}
				break;

			case 15: // Title_Captcha
				{
					Global.Dataholder.CamPosBeforeTitleCard = Global.Dataholder.MainCam.transform.position;
					Global.Dataholder.MainCam.transform.position = new Vector3 (0, 20, -10);
					Global.Dataholder.TitleCardTM.text = "You must solve the\ncaptcha before the\nkey can be grabbed.";
					Global.Dataholder.TitleCardID = 0;
					Global.Dataholder.TitleCardTimer = 0;
					Global.Dataholder.TitleCardVisible = true;
					Global.Dataholder.PlayTitleCardChime = true;
				}
				break;
			case 16: // Title_Acapella
				{
					Global.Dataholder.CamPosBeforeTitleCard = Global.Dataholder.MainCam.transform.position;
					Global.Dataholder.MainCam.transform.position = new Vector3 (0, 20, -10);
					Global.Dataholder.TitleCardTM.text = "You must dispel the\nacapella group before\nthe key can be grabbed.";
					Global.Dataholder.TitleCardID = 0;
					Global.Dataholder.TitleCardTimer = 0;
					Global.Dataholder.TitleCardVisible = true;
					Global.Dataholder.PlayTitleCardChime = true;

				}
				break;
			case 17: // Title_Truth
				{
					Global.Dataholder.CamPosBeforeTitleCard = Global.Dataholder.MainCam.transform.position;
					Global.Dataholder.MainCam.transform.position = new Vector3 (0, 20, -10);
					Global.Dataholder.TitleCardTM.text = "The guard who always\nspeaks the truth must\nbe told that they are\ntrustworthy before\nthe key can be grabbed.";
					Global.Dataholder.TitleCardID = 4;
					Global.Dataholder.TitleCardTimer = 0;
					Global.Dataholder.TitleCardVisible = true;
					Global.Dataholder.PlayTitleCardChime = true;

				}
				break;
			case 99:
				{
					if (!Global.Dataholder.StartedAcapella) {
						Global.Dataholder.StartedAcapella = true;
						Global.Dataholder.OneAnda2.StartDialogue ();
						Global.Dataholder.InsideAcapellaRoom = true;
					}
					Global.Dataholder.LoadRoom (R);

				}
				break;
			}
		}
	}



}
