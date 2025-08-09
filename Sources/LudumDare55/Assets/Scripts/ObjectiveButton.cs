using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveButton : MonoBehaviour {

	public ObjectivesMenu OMenu;
	public SpriteRenderer SR;
	public Sprite On;
	public Sprite Off;
	int Grace;

	// Use this for initialization
	void Start () {
		
	}



	void Update () {
		Grace--;
		if (Grace < 0) {
			SR.sprite = Off;
		}

	}

	void OnMouseOver()
	{
		if (OMenu.OnScreen) {
			SR.sprite = On;
			Grace = 2;
			if (Input.GetKeyDown (KeyCode.Mouse0)) {

				OMenu.OnScreen = false;
				Global.Dataholder.Pmov.InCutscene = false;
				Global.Dataholder.CamMov.enabled = true;
				Global.Dataholder.PostBoomDialogue = false;
				Cursor.lockState = CursorLockMode.Confined;
				Cursor.visible = false;
				Global.Dataholder.TimerActive = true;
				if (Global.Dataholder.Level == 4) {
					Global.Dataholder.CutMan.SubCutscenes [7].SetActive (false);
					Global.Dataholder.LoadedLevel.GetComponent<HideForCutscene> ().ForceUnhide ();

				}

			}

		}
	}

}
