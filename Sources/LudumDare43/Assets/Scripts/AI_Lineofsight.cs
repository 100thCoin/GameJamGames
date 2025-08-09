using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Lineofsight : MonoBehaviour {

	public bool Skeleton;
	public bool Skelepile;
	public bool Cart;
	public bool Wolf;
	public bool Mushroom;
	public bool Hunter;
	public bool RockThrower;


	void OnTriggerEnter(Collider other)
	{

		if(other.gameObject.name == "Mary" || other.gameObject.name.Contains("SheepType"))
		{

			if(Skeleton)
			{
				gameObject.transform.parent.Find("Skeleton").GetComponent<AI_Skeleton>().DetectPlayer = true;
				gameObject.transform.parent.Find("Skeleton").GetComponent<AI_Skeleton>().Mary = GameObject.Find("Mary");
			}
			if(Skelepile)
			{
				gameObject.transform.parent.Find("Skelepile").GetComponent<AI_Skelepile>().DetectPlayer = true;
				gameObject.transform.parent.Find("Skelepile").GetComponent<AI_Skelepile>().Mary = GameObject.Find("Mary");
			}
			if(Cart)
			{
				gameObject.transform.parent.Find("BombCart").GetComponent<BombCart>().DetectPlayer = true;
				gameObject.transform.parent.Find("BombCart").GetComponent<BombCart>().Mary = GameObject.Find("Mary");
			}
			if(Wolf)
			{
				gameObject.transform.parent.Find("Wolf").GetComponent<AI_Wolf>().DetectPlayer = true;
				gameObject.transform.parent.Find("Wolf").GetComponent<AI_Wolf>().Mary = GameObject.Find("Mary");
			}
			if(Mushroom)
			{
				gameObject.transform.parent.Find("Mushroom").GetComponent<AI_Mushroom>().DetectPlayer = true;
				gameObject.transform.parent.Find("Mushroom").GetComponent<AI_Mushroom>().Mary = GameObject.Find("Mary");
			}
			if(Hunter)
			{
				gameObject.transform.parent.Find("Hunter").GetComponent<AI_Hunter>().DetectPlayer = true;
				gameObject.transform.parent.Find("Hunter").GetComponent<AI_Hunter>().Mary = GameObject.Find("Mary");
			}
			if(RockThrower)
			{
				gameObject.transform.parent.Find("RockThrower").GetComponent<AI_RockThrower>().DetectPlayer = true;
				gameObject.transform.parent.Find("RockThrower").GetComponent<AI_RockThrower>().Mary = GameObject.Find("Mary");
			}



			Destroy(gameObject);

		}

	}

}
