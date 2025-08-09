using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

	public float Distace;

	public GameObject Cam;

	public Vector2 StartPos;

	// Use this for initialization
	void Start () {
		StartPos = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		gameObject.transform.position = new Vector3(StartPos.x + Cam.transform.position.x * Distace - 2,StartPos.y + Cam.transform.position.y * Distace,gameObject.transform.position.z);

	}
}
