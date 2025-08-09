using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour {

	public bool Gold;
	public bool Keys;
	public bool Acorns;
	public bool Sticks;
	public bool Logs;
	public bool Berries;
	public bool Onions;
	public bool Pumpkins;
	public bool Rocks;
	public bool Iron;
	public bool Gems;
	public bool Pelt;
	public bool Bones;
	public bool Souls;

	public bool Scrolls;

	public GameObject Screen;

	public bool Other;
	public int OtherID;

	public Sprite[] Sprites;

	public bool overflow = false;


	// Use this for initialization
	void Start () {

		if(Gold){gameObject.GetComponent<SpriteRenderer>().sprite = Sprites[0];}
		if(Keys){gameObject.GetComponent<SpriteRenderer>().sprite = Sprites[1];}
		if(Acorns){gameObject.GetComponent<SpriteRenderer>().sprite = Sprites[2];}
		if(Sticks){gameObject.GetComponent<SpriteRenderer>().sprite = Sprites[3];}
		if(Logs){gameObject.GetComponent<SpriteRenderer>().sprite = Sprites[4];}
		if(Berries){gameObject.GetComponent<SpriteRenderer>().sprite = Sprites[5];}
		if(Onions){gameObject.GetComponent<SpriteRenderer>().sprite = Sprites[6];}
		if(Pumpkins){gameObject.GetComponent<SpriteRenderer>().sprite = Sprites[7];}
		if(Rocks){gameObject.GetComponent<SpriteRenderer>().sprite = Sprites[8];}
		if(Iron){gameObject.GetComponent<SpriteRenderer>().sprite = Sprites[9];}
		if(Gems){gameObject.GetComponent<SpriteRenderer>().sprite = Sprites[10];}
		if(Pelt){gameObject.GetComponent<SpriteRenderer>().sprite = Sprites[11];}
		if(Bones){gameObject.GetComponent<SpriteRenderer>().sprite = Sprites[12];}
		if(Souls){gameObject.GetComponent<SpriteRenderer>().sprite = Sprites[13];}
		if(Other){gameObject.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().OtherIDSprites[OtherID];}




	}
	
	// Update is called once per frame
	void Update () {
		if(Scrolls)
		{
			if(GameObject.Find("Player").gameObject.transform.position.x > gameObject.transform.position.x)
			{
				gameObject.transform.position = new Vector2(GameObject.Find("Player").gameObject.transform.position.x,gameObject.transform.position.y);

			}


		}


	}
	public bool DoOnce;
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			if(!Scrolls)
			{
			if(!DoOnce)
			{

				gameObject.GetComponent<Rigidbody>().isKinematic = true;
				gameObject.GetComponent<BoxCollider>().isTrigger = true;


			if(Gold){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Gold += Random.Range(1,5);}
			else if(Keys){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Keys += 1;}
			else if(Acorns){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Acorns += 1;}
			else if(Sticks){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Sticks += 1;}
			else if(Logs){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Logs += 1;}
			else if(Berries){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Berries += 1;}
			else if(Onions){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Onions += 1;}
			else if(Pumpkins){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Pumpkins += 1;}
			else if(Rocks){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Rocks += 1;}
			else if(Iron){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Iron += 1;}
			else if(Gems){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Gems += 1;}
			else if(Pelt){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Pelt += 1;}
			else if(Bones){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Bones += 1;}
			else if(Souls){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Souls += 1;}
			else if(Other)
				{
					//gameObject.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().OtherIDSprites[OtherID];
					int i = 0;
					while(i < 12)
					{

						if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().OtherInventory[i] == -1)
						{
							GameObject.Find("Main").gameObject.GetComponent<DataHolder>().OtherInventory[i] = OtherID;
							i = 15;
						}
						else
						{
							i = i + 1;
						}
					}

					if(i != 15)
					{
						overflow = true;
					}

				}
			DoOnce = true;
			}
			if(!overflow)
			{
			Destroy(gameObject);
			}
		}
			else
			{
				GameObject.Find("Main").GetComponent<DataHolder>().ParallaxMult = 0;
				GameObject.Find("Main").GetComponent<DataHolder>().CannotOpenInventory = true;


				Instantiate(Screen,new Vector3(GameObject.Find("Main Camera").gameObject.transform.position.x,GameObject.Find("Main Camera").gameObject.transform.position.y,GameObject.Find("Main Camera").gameObject.transform.position.z + 0.61f),gameObject.transform.rotation,GameObject.Find("Main Camera").gameObject.transform);

				Destroy(GameObject.Find("Darkness").gameObject);

				Destroy(gameObject);
			}

		}




	}




}
