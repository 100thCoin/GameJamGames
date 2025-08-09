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

	public bool UNpuase;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		if (hoverGrace > 0) {
			SR.sprite = Hover;
			if (Input.GetKeyDown (KeyCode.Mouse0)) {
				if (UNpuase) {
					Global.Dataholder.Paused = false;
					Global.Dataholder.PauseMenu.SetActive (false);
				} else {
					Super.Dataholder.ReturnToTitle ();
				}
			}

		} else {
			if (hoverGrace > -5) {
				SR.sprite = NoHover;
			}
		}

		hoverGrace--;
	}

	void OnMouseOver()
	{
		hoverGrace = 2;
	}

}
