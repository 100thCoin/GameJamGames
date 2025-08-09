using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour {

	public GameObject Dust;
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Ground")
		{
			Instantiate(Dust,transform.position,Dust.transform.rotation,transform.parent);
			Destroy(gameObject);
		}
	}
}
