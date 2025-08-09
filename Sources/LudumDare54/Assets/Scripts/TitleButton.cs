using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButton : MonoBehaviour {

	public bool MouseOver;
	public int Grace;

	public float Timer;
	public SpriteRenderer SR;

	public bool Play;
	public bool Credits;
	public bool Back;

	public bool Quit;

	public Transform Cam;

	public bool LockedIn;

	public float PlayTimer;

	void OnMouseOver () {
		MouseOver = true;
		Grace = 3;
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		if (LockedIn) {
			PlayTimer += Time.deltaTime;
			Global.Dataholder.MusicMultiplier = 1 - PlayTimer * 0.75f;

			if (PlayTimer > 1.5f) {


				Global.Dataholder.InGame = true;
				Global.Dataholder.SpeedrunTime = 0;
				Global.Dataholder.DoSCreenIn = false;
				Global.Dataholder.DoScreenOut = true;
				Global.Dataholder.MusicMultiplier = 1;
				Global.Dataholder._InGame.SetActive (true);
				Global.Dataholder._Title.SetActive (false);
				return;
			}
			return;
		}


		if (Global.Dataholder.NoMoreTitleButtons) {
			return;
		}

		if (MouseOver) {
			Timer += Time.deltaTime*10;
			Timer = Mathf.Clamp01 (Timer);

			SR.transform.localPosition = new Vector3 (0, DataHolder.SinLerp (0, 2,Timer,1), 0);

			if (Input.GetKeyDown (KeyCode.Mouse0)) {

				if (Play) {
					LockedIn = true;
					Global.Dataholder.NoMoreTitleButtons = true;
					Global.Dataholder.DoSCreenIn = true;


				} else if (Credits) {

					Cam.transform.position = new Vector3 (Cam.transform.position.x, -20, Cam.transform.position.z);

				}
				else if (Back) {

					Cam.transform.position = new Vector3 (Cam.transform.position.x, 0, Cam.transform.position.z);

				}
				else if (Quit) {

					Application.Quit ();

				}

			}



		} else {
			Timer -= Time.deltaTime*10;
			Timer = Mathf.Clamp01 (Timer);
			SR.transform.localPosition = new Vector3 (0, DataHolder.SinLerp (2, 0,1-Timer,1), 0);

		}


		if(Grace< 0)
		{
			MouseOver = false;
		}
		Grace--;
	}
}
