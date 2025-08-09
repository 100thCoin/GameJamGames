using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProps : MonoBehaviour {


	public GameObject[] Props;
	public float[] Ranges;

	public GameObject[] Enemies;

	// Use this for initialization
	void Start () {


		int r = Random.Range(0,Props.Length);

		float SpawnHeight = Random.Range(Ranges[r],-Ranges[r]);

		Instantiate(Props[r],new Vector3(gameObject.transform.position.x,SpawnHeight,0),gameObject.transform.rotation,gameObject.transform.parent);

		int r2 = Random.Range(0,2);

		int r3 = Random.Range(0,Enemies.Length);


		if(SpawnHeight < -5 || SpawnHeight > 5)
		{

			Instantiate(Enemies[r3],new Vector3(gameObject.transform.position.x,-SpawnHeight,0),gameObject.transform.rotation,gameObject.transform.parent);

		}
		else
		{

			if(SpawnHeight >=0)
			{
				Instantiate(Enemies[r3],new Vector3(gameObject.transform.position.x,SpawnHeight-Ranges[r],0),gameObject.transform.rotation,gameObject.transform.parent);
			}
			else
			{

				Instantiate(Enemies[r3],new Vector3(gameObject.transform.position.x,SpawnHeight+Ranges[r],0),gameObject.transform.rotation,gameObject.transform.parent);

			}



		}

		

	}

}
