using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmUI : MonoBehaviour {

	public GameObject Cam;

	void LateUpdate()
	{
		transform.position = new Vector3(Cam.transform.position.x,transform.position.y,transform.position.z);

	}
}
