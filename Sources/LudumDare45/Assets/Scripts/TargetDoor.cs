using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDoor : MonoBehaviour {

	public bool Active;


	public GameObject Door;

	void FixedUpdate()
	{


		if(Active)
		{

			if(Door.transform.position.y < 9)
			{
				Door.transform.position += new Vector3(0,0.8f,0);

			}


		}
		else
		{
			if(Door.transform.position.y > 2)
			{
				Door.transform.position += new Vector3(0,-0.8f,0);

			}

		}






		Active = false;


	}

	void OnTriggerStay(Collider other)
	{
		if(other.tag == "EnemyDeath" || other.tag == "Player")
		{

			Active = true;

		}


	}
}
