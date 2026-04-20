using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour {

	public TitleButtons[] buttons;

	public bool Playing;
	public float PlayingTimer;

	public SpriteRenderer RollingTransition;
	public GameObject TitleMain;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		if (Playing) {
			PlayingTimer += Time.deltaTime;
			RollingTransition.color = new Vector4(0,0,0,DataHolder.SinLerp(0f,0.5f,PlayingTimer,1));

			if (PlayingTimer > 1f) {

				Super.Dataholder.StartGame ();
				TitleMain.SetActive (false);
				PlayingTimer = 0;
				Playing = false;
				RollingTransition.color = new Vector4 (0, 0, 0, 0);
			}

		}


	}
}
