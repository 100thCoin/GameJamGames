using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCoin : MonoBehaviour {

	public float Speed;
	public GameObject Points;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		transform.position += new Vector3(0,(float)Speed/128f,0);
		Speed--;
		Speed--;
		if(Speed <=0)
		{
			Instantiate(Points,transform.position,transform.rotation);
			Destroy(gameObject);
		}

	}
}
