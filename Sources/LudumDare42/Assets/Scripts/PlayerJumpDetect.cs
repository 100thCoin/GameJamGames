using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpDetect : MonoBehaviour {
	public PlayerMovement P;

	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag == "Floor")
		{
			P.OnGround = true;
			if(other.gameObject.transform.position.y > gameObject.transform.position.y)
			{
				P.UpsideDown = true;
			}
			else
			{
				P.UpsideDown = false;

			}
		}
		else if(other.gameObject.tag == "BottomFloor")
		{
			P.OnGround = true;
			P.UpsideDown = false;

		}
		else if(other.gameObject.tag == "TopFloor")
		{
			P.OnGround = true;
			P.UpsideDown = true;

		}



	}

	void OnTriggerEnter(Collider other)
	{

		if(other.gameObject.tag == "PlayerObjective")
		{

			P.Win = true;
			P.AlreadyWon = true;
			P.objective = other.gameObject;
			other.gameObject.GetComponent<ObjectivePlayer>().P = P;
		}

	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Floor")
		{
			P.OnGround = false;
		}
	}
}
