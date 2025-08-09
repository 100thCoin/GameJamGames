using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBounceSheep : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{

		if(other.gameObject.name == "ThrowBox")
		{
			GameObject Sheep = other.gameObject.transform.parent.gameObject;
			Sheep.gameObject.GetComponent<AI_Sheep>().ThreeAxisVel = new Vector3(Sheep.gameObject.GetComponent<AI_Sheep>().ThreeAxisVel.x * 0.7f,Sheep.gameObject.GetComponent<AI_Sheep>().ThreeAxisVel.y * -0.2f,Sheep.gameObject.GetComponent<AI_Sheep>().ThreeAxisVel.z);
			other.gameObject.transform.parent.GetComponent<SphereCollider>().enabled = true;
			other.gameObject.SetActive(false);

		}

		if(other.gameObject.name == "Skeleton")
		{
			//GameObject Sheep = other.gameObject.transform.parent.gameObject;
			other.gameObject.GetComponent<AI_Skeleton>().ThreeAxisVel = new Vector3(other.gameObject.GetComponent<AI_Skeleton>().ThreeAxisVel.x * 0.7f,other.gameObject.GetComponent<AI_Skeleton>().ThreeAxisVel.y * -0.2f,other.gameObject.GetComponent<AI_Skeleton>().ThreeAxisVel.z);


		}

		if(other.gameObject.name == "Skelepile")
		{
			//GameObject Sheep = other.gameObject.transform.parent.gameObject;
			other.gameObject.GetComponent<AI_Skelepile>().ThreeAxisVel = new Vector3(other.gameObject.GetComponent<AI_Skelepile>().ThreeAxisVel.x * 0.7f,other.gameObject.GetComponent<AI_Skelepile>().ThreeAxisVel.y * -0.2f,other.gameObject.GetComponent<AI_Skelepile>().ThreeAxisVel.z);


		}

		if(other.gameObject.name == "Wolf")
		{
			//GameObject Sheep = other.gameObject.transform.parent.gameObject;
			other.gameObject.GetComponent<AI_Wolf>().ThreeAxisVel = new Vector3(other.gameObject.GetComponent<AI_Wolf>().ThreeAxisVel.x * 0.7f,other.gameObject.GetComponent<AI_Wolf>().ThreeAxisVel.y * -0.2f,other.gameObject.GetComponent<AI_Wolf>().ThreeAxisVel.z);

		}

		if(other.gameObject.name.Contains("Item"))
		{
			//GameObject Sheep = other.gameObject.transform.parent.gameObject;
			other.gameObject.GetComponent<ItemPickup>().ThreeAxisVel = new Vector3(other.gameObject.GetComponent<ItemPickup>().ThreeAxisVel.x * 0.7f,other.gameObject.GetComponent<ItemPickup>().ThreeAxisVel.y * -0.2f,other.gameObject.GetComponent<ItemPickup>().ThreeAxisVel.z);

		}
	}
}
