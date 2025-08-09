using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RibbonCustomizerMenu : MonoBehaviour {

	public SpriteRenderer MainLayer;
	public SpriteRenderer MainLayer2;
	public SpriteRenderer TopLayer;
	public SpriteRenderer AloneStripe;

	public Sprite[] Ribbons;
	public float[] RibbonLengths;

	public bool StripeIsNotAlone;
	public bool Empty;

	public Camera Cam;

	public bool Dragging;
	public RibbonCustomizerStripeButton StripeButton;
	public RibbonCustomizerStripeButton ConfirmButton;

	public RibbonCustomizer_Spools CurrentSpool;

	public int BackupFirst;
	public int BackupSecond;

	public int FirstID;
	public int SecondID;
	public float NoSpaceTimer;
	public SpriteRenderer NOSpace;

	public SpriteRenderer SR_Menu;
	public Sprite Confirm;
	public Sprite NoConfirm;

	public bool CanCloseMenu;
	public float LongerErrorTimer;
	public SpriteRenderer LongerError;

	public SpriteRenderer ClickTheNewSpool;
	public SpriteRenderer ThisisTheRibbonYaDingus;
	public bool NoMoreDingus;

	public float ThoseSpoolsDontTimer;
	public SpriteRenderer ThoseSpoolsDont;

	// Use this for initialization
	void OnEnable () {

		BackupFirst = FirstID;
		BackupSecond = SecondID;

	}
	
	// Update is called once per frame
	void Update () {

		if (NoMoreDingus) {
			ThisisTheRibbonYaDingus.enabled = false;
		}

		if (SecondID != -1) {
			NoMoreDingus = true;
		}

		if (Input.GetKeyUp (KeyCode.Q)) {
			CanCloseMenu = true;
		}
		if (CanCloseMenu && (Input.GetKeyDown (KeyCode.Q) || Input.GetKeyDown (KeyCode.Escape))) {
			CanCloseMenu = false;

			FirstID = BackupFirst;
			SecondID = BackupSecond;
			gameObject.SetActive (false);

			if (Input.GetKeyDown (KeyCode.Q)) {
				Global.Dataholder.PlayerMov3D.CanOpenQMenu = false;
				Global.Dataholder.CanPause = true;

			}

			return;

		}

		NoSpaceTimer -= Time.deltaTime;

		NOSpace.enabled = NoSpaceTimer > 0;

		LongerErrorTimer -= Time.deltaTime;

		LongerError.enabled = LongerErrorTimer > 0;

		ThoseSpoolsDontTimer -= Time.deltaTime;

		ThoseSpoolsDont.enabled = ThoseSpoolsDontTimer > 0;

		if (CurrentSpool != null) {
			if (StripeButton.MouseOver) {
				if (Input.GetKeyDown (KeyCode.Mouse0)) {
					Dragging = true;
					SecondID = CurrentSpool.ID;
				}
			}
		}

		if (FirstID == -1) {
			Empty = true;
		} else {
			Empty = false;
			if (SecondID != -1) {
				StripeIsNotAlone = true;

				MainLayer.sprite = Ribbons[FirstID];
				MainLayer2.sprite = Ribbons[FirstID];
				TopLayer.sprite = Ribbons[SecondID];

				MainLayer.transform.localPosition = new Vector3 (-(12-RibbonLengths[FirstID])/2f,0,0);
				MainLayer2.transform.localPosition = new Vector3 ((12-RibbonLengths[FirstID])/2f,0,0);


			} else {
				StripeIsNotAlone = false;
				// lonely stripe
				AloneStripe.sprite = Ribbons[FirstID];
			}

		}



		if (Empty) {
			MainLayer.enabled = false;
			MainLayer2.enabled = false;
			TopLayer.enabled = false;
			AloneStripe.enabled = false;
		} else {
			if (StripeIsNotAlone) {
				MainLayer.enabled = true;
				MainLayer2.enabled = true;
				TopLayer.enabled = true;
				AloneStripe.enabled = false;

				// get mouse position
				if (Dragging) {
					
					Vector2 MousePos = Cam.ScreenToWorldPoint (Input.mousePosition);
					TopLayer.transform.position = new Vector3 (MousePos.x, TopLayer.transform.position.y, TopLayer.transform.position.z);
					if (TopLayer.transform.localPosition.x < -(6-RibbonLengths[SecondID]*0.5f)) {
						TopLayer.transform.localPosition = new Vector3 (-(6-RibbonLengths[SecondID]*0.5f), TopLayer.transform.localPosition.y, TopLayer.transform.localPosition.z);
					}
					if (TopLayer.transform.localPosition.x > (6-RibbonLengths[SecondID]*0.5f)) {
						TopLayer.transform.localPosition = new Vector3 ((6-RibbonLengths[SecondID]*0.5f), TopLayer.transform.localPosition.y, TopLayer.transform.localPosition.z);
					}

					if (TopLayer.transform.localPosition.x > 0) {
						MainLayer.sortingOrder = 2;
						MainLayer2.sortingOrder = 1;
					} else {
						MainLayer.sortingOrder = 1;
						MainLayer2.sortingOrder = 2;
					}

					if (Input.GetKeyUp (KeyCode.Mouse0)) {
						Dragging = false;
						CurrentSpool.OnStripe = true;
					}

				}

			} else {
				MainLayer.enabled = false;
				MainLayer2.enabled = false;
				TopLayer.enabled = false;
				AloneStripe.enabled = true;
			}
		}

		if (ConfirmButton.MouseOver) {

			SR_Menu.sprite = Confirm;

			if (Input.GetKeyDown (KeyCode.Mouse0)) {
				// okay, we need to actually update the real ribbon. what was I thinking when I said "oh yeah- sure, I can make this in 48 hours. easy."

				if (FirstID == -1) {

					LongerErrorTimer = 3;
					NoSpaceTimer = 0;
					return;

				}
				if ((FirstID == 1 && SecondID == -1) || (FirstID == 2 && SecondID == -1) || (FirstID == 1 && SecondID == 2)) {

					LongerErrorTimer = 3;
					NoSpaceTimer = 0;
					return;
				}



				Global.Dataholder.UpdateRibbonStructure ();
				// close menu

				CanCloseMenu = false;
				CanCloseMenu = false;

				BackupFirst = FirstID;
				BackupSecond = SecondID;
				gameObject.SetActive (false);
				Global.Dataholder.CanPause = true;

				return;
			}


		} else {
			SR_Menu.sprite = NoConfirm;

		}



	}
}
