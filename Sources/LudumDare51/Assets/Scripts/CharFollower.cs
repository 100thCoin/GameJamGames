using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharFollower : MonoBehaviour {


	void Update () {
		transform.position = G.Main.CharMov.transform.position + new Vector3(0,3,0);
	}

	void FixedUpdate () {
		transform.position = G.Main.CharMov.transform.position + new Vector3(0,3,0);
	}
}
