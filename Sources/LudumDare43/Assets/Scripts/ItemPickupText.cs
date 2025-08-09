using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupText : MonoBehaviour {

	public float timer;

	
	// Update is called once per frame
	void FixedUpdate () {




		gameObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y + 0.02f,-9);
		timer += Time.fixedDeltaTime;

		if(timer > 3.5f)
		{
			Destroy(gameObject);

		}

	}




}
