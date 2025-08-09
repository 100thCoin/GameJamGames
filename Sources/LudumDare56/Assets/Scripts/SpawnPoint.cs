using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Global.Dataholder.PMov.transform.position = transform.position;
		Global.Dataholder.MostRecentPlatformPosition = transform.position;
			
	}
	

}
