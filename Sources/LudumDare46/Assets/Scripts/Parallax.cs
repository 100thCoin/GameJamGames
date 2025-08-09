using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {
	public float Dist;
	public GameObject Cam;
	void Start()
	{
		Cam = GameObject.Find("Main Camera");
	}

	// Update is called once per frame
	void LateUpdate () {

		transform.position = new Vector3(Cam.transform.position.x * (1/(Dist+1)),transform.position.y,-Dist+30);

	}
}
