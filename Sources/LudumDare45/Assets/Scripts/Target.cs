using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {


	public GameObject Pillar;
	public GameObject Gibs;


	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "EnemyDeath")
		{
			Instantiate(Gibs,transform.position,transform.rotation,transform.parent.parent);


			gameObject.transform.position = new Vector3(0,0,-500);
			Pillar.transform.position = new Vector3(0,0,-500);

		}


	}

}
