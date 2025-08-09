using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonkyKongis : MonoBehaviour {

	public float Timer;
	public GameObject Barrel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Timer += Time.deltaTime;

		if(Timer >= 8)
		{
			Instantiate(Barrel,new Vector3(gameObject.transform.position.x + 2,gameObject.transform.position.y,gameObject.transform.position.z),gameObject.transform.rotation,gameObject.transform);
			Timer -= 8;
		}


	}
}
