using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrownFollow : MonoBehaviour {

	public Transform CrownPos;

	public Vector3 Followpos;
	public float floattimer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		int big = 4;

		Followpos = (Followpos * big + CrownPos.transform.position) / (big+1);

		floattimer += Time.fixedDeltaTime * 6;

		transform.position = Followpos + new Vector3 (0,Mathf.Sin (floattimer+4)*0.1f, 0);

	}
}
