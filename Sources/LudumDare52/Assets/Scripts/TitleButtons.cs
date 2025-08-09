using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButtons : MonoBehaviour {

	public bool Creds;
	public bool REturnCreds;
	public bool Quit;
	public bool Play;

	public bool MouseInside;

	public Camera Cam;

	void OnMouseOver () {

		MouseInside = true;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (MouseInside && Input.GetKeyDown(KeyCode.Mouse0)) {

			if (Creds) {

				Cam.transform.position = new Vector3 (0,40,-50);

			}

			if (REturnCreds) {

				Cam.transform.position = new Vector3 (0,0,-50);

			}

			if (Quit) {
				Application.Quit ();
			}

			if (Play) {
				SuperMain SM = GameObject.Find ("SuperMain").GetComponent<SuperMain> ();
				SM.LoadGameplay ();

			}


		}


		MouseInside = false;
	}
}
