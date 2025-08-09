using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour {

	public GameObject Item1;

	public bool rock;
	public bool tree;
	public bool Bush;
	public bool Onion;
	public bool Pumpkin;

	public bool GraveStone;
	public bool Grave;
	public int state;

	public Sprite[] NewSprite;

	void OnTriggerEnter(Collider other)
	{

		if(other.gameObject.tag == "Shovel" && Grave)
		{

			GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ExpCur = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ExpCur +10;


			int i = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().level * 2 + 1;
			i = i + GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ShovelTier * 20;
			while( i >= 0)
			{
				GameObject Item = Instantiate(Item1,new Vector3(gameObject.transform.position.x,gameObject.transform.position.y+5,0),gameObject.transform.rotation,gameObject.transform.parent.transform) as GameObject;

				int j = Random.Range(0,30);

				if(j == 0){Item.gameObject.GetComponent<Items>().Gold = true;}
				if(j == 1){Item.gameObject.GetComponent<Items>().Gold = true;}
				if(j == 2){Item.gameObject.GetComponent<Items>().Gold = true;}
				if(j == 3){Item.gameObject.GetComponent<Items>().Gold = true;}
				if(j == 4){Item.gameObject.GetComponent<Items>().Gold = true;}
				if(j == 5){Item.gameObject.GetComponent<Items>().Bones = true;}
				if(j == 6){Item.gameObject.GetComponent<Items>().Bones = true;}
				if(j == 7){Item.gameObject.GetComponent<Items>().Bones = true;}
				if(j == 8){Item.gameObject.GetComponent<Items>().Bones = true;}
				if(j == 9){Item.gameObject.GetComponent<Items>().Bones = true;}
				if(j == 10){Item.gameObject.GetComponent<Items>().Bones = true;}
				if(j == 11){Item.gameObject.GetComponent<Items>().Acorns = true;}
				if(j == 12){Item.gameObject.GetComponent<Items>().Acorns = true;}
				if(j == 13){Item.gameObject.GetComponent<Items>().Acorns = true;}
				if(j == 14){Item.gameObject.GetComponent<Items>().Acorns = true;}
				if(j == 15){Item.gameObject.GetComponent<Items>().Rocks = true;}
				if(j == 16){Item.gameObject.GetComponent<Items>().Rocks = true;}
				if(j == 17){Item.gameObject.GetComponent<Items>().Rocks = true;}
				if(j == 18){Item.gameObject.GetComponent<Items>().Rocks = true;}
				if(j == 19){Item.gameObject.GetComponent<Items>().Pelt = true;}
				if(j == 20){Item.gameObject.GetComponent<Items>().Pelt = true;}
				if(j == 21){Item.gameObject.GetComponent<Items>().Pelt = true;}
				if(j == 22){Item.gameObject.GetComponent<Items>().Pelt = true;}
				if(j == 23){Item.gameObject.GetComponent<Items>().Sticks = true;}
				if(j == 24){Item.gameObject.GetComponent<Items>().Sticks = true;}
				if(j == 25){Item.gameObject.GetComponent<Items>().Iron = true;}
				if(j == 26){Item.gameObject.GetComponent<Items>().Iron = true;}
				if(j == 27){Item.gameObject.GetComponent<Items>().Bones = true;}
				if(j == 28){Item.gameObject.GetComponent<Items>().Bones = true;}
				if(j == 29){Item.gameObject.GetComponent<Items>().Gems = true;}








				Item.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-5f,5f),Random.Range(6f,14f),0);

				i = i-1;
			}
			GameObject.Find("Main").gameObject.GetComponent<DataHolder>().RobGrave += 1;

			Destroy(gameObject);

		}


		if(other.gameObject.tag == "Shovel" && Bush)
		{
			GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ExpCur = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ExpCur +3;

			int i = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().level * 2 + 1;
			i = i + GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ShovelTier * 2;
			while( i >= 0)
			{
			GameObject Item = Instantiate(Item1,gameObject.transform.position,gameObject.transform.rotation,gameObject.transform.parent.transform) as GameObject;
			Item.gameObject.GetComponent<Items>().Berries = true;
			Item.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-2f,2f),Random.Range(2f,4f));

			i = i-1;
			}
			Destroy(gameObject);

		}

		if(other.gameObject.tag == "Shovel" && Onion)
		{
			GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ExpCur = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ExpCur +6;

			int i = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().level;
			i = i + GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ShovelTier;

			while( i >= 0)
			{
				GameObject Item = Instantiate(Item1,gameObject.transform.position,gameObject.transform.rotation,gameObject.transform.parent.transform) as GameObject;
				Item.gameObject.GetComponent<Items>().Onions = true;
				Item.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-2f,2f),Random.Range(2f,4f));

				i = i-1;
			}
			Destroy(gameObject);

		}

		if(other.gameObject.tag == "Shovel" && Pumpkin)
		{
			GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ExpCur = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ExpCur + 15;

			int i = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().level + -1;
			i = i + GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ShovelTier;
			while( i >= 0)
			{
				GameObject Item = Instantiate(Item1,gameObject.transform.position,gameObject.transform.rotation,gameObject.transform.parent.transform) as GameObject;
				Item.gameObject.GetComponent<Items>().Pumpkins = true;
				Item.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-2f,2f),Random.Range(2f,4f));

				i = i-1;
			}

			Destroy(gameObject);

		}

		if(other.gameObject.tag == "Pick" && rock)
		{
			GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ExpCur = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ExpCur +3;

			if(other.gameObject.transform.parent.parent.gameObject.GetComponent<ItemUse>().Swing)
			{
				state = state+1;
				if(state == 1){
					GameObject Item = Instantiate(Item1,gameObject.transform.position,gameObject.transform.rotation,gameObject.transform.parent.transform) as GameObject;
					Item.gameObject.GetComponent<Items>().Rocks = true;
					Item.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-2f,2f),Random.Range(2f,4f));
				}
				if(state == 2){
					GameObject Item = Instantiate(Item1,gameObject.transform.position,gameObject.transform.rotation,gameObject.transform.parent.transform) as GameObject;
					Item.gameObject.GetComponent<Items>().Rocks = true;
						Item.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-2f,2f),Random.Range(2f,4f));
								
					int i = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().level;
					i = i + GameObject.Find("Main").gameObject.GetComponent<DataHolder>().PickaxeTier;
					while(i > 0)
					{
						Item = Instantiate(Item1,gameObject.transform.position,gameObject.transform.rotation,gameObject.transform.parent.transform) as GameObject;

						if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().level >= 4 || GameObject.Find("Main").gameObject.GetComponent<DataHolder>().PickaxeTier >=1)
						{
							if(Random.Range(0,3) == 2)
							{
								Item.gameObject.GetComponent<Items>().Iron = true;
								
							}
							else
							{
								Item.gameObject.GetComponent<Items>().Rocks = true;

							}
						}
						else							
						{
						Item.gameObject.GetComponent<Items>().Rocks = true;
						}
						Item.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-2f,2f),Random.Range(2f,4f));
						i = i-1;			
					}
				}
				if(state == 3){
					GameObject Item = Instantiate(Item1,gameObject.transform.position,gameObject.transform.rotation,gameObject.transform.parent.transform) as GameObject;
					Item.gameObject.GetComponent<Items>().Rocks = true;
					int i = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().level;
					i = i + GameObject.Find("Main").gameObject.GetComponent<DataHolder>().PickaxeTier;
					while(i > 0)
					{
						Item = Instantiate(Item1,gameObject.transform.position,gameObject.transform.rotation,gameObject.transform.parent.transform) as GameObject;

						if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().level >= 2 || GameObject.Find("Main").gameObject.GetComponent<DataHolder>().PickaxeTier >=1)
						{
							if(Random.Range(0,2) == 1)
							{

								if(Random.Range(0,2) == 1)
								{
									Item.gameObject.GetComponent<Items>().Gems = true;
								}
								else
								{
								Item.gameObject.GetComponent<Items>().Iron = true;
								}
							}
							else
							{
								Item.gameObject.GetComponent<Items>().Rocks = true;

							}
						}
						else							
						{
							Item.gameObject.GetComponent<Items>().Rocks = true;
						}
						i = i-1;
					}
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().QuestRock += 1;
					Destroy(gameObject);

				}
				if(state >= 4)
				{
				}
				else
				{
				gameObject.GetComponent<SpriteRenderer>().sprite = NewSprite[state-1];
				}}
		}

		if(other.gameObject.tag == "Axe" && tree)
		{
			GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ExpCur = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ExpCur +2.5f;

			if(other.gameObject.transform.parent.parent.gameObject.GetComponent<ItemUse>().Swing)
			{
				state = state+1;
				if(state == 1){
					GameObject Item = Instantiate(Item1,gameObject.transform.position,gameObject.transform.rotation,gameObject.transform.parent.transform) as GameObject;
					Item.gameObject.GetComponent<Items>().Acorns = true;
					Item.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-2f,2f),Random.Range(2f,4f));
				}
				if(state == 2){
					GameObject Item = Instantiate(Item1,gameObject.transform.position,gameObject.transform.rotation,gameObject.transform.parent.transform) as GameObject;
					Item.gameObject.GetComponent<Items>().Acorns = true;
					Item.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-2f,2f),Random.Range(2f,4f));

					int i = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().level;
					i = i + GameObject.Find("Main").gameObject.GetComponent<DataHolder>().AxeTier;
					while(i > 0)
					{
						Item = Instantiate(Item1,gameObject.transform.position,gameObject.transform.rotation,gameObject.transform.parent.transform) as GameObject;

						if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().level >= 4 || GameObject.Find("Main").gameObject.GetComponent<DataHolder>().PickaxeTier >=1)
						{
							if(Random.Range(0,3) == 2)
							{
								Item.gameObject.GetComponent<Items>().Sticks = true;

							}
							else
							{
								Item.gameObject.GetComponent<Items>().Acorns = true;

							}
						}
						else							
						{
							Item.gameObject.GetComponent<Items>().Acorns = true;
						}
						Item.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-2f,2f),Random.Range(2f,4f));
						i = i-1;
					}
				}
				if(state == 3){
					GameObject Item = Instantiate(Item1,gameObject.transform.position,gameObject.transform.rotation,gameObject.transform.parent.transform) as GameObject;
					Item.gameObject.GetComponent<Items>().Acorns = true;
					int i = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().level;
					i = i + GameObject.Find("Main").gameObject.GetComponent<DataHolder>().PickaxeTier;
					while(i > 0)
					{
						Item = Instantiate(Item1,gameObject.transform.position,gameObject.transform.rotation,gameObject.transform.parent.transform) as GameObject;

						if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().level >= 2 || GameObject.Find("Main").gameObject.GetComponent<DataHolder>().PickaxeTier >=1)
						{
							if(Random.Range(0,2) == 1)
							{

								if(Random.Range(0,2) == 1)
								{
									if(Random.Range(0,3) == 1)
									{
										GameObject Item2 = Instantiate(Item1,gameObject.transform.position,gameObject.transform.rotation,gameObject.transform.parent.transform) as GameObject;
										Item2.gameObject.GetComponent<Items>().Other = true;
										Item2.gameObject.GetComponent<Items>().OtherID = 15;


									}


									Item.gameObject.GetComponent<Items>().Logs = true;
								}
								else
								{
									Item.gameObject.GetComponent<Items>().Sticks = true;
								}
							}
							else
							{
								Item.gameObject.GetComponent<Items>().Acorns = true;

							}
						}
						else							
						{
							Item.gameObject.GetComponent<Items>().Acorns = true;
						}
						i = i-1;
					}
				}
				if(state == 4){
					GameObject Item = Instantiate(Item1,gameObject.transform.position,gameObject.transform.rotation,gameObject.transform.parent.transform) as GameObject;
					Item.gameObject.GetComponent<Items>().Acorns = true;
					int i = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().level;
					i = i + GameObject.Find("Main").gameObject.GetComponent<DataHolder>().PickaxeTier;
					while(i > 0)
					{
						Item = Instantiate(Item1,gameObject.transform.position,gameObject.transform.rotation,gameObject.transform.parent.transform) as GameObject;

						if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().level >= 2 || GameObject.Find("Main").gameObject.GetComponent<DataHolder>().PickaxeTier >=1)
						{
							if(Random.Range(0,2) == 1)
							{

								if(Random.Range(0,2) == 1)
								{
									Item.gameObject.GetComponent<Items>().Logs = true;
								}
								else
								{
									Item.gameObject.GetComponent<Items>().Sticks = true;
								}
							}
							else
							{
								Item.gameObject.GetComponent<Items>().Acorns = true;

							}
						}
						else							
						{
							Item.gameObject.GetComponent<Items>().Acorns = true;
						}
						i = i-1;
					}

					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().QuestTree += 1;


				}
				if(state >=5)
				{
					
					//Destroy(gameObject);
				}
				else
				{
					gameObject.GetComponent<SpriteRenderer>().sprite = NewSprite[state-1];
				}}
		}




	}


	// Use this for initialization
	void Start () {

		if(rock)
		{
			int i = Random.Range(0,3);
			while(i > 0)
			{
				GameObject Item = Instantiate(Item1,gameObject.transform.position,gameObject.transform.rotation,gameObject.transform.parent.transform) as GameObject;
				Item.gameObject.GetComponent<Items>().Rocks = true;
				Item.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-2f,2f),Random.Range(2f,4f));
				i = i-1;
			}
		}

		if(tree)
		{
			gameObject.transform.position = new Vector2(gameObject.transform.position.x,gameObject.transform.position.y + 0.75f);
			int i = Random.Range(0,4);
			while(i > 0)
			{
				GameObject Item = Instantiate(Item1,gameObject.transform.position,gameObject.transform.rotation,gameObject.transform.parent.transform) as GameObject;
				Item.gameObject.GetComponent<Items>().Acorns = true;
				Item.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-2f,2f),Random.Range(2f,4f));
				i = i-1;
			}
		}
		if(Bush)
		{
			int i = Random.Range(0,8);
			while(i > 0)
			{
				GameObject Item = Instantiate(Item1,gameObject.transform.position,gameObject.transform.rotation,gameObject.transform.parent.transform) as GameObject;
				Item.gameObject.GetComponent<Items>().Berries = true;
				Item.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-4f,4f),Random.Range(2f,6f));
				i = i-1;
			}
		}

		if(Onion)
		{
			int i = Random.Range(0,7);
			i = i-5;
			while(i > 0)
			{
				GameObject Item = Instantiate(Item1,gameObject.transform.position,gameObject.transform.rotation,gameObject.transform.parent.transform) as GameObject;
				Item.gameObject.GetComponent<Items>().Berries = true;
				Item.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-4f,4f),Random.Range(2f,6f));
				i = i-1;
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
