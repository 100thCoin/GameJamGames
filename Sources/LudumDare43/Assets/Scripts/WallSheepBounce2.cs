using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSheepBounce2 : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{

		if(other.gameObject.name == "ThrowBox")
		{
			GameObject Sheep = other.gameObject.transform.parent.gameObject;
			Sheep.gameObject.GetComponent<AI_Sheep>().ThreeAxisVel = new Vector3(Sheep.gameObject.GetComponent<AI_Sheep>().ThreeAxisVel.x * -0.2f,Sheep.gameObject.GetComponent<AI_Sheep>().ThreeAxisVel.y * -0.2f,Sheep.gameObject.GetComponent<AI_Sheep>().ThreeAxisVel.z);
			other.gameObject.transform.parent.GetComponent<SphereCollider>().enabled = true;
			other.gameObject.SetActive(false);

		}

		if(other.gameObject.name.Contains("Item"))
		{
			//GameObject Sheep = other.gameObject.transform.parent.gameObject;
			other.gameObject.GetComponent<ItemPickup>().ThreeAxisVel = new Vector3(other.gameObject.GetComponent<ItemPickup>().ThreeAxisVel.x * -0.2f,other.gameObject.GetComponent<ItemPickup>().ThreeAxisVel.y * -0.2f,other.gameObject.GetComponent<ItemPickup>().ThreeAxisVel.z);

		}

	}
}

