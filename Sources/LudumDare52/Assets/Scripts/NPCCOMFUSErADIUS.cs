using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCOMFUSErADIUS : MonoBehaviour {

	//nice caps lock

	// Use this for initialization
	PlayerMovement P;
	public NPC guy;
	public bool PromptKillTodd;
	public GameObject VoiceLine;
	public bool DoVoiceOnce;
	public bool ChickenCheck;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (ChickenCheck) {
			if (Main.Data.KilledChicken) {
				guy.SR.flipX = Main.Data.Playerr.transform.position.x > guy.transform.position.x;
				if (!DoVoiceOnce) {
					DoVoiceOnce = true;
					if (Main.Data.CurrentVoiceLine == null) {
						Main.Data.CurrentVoiceLine = Instantiate (VoiceLine);
					} else {
						Main.Data.QueuedVoiceLine = VoiceLine;
					}
				}

				guy.Alerted = true;
				guy.AlertRenderer.enabled = true;
				if (PromptKillTodd) {
					Main.Data.PromptKillTodd = true;
				} else {
					Main.Data.PromptKillNeighbor = true;

				}
			}

		}

	}


	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			if (P == null) {
				P = other.GetComponent<PlayerMovement> ();
			}

			if (!DoVoiceOnce) {
				DoVoiceOnce = true;
				if (Main.Data.CurrentVoiceLine == null) {
					Main.Data.CurrentVoiceLine = Instantiate (VoiceLine);
				} else {
					Main.Data.QueuedVoiceLine = VoiceLine;
				}
			}

			guy.Alerted = true;
			guy.AlertRenderer.enabled = true;
			if (PromptKillTodd) {
				Main.Data.PromptKillTodd = true;
			} else {
				Main.Data.PromptKillNeighbor = true;

			}
		}


	}


}
