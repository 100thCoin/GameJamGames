using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject Player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
		Player = GameObject.Find ("Player");

		if (Player.gameObject.transform.position.x > gameObject.transform.position.x + 0) {

			gameObject.transform.position = new Vector3 (Player.gameObject.transform.position.x , gameObject.transform.position.y, -100);

		}

		if (Player.gameObject.transform.position.x < gameObject.transform.position.x - 4) {

			gameObject.transform.position = new Vector3 (Player.gameObject.transform.position.x + 4, gameObject.transform.position.y, -100);

		}


	}
}
