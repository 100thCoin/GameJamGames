using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	public int PlayerInFront;
	public MeshRenderer MR;
	public Material Door1;
	public Material Door2;
	public Material Door3;
	public bool Shop;
	public bool Cave;
	public int GemRequirement;
	public SpriteRenderer[] GemIcons;

	public Transform TelePlayerHere;

	public bool DEBUGGIN;

	public bool ScreenTransition;
	public bool ScreenTransitionDoOnce;
	public float TransitionTimer;

	public GameObject Voice1;
	public GameObject Voice2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (ScreenTransition) {
			TransitionTimer += Time.deltaTime;
			Global.Dataholder.RollingTransition.color = new Vector4 (0, 0, 0, TransitionTimer);
			if (TransitionTimer > 0.5f && !ScreenTransitionDoOnce) {
				ScreenTransitionDoOnce = true;
				Global.Dataholder.PMov.transform.position = TelePlayerHere.transform.position;
				Global.Dataholder.MainCamera.transform.position = new Vector3 (TelePlayerHere.transform.position.x, TelePlayerHere.transform.position.y, -100);
				if (GemRequirement == 4 || GemRequirement == 2) {
					Global.Dataholder.MainCamera.transform.position = new Vector3 (0, TelePlayerHere.transform.position.y, -100);
					Global.Dataholder.CamMov.BossCam = new Vector3 (0, TelePlayerHere.transform.position.y, -100);
					Global.Dataholder.GravTileA = null;
					Global.Dataholder.GravTileB = null;
					if (GemRequirement == 2 && !Global.Dataholder.NecromooncerDefeated) {
						Global.Dataholder.VoiceQueue.Add (Voice1);
						Global.Dataholder.VoiceQueue.Add (Voice2);
						Global.Dataholder.CamMov.Boss1Camera = true;
					} else if (GemRequirement == 4) {
						Global.Dataholder.VoiceQueue.Add (Voice1);
						Global.Dataholder.VoiceQueue.Add (Voice2);
						Global.Dataholder.PMov.PreventMovementForCutscenePurposes = true;;

					}
				}
				else 
				{
					Global.Dataholder.MainCamera.transform.position = new Vector3 (0, TelePlayerHere.transform.position.y, -100);
					Global.Dataholder.CamMov.BossCam = new Vector3 (0, TelePlayerHere.transform.position.y, -100);
					Global.Dataholder.CamMov.TargetPos =new Vector3 (0, TelePlayerHere.transform.position.y, -100);
					Global.Dataholder.GravTileA = null;
					Global.Dataholder.GravTileB = null;
				}
			}

			if (TransitionTimer > 1.1f) {
				ScreenTransition = false;

			}


			return;
		}


		if (PlayerInFront > 0) {

			bool unlocked = false;
			if (!Shop && !Cave) {
				unlocked = true;
			}
			if (Cave) {
				unlocked = Global.Dataholder.CountGems () >= GemRequirement;
			}
			if (Shop) {
				unlocked = Global.Dataholder.NecromooncerDefeated;
			}

			if (unlocked || DEBUGGIN) {
				MR.material = Door2;
				if (Input.GetKeyDown (KeyCode.W)) {
					// do door transition.
					if (Shop) {
						Global.Dataholder.WorkshopOpen = !Global.Dataholder.WorkshopOpen;
						Global.Dataholder.BlueWorkshop.gameObject.SetActive (Global.Dataholder.WorkshopOpen);
					} else if (Cave) {
						if (GemRequirement == 2) {
							// take you to boss fight of the game. Lock the player movement.
							ScreenTransition = true;
							if (Global.Dataholder.NecromooncerDefeated) {

							} else {
								Global.Dataholder.TheHudButton.Inactive = true;
								Global.Dataholder.GravTileA = null;
								Global.Dataholder.GravTileB = null;
							}


						} else {

							// take you to the final room of the game. Lock the player movement.
							ScreenTransition = true;
							Global.Dataholder.InFinaleCave = true;
							Global.Dataholder.TheHudButton.Inactive = true;
							Global.Dataholder.CamMov.enabled = false;
							Global.Dataholder.PMov.PreventMovementForCutscenePurposes = true;
							Global.Dataholder.PMov.PreventMovementForCutscenePurposes2 = true;

						}
					} else {
						ScreenTransition = true;
						Global.Dataholder.TheHudButton.Inactive = false;
						Global.Dataholder.TheHudButton.Blink = true;
					}
				}
			} else {
				MR.material = Door3;


			}
		} else {
			MR.material = Door1;
			if (Shop) {
				Global.Dataholder.WorkshopOpen = false;
				Global.Dataholder.BlueWorkshop.gameObject.SetActive (false);
			}
		}

		if (Cave) {
			for (int i = 0; i < GemIcons.Length; i++) {
				if (i < Global.Dataholder.CountGems ()) {
					GemIcons [i].enabled = true;
				}
			}
		}

	}

	void FixedUpdate()
	{
		if (Global.Dataholder.PMov.RB.velocity.magnitude > 0.1f) {
			PlayerInFront--;
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "Player") {
			PlayerInFront = 5;
		}
	}
}
