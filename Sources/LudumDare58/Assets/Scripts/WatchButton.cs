using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchButton : MonoBehaviour {

	public Sprite Idle;
	public Sprite Hover;
	public RuntimeAnimatorController Blink;

	public float MouseOverTimer;
	public int Grace;
	public Transform Vis;
	public SpriteRenderer SR;
	public Animator Anim;
	// Use this for initialization
	public float WiggleTimer;
	public float AnimTimer;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Grace <= 0) {
			if (Grace == 0) {
				Cursor.SetCursor (null, Vector2.zero, CursorMode.ForceSoftware);
				//print ("RemoveCursor");

			}
			WiggleTimer = 0;
			SR.sprite = Global.Dataholder.GameWatchView ? Hover : Idle;
		} else {
			SR.sprite = Global.Dataholder.GameWatchView ? Idle : Hover;

			WiggleTimer += Time.deltaTime * 60;
			if (WiggleTimer > 30) {
				Vis.transform.eulerAngles = Vector3.zero;
			} else {
				Vis.transform.eulerAngles = new Vector3 (0, 0, (300 * Mathf.Cos (WiggleTimer*0.5f)) / (WiggleTimer * WiggleTimer + 30));
			}

			if (Input.GetKeyDown (KeyCode.Mouse0)) {
				Global.Dataholder.GameWatchView = !Global.Dataholder.GameWatchView;
				if (Global.Dataholder.GameWatchView) {
					Anim.runtimeAnimatorController = Blink;
				} else {
					Global.Dataholder.ItemInHands = -1;
				}
			}
		}
		Grace--;

		if (Global.Dataholder.GameWatchView) {
			if (AnimTimer < 1) {
				AnimTimer += Time.deltaTime*3;
			}
			if (AnimTimer > 1) {
				AnimTimer = 1;
				Anim.runtimeAnimatorController = null;
			}

		} else {
			if (AnimTimer >0) {
				AnimTimer -= Time.deltaTime*3;
			}
			if (AnimTimer < 0) {
				AnimTimer = 0;
			}
		}
		transform.localPosition = new Vector3 (DataHolder.TwoCurveLerp (-3.25f, 3.25f, AnimTimer, 1), -3, 0);
	}

	void FixedUpdate()
	{
		if (Grace <= 0) {
			if (Vis.eulerAngles.z > 180) {
				Vis.eulerAngles -= new Vector3(0,0,360);
			}
			Vis.eulerAngles = new Vector3(Vis.eulerAngles.x,Vis.eulerAngles.y, (Vis.eulerAngles.z > 180 ? (Vis.eulerAngles.z-360) : Vis.eulerAngles.z)  * 0.8f);
		}
	}

	void OnMouseOver()
	{
		if (true) {
			Grace = 2;

			Cursor.SetCursor (Global.Dataholder.CustomCursors [7].Tex.texture, Global.Dataholder.CustomCursors [7].HotSpot, CursorMode.ForceSoftware);
		}
	}
}
