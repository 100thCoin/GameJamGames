using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

	public bool Hidden = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Hidden = !Hidden;

		}

		transform.localPosition = Hidden ? new Vector3(0,0,-500) : new Vector3(0,0,3.58f);

	}
}
