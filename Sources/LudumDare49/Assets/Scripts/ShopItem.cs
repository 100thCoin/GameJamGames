using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour {

	public bool AtItem;
	public GameObject ShopUI;

	void FixedUpdate () {

		ShopUI.SetActive(AtItem);


		AtItem = false;
	}

	void OnTriggerStay(Collider other)
	{
		if(other.tag =="Player")
		{
			AtItem = true;
		}

	}
}
