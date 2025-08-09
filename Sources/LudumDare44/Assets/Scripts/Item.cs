using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

	DataHolder Main;

	public ItemStat Itemstats;
	public GameObject ItemText;

	public bool DoOnce;
	public bool SuperSafetyDoOnce;

	// Use this for initialization
	void Start () {


		GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-3,3),Random.Range(2,4));
		Main = GameObject.Find("Main").GetComponent<DataHolder>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter(Collider other)
	{
		if(other.name == "Player" && !DoOnce)
		{
			DoOnce = true;
			if(!SuperSafetyDoOnce)
			{
				SuperSafetyDoOnce = true;
				Main.AddItem(Itemstats);
			}GameObject Desc = Instantiate(ItemText,new Vector3(gameObject.transform.position.x,gameObject.transform.position.y + 3,gameObject.transform.position.z),gameObject.transform.rotation,gameObject.transform.parent) as GameObject;
			Desc.GetComponent<ItemDesc>().Itemstat = Itemstats;
			Main.SpawnShop1Ladderite();
			if(Main.PostTUT)
			{
				Main.SpawnLantern();

			}

			Destroy(gameObject);

		}


	}


}
