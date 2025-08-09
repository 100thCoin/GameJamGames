using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarderCols : MonoBehaviour {

	public bool Y;
	public bool XOnPlayerRight;
	public bool XOnPlayerLeft;

	void OnTriggerStay(Collider other)
	{

		if(other.tag == "Player")
		{
			if(Y)
			{
				if(!Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
				other.transform.position = new Vector3(other.transform.position.x,transform.position.y+1.75f,other.transform.position.z);
			}

			if(XOnPlayerLeft)
			{
				if(!Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
					other.transform.position = new Vector3(transform.position.x+1.25f,other.transform.position.y,other.transform.position.z);
			}

			if(XOnPlayerRight)
			{
				if(!Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
					other.transform.position = new Vector3(transform.position.x-1.25f,other.transform.position.y,other.transform.position.z);
			}
		}



	}

}
