using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButtons : MonoBehaviour {
	public bool Play;
	public bool Credits;
	public bool Quit;
	public bool Back;

	public int hoverGrace;
	public SpriteRenderer SR;
	public Sprite Hover;
	public Sprite NoHover;

	public GameObject Camera;
	public Vector3 CamMove;

	public TitleManager TMan;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Super.Dataholder.LockTitle) {
			return;
		}
		if (hoverGrace > 0) {
			SR.sprite = Hover;
			if (Input.GetKeyDown (KeyCode.Mouse0)) {

				if (Credits || Back) {
					Camera.transform.position = CamMove;
				}

				if (Play) {
					TMan.Playing = true;
					Super.Dataholder.LockTitle = true;
				}

				if (Quit) {
					Application.Quit ();
				}
			}

		} else {
			SR.sprite = NoHover;

		}

		hoverGrace--;
	}

	void OnMouseOver()
	{
		hoverGrace = 2;
	}

}
