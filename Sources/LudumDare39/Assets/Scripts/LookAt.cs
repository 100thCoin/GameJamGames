using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour {

	public Vector2 MousePos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		MousePos = GameObject.Find("MoveCamera").gameObject.GetComponent<Camera>().ScreenToWorldPoint (Input.mousePosition);

		gameObject.transform.eulerAngles = new Vector3 (0, 0, Mathf.Atan2 (gameObject.transform.position.y - MousePos.y, gameObject.transform.position.x - MousePos.x) * Mathf.Rad2Deg + 90);

	}
}
