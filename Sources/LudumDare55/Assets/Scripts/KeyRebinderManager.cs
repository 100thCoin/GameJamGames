using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class KeyRebinderManager : MonoBehaviour {

	public KeyRebinder CurrentlyRebindingThisGuy;
	public int Delay;

	public bool Appear;
	public float SchmovementTimer;
	public bool Locked;
	public GameObject MainMenu;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Locked) {
			Appear = false;
			if (SchmovementTimer <= 0) {

				Super.Dataholder.StartGame ();
				Appear = true;
				MainMenu.SetActive (false);
				return;
			}
		}

		if (Appear) {
			SchmovementTimer += Time.deltaTime;
		} else {
			SchmovementTimer -= Time.deltaTime;
		}
		SchmovementTimer = Mathf.Clamp01 (SchmovementTimer);
		Super.Dataholder.MusicMultiplier = SchmovementTimer;

		transform.position = new Vector3 (0, DataHolder.ParabolicLerp(-34,0,SchmovementTimer,1), 0);


		Delay--;

		if (CurrentlyRebindingThisGuy != null) {

			if(Input.anyKeyDown && Delay < 0) {
				// what button was it?
				foreach (KeyCode KC in Enum.GetValues(typeof(KeyCode))) {
					if (Input.GetKeyDown (KC)) {
						KeyCode[] ControlRebinds = {
							KeyCode.D,
							KeyCode.A,
							KeyCode.S,
							KeyCode.W,
							KeyCode.Space,
							KeyCode.Mouse0,
							KeyCode.Mouse1,
							KeyCode.R,
							KeyCode.Escape
						};
						Super.Dataholder.ReboundInputs [CurrentlyRebindingThisGuy.ID] = KC;
						CurrentlyRebindingThisGuy.TM.text = RebindNamesInEnglish (KC.ToString ());
						CurrentlyRebindingThisGuy.TM.color = new Vector4 (1, 1, 1, 1);
						if (CurrentlyRebindingThisGuy.ShrinkText) {
							CurrentlyRebindingThisGuy.TM.transform.localScale = (Vector3.one / (CurrentlyRebindingThisGuy.TM.text.Length * 0.5f + 0.5f)) * 0.0625f;
						}
						CurrentlyRebindingThisGuy = null;

						break;
					}
				}

			}


		}

	}





	public String RebindNamesInEnglish(String input)
	{
		switch (input) {
		case "Alpha1": return "1";
		case "Alpha2": return "2";
		case "Alpha3": return "3";
		case "Alpha4": return "4";
		case "Alpha5": return "5";
		case "Alpha6": return "6";
		case "Alpha7": return "7";
		case "Alpha8": return "8";
		case "Alpha9": return "9";
		case "Alpha0": return "0";
		case "Space": return "Spacebar";
		case "Mouse0": return "Left Mouse";
		case "Mouse1": return "Right Mouse";
		case "Mouse2": return "Middle Mouse";
		case "*+DPadX":	return "DPad Right";
		case "*-DPadX":	return "DPad Left";
		case "*+DPadY":	return "DPad Up";
		case "*-DPadY":	return "DPad Down";
		default: break;
		}

		return input;

	}



}
