using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovment : MonoBehaviour {

	public Vector2 MousePos;

	public Vector3 CameraSlerp;
	public Vector3 CameraNewPos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		MousePos = GameObject.Find("MoveCamera").gameObject.GetComponent<Camera>().ScreenToWorldPoint (Input.mousePosition);

		if (MousePos.x < GameObject.Find ("Main Camera").gameObject.transform.position.x - 16) {
			MousePos.x = GameObject.Find ("Main Camera").gameObject.transform.position.x - 16;
		}
		if (MousePos.x > GameObject.Find ("Main Camera").gameObject.transform.position.x + 16) {
			MousePos.x = GameObject.Find ("Main Camera").gameObject.transform.position.x + 16;
		}

		if (MousePos.y < GameObject.Find ("Main Camera").gameObject.transform.position.y - 14) {
			MousePos.y = GameObject.Find ("Main Camera").gameObject.transform.position.y - 14;
		}
		if (MousePos.y > GameObject.Find ("Main Camera").gameObject.transform.position.y + 14) {
			MousePos.y = GameObject.Find ("Main Camera").gameObject.transform.position.y + 14;
		}
		CameraNewPos = new Vector3 ((MousePos.x + gameObject.transform.position.x) / 2, (MousePos.y + gameObject.transform.position.y) / 2, -10);

		CameraNewPos = new Vector3 ((CameraNewPos.x + gameObject.transform.position.x) / 2, (CameraNewPos.y + gameObject.transform.position.y) / 2, -10);
		CameraNewPos = new Vector3 ((CameraNewPos.x + gameObject.transform.position.x) / 2, (CameraNewPos.y + gameObject.transform.position.y) / 2, -10);


	}

	void FixedUpdate()
	{

		CameraSlerp = new Vector3 ((CameraNewPos.x + GameObject.Find ("Main Camera").gameObject.transform.position.x) / 2, (CameraNewPos.y + GameObject.Find ("Main Camera").gameObject.transform.position.y) / 2, -10);
		GameObject.Find ("Main Camera").gameObject.transform.position = CameraSlerp;
	}

}
