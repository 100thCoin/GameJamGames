using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour {

	public TitleButtons[] buttons;

	public bool Playing;
	public float PlayingTimer;

	public GameObject TitleMain;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		if (Playing) {
			PlayingTimer += Time.deltaTime;

			if (PlayingTimer > 1f) {

				Super.Dataholder.StartGame ();
				TitleMain.SetActive (false);
				PlayingTimer = 0;
				Playing = false;

				Super.Dataholder.Volume_Voice = Super.Dataholder.Volume_Music * 1.4f;
				if (Super.Dataholder.Volume_Voice > 1) {
					Super.Dataholder.Volume_Voice = 1;
				}
				if (Super.Dataholder.Volume_Music > 0.6f) {
					Super.Dataholder.Volume_Music = 0.6f;
				}

			}

		}


	}
}
