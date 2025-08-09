using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseLamb : MonoBehaviour {

	public bool SeeMary;
	public MaryController Mary;

	public GameObject Spawnin;
	public bool GraceFrame;

	public GameObject Windoow;

	public GameObject Sheep;

	public int Type;

	public Sprite[] SheepSpr;

	public Sprite[] WindowSpr;

	public Sprite NotEnoughGold;

	public int[] Price;

	public GameObject[] Spawnem;


	public int pickone;



	public bool DoOnce;

	public bool ClerkDead;

	void Start()
	{
		/*
		pickone = Random.Range(0,8);
	
		if(pickone >= 3)
		{
			pickone++;
		}*/

		Windoow.GetComponent<SpriteRenderer>().sprite = WindowSpr[pickone];
		Sheep.GetComponent<SpriteRenderer>().sprite = SheepSpr[pickone];

		Spawnin = Spawnem[pickone];





	}




	void FixedUpdate()
	{

		if(!GraceFrame)
		{
			SeeMary = false;
			Windoow.SetActive(false);
			DoOnce = false;
		}
		else
		{
			GraceFrame = false;
		}



	}


	void Update()
	{
		if(ClerkDead)
		{
			GameObject newsheep =	Instantiate(Spawnin,new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,0),gameObject.transform.rotation)as GameObject;
			newsheep.GetComponent<AI_Sheep>().Mary = GameObject.Find("Mary");


			Destroy(gameObject.transform.parent.gameObject);
		}






		if(Input.GetKeyDown(KeyCode.Space) && SeeMary)
		{


			if(Mary.Gold >= Price[pickone])
			{
				GameObject newsheep =	Instantiate(Spawnin,new Vector3(gameObject.transform.position.x,gameObject.transform.position.y-24,0),gameObject.transform.rotation)as GameObject;
				newsheep.GetComponent<AI_Sheep>().Mary = Mary.gameObject;

				Mary.Gold -= Price[pickone];

			}
			else
			{
				Windoow.GetComponent<SpriteRenderer>().sprite = NotEnoughGold;

			}



		}


	}


	void OnTriggerStay(Collider other)
	{

		if(other.gameObject.name == "Mary")
		{
			
			Mary = other.gameObject.GetComponent<MaryController>();
			SeeMary = true;
			GraceFrame = true;
			Windoow.SetActive(true);

			if(!DoOnce)
			{
				DoOnce = true;
				Windoow.GetComponent<SpriteRenderer>().sprite = WindowSpr[pickone];


			}


		}




	}

}
