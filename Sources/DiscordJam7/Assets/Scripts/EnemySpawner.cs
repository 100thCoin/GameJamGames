using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject Type;
	public Transform[] SpawnPoints;
	public GameObject Puffs;
	public int Delay =60;
	public int ToSpawn;
	public bool Done;
	// Use this for initialization
	void Start () {

		ToSpawn = Main.DataHolder.ZeldaRoomRemainingenemies[Main.DataHolder.CurrentRoomID];

		int i = 0;

		Delay = 24;

		while(i < ToSpawn)
		{
			Instantiate(Puffs,SpawnPoints[i].transform.position,transform.rotation,transform);
			i++;
		}

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		Delay--;
		if(Delay <=0 && !Done)
		{
			Done = true;

			int i = 0;
			while(i < ToSpawn)
			{
				Instantiate(Type,SpawnPoints[i].transform.position,transform.rotation,transform);
				i++;
			}


		}



	}
}
