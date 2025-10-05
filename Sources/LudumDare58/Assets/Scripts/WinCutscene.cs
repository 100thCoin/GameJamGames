using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCutscene : MonoBehaviour {

	public GameObject Scene1;
	public GameObject Scene2;

	public float Timer;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		Cursor.SetCursor (null, Vector2.zero, CursorMode.ForceSoftware);

		Timer += Time.deltaTime;
		if(Timer > 2.5)
		{
			Scene1.SetActive (false);
		}

		if (Timer > 18) {
			Scene2.SetActive (false);

		}

		if (Timer > 26) {
			Global.Dataholder.VictoryScreen.SetActive (true);
			Global.Dataholder.LoseGameCutscene.SetActive (false);
		}
	}
}
