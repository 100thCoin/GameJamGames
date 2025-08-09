using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour {

	public Sprite[] Talk;

	public Sprite[] Talk2;

	public Sprite[] Talk3;


	public bool Talking1;
	public bool Talking2;
	public bool Talking3;

	public int Msg;

	public bool CanInteract;

	public bool TUT;
	public bool TUTEnd;

	public GameObject PickaxeSale;

	public SpriteRenderer ShopKeep;

	public DataHolder Main;

	public GameObject Tip1;
	public GameObject Tip2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Tip1 != null)
		{
			Tip1.SetActive(CanInteract);
			Tip2.SetActive(CanInteract);
		}



	


		if(CanInteract)
		{





			GetComponent<SpriteRenderer>().enabled = true;
			if(Talking1)
			{
				if(Input.GetKeyDown(KeyCode.W)&& Msg < Talk.Length-1)
				{
					Msg++;
				}
				if(Input.GetKeyDown(KeyCode.S) && Msg > 0)
				{
					Msg--;
				}
				GetComponent<SpriteRenderer>().sprite = Talk[Msg];

				if(TUT)
				{
					if(Msg == 18)
					{
						PickaxeSale.SetActive(true);
					}

				}
				if(TUTEnd)
				{
					if(Msg == 1)
					{
						Main.UnlockTele = true;
					}

				}

			}
			else if(Talking2)
			{
				if(Input.GetKeyDown(KeyCode.W) && Msg < Talk2.Length-1)
				{
					Msg++;
				}
					if(Input.GetKeyDown(KeyCode.S) && Msg > 0)
				{
					Msg--;
				}
				GetComponent<SpriteRenderer>().sprite = Talk2[Msg];
			}
			else if(Talking3)
			{
				if(Input.GetKeyDown(KeyCode.W) && Msg < Talk3.Length-1)
				{
					Msg++;
				}
				if(Input.GetKeyDown(KeyCode.S) && Msg > 0)
				{
					Msg--;
				}
				GetComponent<SpriteRenderer>().sprite = Talk3[Msg];
			}


		}
		else
		{
			GetComponent<SpriteRenderer>().enabled = false;

		}



	}


	void OnTriggerStay(Collider other)
	{
		if(other.name == "Player")
		{
			CanInteract = true;

			if(other.transform.position.x < ShopKeep.transform.position.x)
			{
				ShopKeep.flipX = true;
			}
			else
			{
				ShopKeep.flipX = false;
			}



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
