using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckCard : MonoBehaviour {

	public int Grace;
	public int Layer;
	public Collider Col;
	public Transform Vis;
	public SpriteRenderer SR;
	public MeshRenderer TMesh;
	public TextMesh TM;

	public float HighlightTimer;

	public int Index;
	public int PhraseID;

	public bool MyPeopleNeedMe;
	public float MyPeopleNeedMeTimer;
	public SpriteRenderer FadeWhite;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 MousePos = Global.Dataholder.MainCam.ScreenToWorldPoint (Input.mousePosition);
		RaycastHit[] Hit = Physics.RaycastAll (MousePos, Vector3.forward,0x200);
		if (Hit != null && Global.Dataholder.CardsOpen) {
			if (Hit.Length > 0) {
				int ClosestLayer = 99;
				Collider ClosestCol = null;
				for (int i = 0; i < Hit.Length; i++) {
					DeckCard DC = Hit [i].collider.GetComponent<DeckCard> ();
					if (DC != null) {
						if (DC.Layer < ClosestLayer) {
							ClosestLayer = DC.Layer;
							ClosestCol = Hit [i].collider;
						}
					}
				}
				if (ClosestCol != null) {
					if (ClosestCol == Col) {
						Grace = 2;
						Global.Dataholder.HandOverCardGrace = 5;

						Cursor.SetCursor (Global.Dataholder.CustomCursors [4].Tex.texture, Global.Dataholder.CustomCursors [4].HotSpot, CursorMode.ForceSoftware);

						if (Layer != 0) {
							Global.Dataholder.DeckMain.ReOrderCards (Index);
						}
						if (Input.GetKeyDown (KeyCode.Mouse0) && Global.Dataholder.CardsOpen) {
							Interact ();
						}
					}
				}
			}
		}
		if (Grace <= 0 && Grace >= -5) {
			//Cursor.SetCursor (null, Vector2.zero, CursorMode.ForceSoftware);
		}
		Grace--;
		if (!MyPeopleNeedMe) {
			if (Grace > 0) {
				HighlightTimer += Time.deltaTime * 7;
			} else {
				HighlightTimer -= Time.deltaTime * 9;
			}
			HighlightTimer = Mathf.Clamp01 (HighlightTimer);
			Vis.transform.localPosition = new Vector3 (0, DataHolder.ParabolicLerp (0, 1, HighlightTimer, 1), 0);
			float dim = DataHolder.ParabolicLerp (0.8f, 1f, HighlightTimer, 1);
			SR.color = new Vector4 (dim, dim, dim, 1);
			TM.color = new Vector4 (dim, dim, dim, 1);
		} else {
			MyPeopleNeedMeTimer += Time.deltaTime;
			FadeWhite.color = new Vector4 (1, 1, 1, MyPeopleNeedMeTimer * 10);
			Vis.transform.localPosition = new Vector3 (0, DataHolder.SinLerp (1, 21, Mathf.Clamp01(MyPeopleNeedMeTimer*3), 1), 0);
		}


	}

	void Interact()
	{

		Global.Dataholder.CardsOpen = false;
		if (Global.Dataholder.HandInBattle) {
			if (Global.Dataholder.BossMan.CorrectCards [Global.Dataholder.BossMan.Round-1] == PhraseID) {
				Global.Dataholder.BossMan.HIT = true;
				Global.Dataholder.UnlockedCards [PhraseID] = false; // used it!
				Global.Dataholder.BossMan.PhaseTimer = -99;
			} else {
				Global.Dataholder.BossMan.MISS = true;
				Global.Dataholder.BossMan.PhaseTimer = -99;
			}
			MyPeopleNeedMe = true;

		} else {
			if (Global.Dataholder.HandTree != null && Global.Dataholder.HandTree.Length > PhraseID) {
				if (Global.Dataholder.HandTree [PhraseID] != null) {
					Global.Dataholder.HandTree [PhraseID].StartDialogue ();
				} else {
					Global.Dataholder.GenericHandTree [PhraseID].StartDialogue ();
				}
			} else {
				// Uh oh! We need to use the generic tree.
				Global.Dataholder.GenericHandTree [PhraseID].StartDialogue ();
			}
		}
	}
}
