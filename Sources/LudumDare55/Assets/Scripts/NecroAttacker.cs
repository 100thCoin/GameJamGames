using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecroAttacker : MonoBehaviour {

	public float FireRate;
	public float Timer;
	public GameObject Projectile;
	public Transform SpawnPoint;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (!Global.Dataholder.Pmov.InCutscene) {
			Timer -= Time.deltaTime;
			if (Timer < 0) {
				Timer += FireRate;
				Instantiate (Projectile, SpawnPoint.transform.position, SpawnPoint.transform.rotation, SpawnPoint);

			}
		
		
		
		}

	}
}
