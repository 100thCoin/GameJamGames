using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissonancerBoss : MonoBehaviour {

	public bool FallingToDeath;
	public float FallingDeathTimer;

	public SpriteRenderer SR;

	public GameObject BarrelPrefab;
	public float BarrelTimer;
	public Transform Barrelspawner;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (FallingToDeath) {

			FallingDeathTimer += Time.deltaTime;
			SR.flipY = true;
			float fallpos = Mathf.Lerp (9.75f, 4.75f, FallingDeathTimer);
			transform.position = new Vector3 (transform.position.x, fallpos, transform.position.z);
			if (FallingDeathTimer > 1) {
				Global.Dataholder.WinGame ();
			}
		} else {

			BarrelTimer += Time.deltaTime;
			if (BarrelTimer > 8f) {
				BarrelTimer -= 8f;
				Instantiate (BarrelPrefab, Barrelspawner.transform.position, transform.rotation, Barrelspawner.transform);
			}
		}

	}

	void OnTriggerEnter(Collider other)
	{

		if (other.CompareTag ("Void") || other.CompareTag ("Barrel")) {
			{
				FallingToDeath = true;

			}
		}
	}
}
