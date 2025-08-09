using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StronkyKong : MonoBehaviour {

	public GameObject Barrel;
	public GameObject BlueBarrel;

	public float ThrowTimer;

	public DataHolder Main;

	public Vector3 BarrelSpawn;

	public bool FirstBarrel;

	public SpriteRenderer SR;
	public Sprite Idle;
	public Sprite Reach;
	public Sprite Hold;
	public Sprite Toss;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Main.FreezeTimer <= 0)
		{
			ThrowTimer -= Time.deltaTime;
	
			if(ThrowTimer < 0)
			{
				ThrowTimer += 3;
				bool summonBlue = false;
				if((Main.FireballsPresent < 3 && Main.AllowThreeFires) || Main.FireballsPresent < 2)
				{
					summonBlue = Random.Range(0,10) == 1;
				}
				if(!Main.PlayerHasPlacedSomething)
				{
					summonBlue = false;
				}

				if(FirstBarrel)
				{
					if(Main.PlayerHasPlacedSomething)
					{
						FirstBarrel = false;
						summonBlue = true;

					}
					else
					{
						summonBlue = false;
					}

				}

				if(summonBlue)
				{
					Instantiate(BlueBarrel,transform.position + BarrelSpawn,transform.rotation,Main.HazardMap);
					Main.FireballsPresent++;
				}
				else
				{
					Instantiate(Barrel,transform.position + BarrelSpawn,transform.rotation,Main.HazardMap);
				}

			}
		
		
		}

	}
}
