using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepStore : MonoBehaviour {

	public int[] pickone;
	public GameObject[] spawnpoints;

	public GameObject PropSheep;

	public GameObject Maggy;


	public GameObject[] Prop;
	// Use this for initialization
	void Start () {

		pickone[0] = 0;
		pickone[1] = 1;


		pickone[2] = Random.Range(2,8);

		if(pickone[2] >= 3)
		{
			pickone[2]++;
		}

		while(pickone[3] == 0 || pickone[3] == pickone[2])
		{	
		pickone[3] = Random.Range(2,8);

		if(pickone[3] >= 3)
		{
			pickone[3]++;
		}
		}

		while(pickone[4] == 0 || pickone[4] == pickone[2] || pickone[4] == pickone[3])
		{	
			pickone[4] = Random.Range(2,8);

			if(pickone[4] >= 3)
			{
				pickone[4]++;
			}
		}


		Prop[0] = Instantiate(PropSheep,spawnpoints[0].transform.position,gameObject.transform.rotation,gameObject.transform);
		Prop[0].transform.Find("PurchaseWindow").GetComponent<PurchaseLamb>().pickone = pickone[0];

		Prop[1] = Instantiate(PropSheep,spawnpoints[1].transform.position,gameObject.transform.rotation,gameObject.transform);
		Prop[1].transform.Find("PurchaseWindow").GetComponent<PurchaseLamb>().pickone = pickone[1];
	
		Prop[2] = Instantiate(PropSheep,spawnpoints[2].transform.position,gameObject.transform.rotation,gameObject.transform);
		Prop[2].transform.Find("PurchaseWindow").GetComponent<PurchaseLamb>().pickone = pickone[2];

		Prop[3] = Instantiate(PropSheep,spawnpoints[3].transform.position,gameObject.transform.rotation,gameObject.transform);
		Prop[3].transform.Find("PurchaseWindow").GetComponent<PurchaseLamb>().pickone = pickone[3];

		Prop[4] = Instantiate(PropSheep,spawnpoints[4].transform.position,gameObject.transform.rotation,gameObject.transform);
		Prop[4].transform.Find("PurchaseWindow").GetComponent<PurchaseLamb>().pickone = pickone[4];



	}
	
	// Update is called once per frame
	void Update () {

		if(Maggy == null)
		{

			int i = 0;
			while ( i < 5)
			{
				Prop[i].transform.Find("PurchaseWindow").GetComponent<PurchaseLamb>().ClerkDead = true;


				i++;
			}

			Destroy(this);


		}



	}
}
