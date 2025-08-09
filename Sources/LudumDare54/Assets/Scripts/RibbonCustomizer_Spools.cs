using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RibbonCustomizer_Spools : MonoBehaviour {

	public TextMesh TM;
	public int ID;
	public RibbonCustomizerMenu RCustMan;
	public bool Selected;
	public bool OnStripe;

	public SpriteRenderer Glow;
	public SpriteRenderer Tooltip;

	public SpriteRenderer IconSR;
	public Sprite UnlockedSprite;

	public bool unlocked;

	// Use this for initialization
	void Start () {
		
	}

	void Update()
	{
		Glow.enabled = Selected;
		Tooltip.enabled = (Selected && !OnStripe) && !RCustMan.Dragging;
		if (!RCustMan.NoMoreDingus && Selected) {
			RCustMan.ThisisTheRibbonYaDingus.enabled = (Selected && !OnStripe) && !RCustMan.Dragging;
		}

	}

	// Update is called once per frame
	void OnEnable () {

		if (ID == -1) {
			TM.text = "";

		} else {
			if (Global.Dataholder.RibbonCollectibles [ID] >= Global.Dataholder.ImplementedRibbonsPerType [ID]) {
				TM.text = "";
				IconSR.sprite = UnlockedSprite;
				unlocked = true;
			} else if (Global.Dataholder.RibbonCollectibles [ID] == 0) {
				TM.text = "?";
			}
			else
			{
				TM.text = "" + Global.Dataholder.RibbonCollectibles [ID] + "/" + Global.Dataholder.ImplementedRibbonsPerType [ID];
				IconSR.sprite = UnlockedSprite;
			}
		}
	}

	void OnMouseOver () {

		if (!unlocked && !Global.Dataholder.DEBUGMODE) {
			return;
		}

		if (Input.GetKeyDown (KeyCode.Mouse0)) {

			if (RCustMan.ClickTheNewSpool.enabled) {
				if (ID == 0) {
					return;
				} else if (ID == 1) {

					RCustMan.ClickTheNewSpool.enabled = false;
				}

			}


			if (!OnStripe) {
				
				if (Selected) {
					Selected = false;
					RCustMan.CurrentSpool = null;
				} else {
					if (RCustMan.FirstID == -1) {
						Selected = true;
						RCustMan.FirstID = ID;
						OnStripe = true;
						RCustMan.NoSpaceTimer = 0;
						RCustMan.LongerErrorTimer = 0;
						RCustMan.ThoseSpoolsDontTimer = 0;

					}
					else if (RCustMan.SecondID == -1) {
						if (RCustMan.RibbonLengths [RCustMan.FirstID] + RCustMan.RibbonLengths [ID] > 13) {
							RCustMan.NoSpaceTimer = 3;
							RCustMan.LongerErrorTimer = 0;
							RCustMan.ThoseSpoolsDontTimer = 0;

						} else {

							if ((RCustMan.FirstID == 1 && ID == 2) || (RCustMan.FirstID == 2 && ID == 1)) {
								RCustMan.NoSpaceTimer = 0;
								RCustMan.LongerErrorTimer = 0;
								RCustMan.ThoseSpoolsDontTimer = 3;
							} else {

								Selected = true;
								RCustMan.CurrentSpool = this;
								RCustMan.NoSpaceTimer = 0;
								RCustMan.LongerErrorTimer = 0;
								RCustMan.ThoseSpoolsDontTimer = 0;

							}
						}

					} else {
						// NOT ENOUGH SPACE
						RCustMan.NoSpaceTimer = 3;
						RCustMan.LongerErrorTimer = 0;

					}
				}


			} else {
				OnStripe = false;

				if (RCustMan.CurrentSpool != null) {
					if (RCustMan.CurrentSpool.Selected && !RCustMan.CurrentSpool.OnStripe) {
						RCustMan.CurrentSpool.Selected = false;
					}
				}
				Selected = false;

				RCustMan.CurrentSpool = null;

				if (RCustMan.FirstID == ID) {
					//remove the base layer, change the other one to the base.
					RCustMan.FirstID = RCustMan.SecondID;
					RCustMan.SecondID = -1;

				} else {
					// remove top layer
					RCustMan.SecondID = -1;


				}

			}
		}

	}

}
