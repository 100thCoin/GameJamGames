using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallCutscene : MonoBehaviour {

	public GameObject PlayerFalling;
	public float Timer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Cursor.SetCursor (null, Vector2.zero, CursorMode.ForceSoftware);

		Timer += Time.deltaTime;
		PlayerFalling.transform.localScale = new Vector3(2-Timer,2-Timer,1);
		if(Timer > 2)
		{
			PlayerFalling.transform.localScale = Vector3.zero;
		}
		if (Timer > 3) {
			Global.Dataholder.VictoryScreen.SetActive (true);
			Global.Dataholder.LoseGameCutscene.SetActive (false);
		}
	}
}
