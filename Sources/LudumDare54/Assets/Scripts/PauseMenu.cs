using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

	public RibbonCustomizerStripeButton ResumeButton;
	public RibbonCustomizerStripeButton QuitButton;

	public SpriteRenderer SR;
	public Sprite MenuNoButtons;
	public Sprite Menu_Resume;
	public Sprite Menu_Quit;
	public bool CanCloseMenu;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyUp (KeyCode.Escape)) {
			CanCloseMenu = true;
		}
		if (CanCloseMenu &&  Input.GetKeyDown (KeyCode.Escape)) {
			CanCloseMenu = false;

			gameObject.SetActive (false);

			if (Input.GetKeyDown (KeyCode.Escape)) {
				Global.Dataholder.CanPause = false;
			}

			return;

		}


		if (ResumeButton.MouseOver) {
			SR.sprite = Menu_Resume;

			if (Input.GetKeyDown (KeyCode.Mouse0)) {
				CanCloseMenu = false;

				gameObject.SetActive (false);

				Global.Dataholder.CanPause = true;

				return;
			}

		} else if (QuitButton.MouseOver) {
			SR.sprite = Menu_Quit;
			if (Input.GetKeyDown (KeyCode.Mouse0)) {
				Global.Dataholder.GameLoad.REGENERATE ();
				return;
			}


		} else {
			SR.sprite = MenuNoButtons;
		}






	}
}
