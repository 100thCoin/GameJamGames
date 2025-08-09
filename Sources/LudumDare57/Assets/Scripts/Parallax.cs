using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {
	public float Mult;
	public Vector3 Offset;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		float t = transform.localPosition.z;
		transform.localPosition = (Global.Dataholder.MainCamera.transform.position - transform.parent.position) * Mult + Offset;
		transform.localPosition = new Vector3 (transform.localPosition.x, transform.localPosition.y, t);
	}
}
