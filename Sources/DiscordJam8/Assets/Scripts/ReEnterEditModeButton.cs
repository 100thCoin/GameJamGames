using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReEnterEditModeButton : MonoBehaviour {

	public SpriteRenderer SR;
	public Sprite On;
	public Sprite Off;

	public bool MouseOver;
	public int Grace;

	public float AppearTimer;

	void OnMouseOver () {
		if (!Global.DataHolder.InLevelEditor && !Global.DataHolder.GameIsPaused && !Global.DataHolder.PlayerTouchedAxe) {
			MouseOver = true;
			Grace = 3;
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Global.DataHolder.GameIsPaused) {
			return;
		}

		if (Global.DataHolder.InLevelEditor || Global.DataHolder.WatchingCinematicCutscene || Global.DataHolder.PreLevel1 || Global.DataHolder.PlayerTouchedAxe) {
			Grace = 0;
			MouseOver = false;
			AppearTimer -= Time.deltaTime*5;
			SR.enabled = false;

			if (AppearTimer <= -1) {
				AppearTimer = -1;
			}
		} else {
			AppearTimer += Time.deltaTime*1;
			if (AppearTimer > 0) {
				SR.enabled = true;
				if (AppearTimer >= 1) {
					AppearTimer = 1;
				}
				transform.localPosition = new Vector3(DataHolder.ParabolicLerp(-28,-22,AppearTimer,1),DataHolder.ParabolicLerp(18,14,AppearTimer,1),10);
			}
		}



		if (MouseOver) {
			SR.sprite = On;
			if(Input.GetKeyDown(KeyCode.Mouse0))
			{
				Global.DataHolder.EnterLevelEditor ();
				MouseOver = false;
				Grace = 0;
			}

		} else {
			SR.sprite = Off;

		}

		Grace--;
		if (Grace < 0) {
			MouseOver = false;
		}
	}
}
