using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitToTitleButton : MonoBehaviour {


	public int hoverGrace;
	public SpriteRenderer SR;
	public Sprite Hover;
	public Sprite NoHover;

	public GameObject Camera;
	public Vector3 CamMove;

	public TitleMan TMan;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		if (hoverGrace > 0) {
			SR.sprite = Hover;
			if (Input.GetKeyDown (KeyCode.Mouse0)) {

				Super.Dataholder.ReturnToTitle ();
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
