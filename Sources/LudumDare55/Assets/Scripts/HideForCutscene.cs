using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideForCutscene : MonoBehaviour {

	public GameObject Target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Global.Dataholder.FailedALevel) {

			Target.SetActive (true);
			Global.Dataholder.FailedALevel = false;
			Destroy (this);

		}

	}

	public void ForceUnhide()
	{
		Target.SetActive (true);
		Destroy (this);
	}

}
