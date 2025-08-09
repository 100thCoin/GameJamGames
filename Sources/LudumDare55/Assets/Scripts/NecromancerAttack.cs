using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerAttack : MonoBehaviour {

	public float speed;
	Vector3 Dir;
	// Use this for initialization
	void Start () {
		Dir = (transform.position - Global.Dataholder.HeroMov.transform.position).normalized;

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		transform.position -= Dir * speed;

	}
}
