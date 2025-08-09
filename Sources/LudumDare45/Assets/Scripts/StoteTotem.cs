using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoteTotem : MonoBehaviour {

	public GameObject StoneTotemGore;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "EnemyDeath")
		{

			Instantiate(StoneTotemGore,other.transform.position,other.transform.rotation,transform.parent.parent);

			transform.position = new Vector3(0,0,-500);

		}
	}

}
