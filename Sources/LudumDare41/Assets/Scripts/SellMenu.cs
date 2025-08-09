using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellMenu : MonoBehaviour {

	public bool WillFail;
	public int Required;
	public int Have;

	public int SellPrice;

	public Sprite Nuetral;
	public Sprite Fail;
	public Sprite Success;

	public GameObject NPC;

	public GameObject ItemDrop;

	public int ItemId;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		if(Have == 0)
		{
			WillFail = true;
		}
		else
		{
			WillFail = false;
		}


	}

	void OnMouseOver()
	{
		if(WillFail)
		{
			gameObject.transform.parent.parent.gameObject.GetComponent<SpriteRenderer>().sprite = Fail;
		}
		else
		{
			gameObject.transform.parent.parent.gameObject.GetComponent<SpriteRenderer>().sprite = Success;

			if(Input.GetKeyDown(KeyCode.Mouse0))
			{
				//NPC.gameObject.GetComponent<NPCInteract>().Locked = true;

				if(ItemId == 2){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Acorns -= Required;}
				if(ItemId == 3){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Sticks -= Required;}
				if(ItemId == 4){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Logs -= Required;}
				if(ItemId == 5){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Berries -= Required;}
				if(ItemId == 6){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Onions -= Required;}
				if(ItemId == 7){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Pumpkins -= Required;}
				if(ItemId == 8){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Rocks -= Required;}
				if(ItemId == 9){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Iron -= Required;}
				if(ItemId == 10){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Gems -= Required;}
				if(ItemId == 11){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Pelt -= Required;}
				if(ItemId == 12){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Bones -= Required;}
				if(ItemId == 13){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Souls -= Required;}
				GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Gold += SellPrice * Required;

			}
		}




	}

	void OnMouseExit()
	{
		gameObject.transform.parent.parent.gameObject.GetComponent<SpriteRenderer>().sprite = Nuetral;


	}




}
