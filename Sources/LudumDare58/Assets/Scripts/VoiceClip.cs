using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceClip : MonoBehaviour {

	public AudioSource AS;
	public float Timer;

	public bool UnlockArbitraryGoal;
	public int GoalID;

	public bool WaitCursor;
	public bool Skippable;

	public bool DontDestroySelf;

	public bool dontCareGiveItem;
	public int ITemID;

	public bool FrontburgerMan;

	public bool forceCloseGW;

	// Use this for initialization
	void Start () {
		Global.Dataholder.CurrentVoiceClip = this;
		if (UnlockArbitraryGoal) {
			Global.Dataholder.ArbitraryChecks [GoalID] = true;
		}

		if (dontCareGiveItem) {
			Global.Dataholder.Inventory [ITemID] = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (forceCloseGW) {
			Global.Dataholder.GameWatchView = false;
		}
		Timer += Time.deltaTime;
		if (WaitCursor) {
			Cursor.SetCursor (Global.Dataholder.CustomCursors [8].Tex.texture, Global.Dataholder.CustomCursors [8].HotSpot, CursorMode.ForceSoftware);
		}
		if (Skippable) {
			if (Input.GetKeyDown (KeyCode.Mouse0)) {
				Timer = 99999;
			}
		}
		if (Timer > AS.clip.length) {
			Global.Dataholder.CurrentVoiceClip = null;
			if (WaitCursor) {
				Cursor.SetCursor (null, Vector2.zero, CursorMode.ForceSoftware);
				//print ("RemoveCursor");

			}
			if (FrontburgerMan) {
				if (Timer > 35f) {
					Global.Dataholder.Frontburgerman.SetActive (false);
				}
			}

			if (!DontDestroySelf) {
				
				Destroy (gameObject);
			}
		}
	}
}
