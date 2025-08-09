using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreBuyItem : MonoBehaviour {

	public bool Pick;
	public bool Ladderite;
	public bool Ladderite10;

	public bool Lantern;

	public int Price;
	public bool CanInteract;

	public DataHolder Main;

	public Sprite NEM;
	public Sprite ThisSprite;

	public float NEMTimer;

	public GameObject Dialogueo;

	public RedicleFollow Red;

	// Use this for initialization
	void Start () {
		Main = GameObject.Find("Main").GetComponent<DataHolder>();
		ThisSprite = GetComponent<SpriteRenderer>().sprite;
		if(Ladderite10)
		{
			Dialogueo = Main.DiaguoLad10;
		}

	}
	
	// Update is called once per frame
	void Update () {

		NEMTimer+= Time.deltaTime;

		if(CanInteract && Input.GetKeyDown(KeyCode.W))
		{
			if(Price < Main.Money)
			{
				Main.Money -= Price;
				Main.TotalSpent += Price;

				Main.GetComponent<HeartBeat>().MoneyCount.text = "Money: " + Main.Money;

				if(Pick)
				{
					Main.UnlockedPick = true;
					Dialogueo.GetComponent<Dialogue>().Msg = 0;
					Dialogueo.GetComponent<Dialogue>().Talking2 = true;
					Dialogueo.GetComponent<Dialogue>().Talking1 = false;
					Red.Mode = 0;
					Destroy(gameObject);
				}

				if(Ladderite)
				{
					Main.RefinedLadderite++;
					if(Ladderite10)
					{
						Main.TUTHasEverLadderite = true;
						if(Main.RefinedLadderite >= 10)
						{
							Dialogueo.GetComponent<Dialogue>().Msg = 0;
							Dialogueo.GetComponent<Dialogue>().Talking3 = true;
							Dialogueo.GetComponent<Dialogue>().Talking2 = false;
							Destroy(gameObject);
						}

					}
				}

				if(Lantern)
				{
					Main.Lanterns++;
					Main.TUTHasEverLantern = true;


				}

			}
			else
			{
				NEMTimer = 0;
			}


		}
		if(NEMTimer < 2)
		{
			GetComponent<SpriteRenderer>().sprite = NEM;
		}
		else
		{
			GetComponent<SpriteRenderer>().sprite = ThisSprite;
		}
		if(!CanInteract)
		{
			GetComponent<SpriteRenderer>().enabled = false;

		}
		else
		{
			GetComponent<SpriteRenderer>().enabled = true;

		}


	}


	void OnTriggerStay(Collider other)
	{
		if(other.name == "Player")
		{
			CanInteract = true;
		}
	}
	void OnTriggerExit(Collider other)
	{
		if(other.name == "Player")
		{
			CanInteract = false;
		}
	}

}
