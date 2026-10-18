using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnUnlock : MonoBehaviour {

	public int UnlockRequired;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Global.Dataholder.GenericUnlocks [UnlockRequired]) {
			Cursor.SetCursor (null, Vector2.zero, CursorMode.ForceSoftware); // sloppy bug fix

			gameObject.SetActive (false);
		}

	}
}
