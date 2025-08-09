using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCamPos : MonoBehaviour {

	public GameObject NewWall;

	public GameObject Target;

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			GameObject.Find("Main Camera").transform.parent.GetComponent<CameraControl>().Player = Target;
			GameObject.Find("Main Camera").transform.parent.GetComponent<CameraControl>().Target = Target;

			NewWall.SetActive(true);
			Destroy(gameObject);

		}

	}


}
