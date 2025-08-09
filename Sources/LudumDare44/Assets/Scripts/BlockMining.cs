using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMining : MonoBehaviour {


	public int Hp;
	public float AutoMineTimer;

	public bool RedicleOver;

	public Sprite[] DMG;

	public SpriteRenderer Damage;

	public GameObject ItemToDrop;
	public GameObject ItemToDrop2;

	public bool AwaitDeath;

	public bool Indestructible;

	public int Ladders;

	public bool LadderMode;
	public GameObject LadderBase;

	public bool NOLADDER;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame

	void Update () {



		if(Input.GetKeyDown(KeyCode.Mouse0) && RedicleOver && !Indestructible)
		{
			AutoMineTimer = 0.125f;
			if(!LadderMode)
			{
				Hp--;
			}

		}

		if(Hp < 3)
		{

			if(Hp <= -1 && !AwaitDeath)
			{
				AwaitDeath = true;
				Instantiate(ItemToDrop,gameObject.transform.position,gameObject.transform.rotation,gameObject.transform.parent);
				if(ItemToDrop2 != null)
				{
					Instantiate(ItemToDrop2,gameObject.transform.position,gameObject.transform.rotation,gameObject.transform.parent);
				}
				gameObject.transform.position = new Vector3(0,0,-500);
			}
			else if(!AwaitDeath)
			{
				Damage.sprite = DMG[Hp];
			}
		}


	}

	void FixedUpdate () {

		if(Input.GetKey(KeyCode.Mouse0) && RedicleOver && AutoMineTimer < 0 && !Indestructible)
		{
			AutoMineTimer += 0.125f;
			if(!LadderMode)
			{
				Hp--;
			}

		}
		else if (Input.GetKey(KeyCode.Mouse0) && RedicleOver)
		{
			AutoMineTimer -= Time.fixedDeltaTime;

		}

		if(AwaitDeath)
		{
			gameObject.transform.position = new Vector3(0,0,-500);

			Destroy(gameObject);
		}

	}
}
