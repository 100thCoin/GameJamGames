using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipCutsceneButton : MonoBehaviour {

	public SpriteRenderer SR;
	public Sprite On;
	public Sprite Off;

	public bool MouseOver;
	public int Grace;

	void OnMouseOver () {
		if (!Global.DataHolder.InLevelEditor) {
			MouseOver = true;
			Grace = 3;
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Global.DataHolder.CutsceneOver) {
			Destroy (this);
		}

		if (MouseOver) {
			SR.sprite = On;
			if(Input.GetKeyDown(KeyCode.Mouse0))
			{
				Global.DataHolder.CutsceneOver = true;
				Destroy (this);
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
