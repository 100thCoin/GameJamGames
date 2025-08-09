using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformingTNT : MonoBehaviour {

	public GameObject Vis;
	public float RespawnTimer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (RespawnTimer > 0) {
			RespawnTimer -= Time.deltaTime;

		}

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("Player") && Global.Dataholder.Pmov.transform.position.y > transform.position.y-0.5f && Global.Dataholder.Pmov.RB.velocity.y < 0.5f) {
			RespawnTimer = 2;
			Global.Dataholder.Pmov.RB.velocity = new Vector3 (0,12,0);
			Instantiate (Global.Dataholder.ExplosionPrefab, transform.position, transform.rotation, transform);
			Instantiate (Global.Dataholder.SFX_Bomb, transform.position, transform.rotation, transform);

		}





	}






}
