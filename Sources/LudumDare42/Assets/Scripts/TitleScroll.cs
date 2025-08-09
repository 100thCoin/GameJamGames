using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScroll : MonoBehaviour {




	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
		gameObject.transform.position = new Vector3(gameObject.transform.position.x - Time.deltaTime * 15,gameObject.transform.position.y,gameObject.transform.position.z);

		if(gameObject.transform.position.x < -144)
		{
			gameObject.transform.position = new Vector3(gameObject.transform.position.x + 288,gameObject.transform.position.y,gameObject.transform.position.z);
		}

	}
}
