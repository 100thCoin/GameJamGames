using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventSoftlockForSure : MonoBehaviour {

	public GameObject Door_EnterslashLocked;
	public GameObject Door_UnlockWithKey;
	public GameObject Door_Uncanny;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Global.Dataholder.GameWatchView) {
			Door_EnterslashLocked.SetActive (false);
			Door_Uncanny.SetActive (false);
			if (Door_UnlockWithKey != null) {
				Door_UnlockWithKey.SetActive (true);
			}
		} else if (Global.Dataholder.ArbitraryChecks [10]) {
			Door_EnterslashLocked.SetActive (false);
			Door_Uncanny.SetActive (true);
			if (Door_UnlockWithKey != null) {
				
				Door_UnlockWithKey.SetActive (false);
			}
		} else {
			Door_EnterslashLocked.SetActive (true);
			Door_Uncanny.SetActive (false);
			if (Door_UnlockWithKey != null) {
				
				Door_UnlockWithKey.SetActive (false);
			}
		}


	}
}
