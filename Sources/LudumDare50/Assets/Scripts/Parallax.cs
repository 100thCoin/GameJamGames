using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

	public Transform Cam;

	public float Distance;

	public Vector3 Spos;

	// Update is called once per frame
	void LateUpdate () {

		transform.localPosition = Spos + Cam.position / Distance;


	}
}
