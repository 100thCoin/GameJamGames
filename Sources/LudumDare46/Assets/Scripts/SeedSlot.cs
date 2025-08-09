using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedSlot : MonoBehaviour {

	public bool Filled;
	public bool FilledOnce;
	public Farm F;
	public GameObject SeedPlanter;

	public bool Mercy;

	public SpriteRenderer SR;
	// Use this for initialization
	void OnEnable () {

		F = transform.parent.parent.parent.GetComponent<Farm>();
		SeedPlanter = F.PSeedHolder;
		F.OpenSlots++;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseOver()
	{
		if(!Filled)
		{
			Mercy = true;
		}
	}


	void LateUpdate()
	{

		if(Mercy)
		{

			SeedPlanter.transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z+0.1f);

			if(Input.GetKeyDown(KeyCode.Mouse0))
			{
				if(F.SeedID != -1)
				{
					F.PlantSeed(F.SeedID,transform);

				}
			}




		}

		Mercy = false;
	}

	void OnTriggerStay(Collider other)
	{
		if(other.tag == "seed")
		{
			if(!FilledOnce)
			{
				FilledOnce = true;
				Filled = true;
				F.OpenSlots--;
				GetComponent<BoxCollider>().enabled = false;
				SR.enabled = false;
			}
		}
	}

}
