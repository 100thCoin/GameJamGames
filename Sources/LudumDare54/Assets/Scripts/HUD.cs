using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour {

	public float MoveOnScreenTimer;

	public Transform HudObject;

	public Sprite[] RibbonIcon;
	public SpriteRenderer RibbonHUDIcon;
	public TextMesh[] CountTMs;

	public GameObject NewSpool;


	public bool HaltTimerStayOnScreen;
	public bool NeverAgain;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		if (Global.Dataholder.TimeSinceLastCollectible < 2) {
			MoveOnScreenTimer += Time.deltaTime;

		}

		if (HaltTimerStayOnScreen && Input.GetKeyDown (KeyCode.Q)) {
			NeverAgain = true;
		}

		HaltTimerStayOnScreen = (Global.Dataholder.RibbonIDForIcon == 1 && Global.Dataholder.RibbonCollectibles[1] == Global.Dataholder.ImplementedRibbonsPerType[1]) && !NeverAgain;

		NewSpool.SetActive (HaltTimerStayOnScreen);

		if (Global.Dataholder.TimeSinceLastCollectible > 5 && !HaltTimerStayOnScreen) {
			
			MoveOnScreenTimer -= Time.deltaTime;
		}
		MoveOnScreenTimer = Mathf.Clamp01 (MoveOnScreenTimer);
		HudObject.transform.localPosition = new Vector3(0,DataHolder.SinLerp(0.2f,0,MoveOnScreenTimer,1),0);

		if (Global.Dataholder.TimeSinceLastCollectible < 1) {

			// update HUD text.
			RibbonHUDIcon.sprite = RibbonIcon [Global.Dataholder.RibbonIDForIcon];



			int i = 0;
			while (i < CountTMs.Length) {
				CountTMs [i].text = "" + Global.Dataholder.RibbonCollectibles [Global.Dataholder.RibbonIDForIcon] + " / " + Global.Dataholder.ImplementedRibbonsPerType [Global.Dataholder.RibbonIDForIcon];

				i++;
			}



		}



	}
}
