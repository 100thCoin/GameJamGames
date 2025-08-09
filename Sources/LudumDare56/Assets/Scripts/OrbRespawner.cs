using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbRespawner : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Global.Dataholder.OrbRes = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public GameObject OrbPrefab;

	public void RespawnOrb()
	{
		Instantiate (OrbPrefab, transform.position, transform.rotation,transform);
		Instantiate (Global.Dataholder.CircleSmall, transform.position, transform.rotation,transform);
	}
}
