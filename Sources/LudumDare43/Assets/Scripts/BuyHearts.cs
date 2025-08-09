using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyHearts : MonoBehaviour {

	public bool SeeMary;
	public MaryController Mary;

	public bool GraceFrame;

	public GameObject Windoow;

	public Sprite  WindowSpr;

	public Sprite NotEnoughGold;

	public int Price;




	public bool DoOnce;


	void Start()
	{
		/*
		pickone = Random.Range(0,8);
	
		if(pickone >= 3)
		{
			pickone++;
		}*/






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





		if(Input.GetKeyDown(KeyCode.Space) && SeeMary)
		{


			if(Mary.Gold >= Price)
			{

				Mary.Gold -= Price;
				Mary.HP += 1;
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
				Windoow.GetComponent<SpriteRenderer>().sprite = WindowSpr;


			}


		}




	}

}
