using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeRequest : MonoBehaviour {

	public bool WillFail;
	public int Required;
	public int Have;


	public Sprite Nuetral;
	public Sprite Fail;
	public Sprite Success;

	public GameObject NPC;

	public GameObject ItemDrop;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Required > Have)
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
				NPC.gameObject.GetComponent<NPCInteract>().Locked = true;

				if(NPC.GetComponent<NPCInteract>().Desired == 2){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Acorns -= Required;}
				if(NPC.GetComponent<NPCInteract>().Desired == 3){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Sticks -= Required;}
				if(NPC.GetComponent<NPCInteract>().Desired == 4){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Logs -= Required;}
				if(NPC.GetComponent<NPCInteract>().Desired == 5){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Berries -= Required;}
				if(NPC.GetComponent<NPCInteract>().Desired == 6){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Onions -= Required;}
				if(NPC.GetComponent<NPCInteract>().Desired == 7){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Pumpkins -= Required;}
				if(NPC.GetComponent<NPCInteract>().Desired == 8){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Rocks -= Required;}
				if(NPC.GetComponent<NPCInteract>().Desired == 9){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Iron -= Required;}
				if(NPC.GetComponent<NPCInteract>().Desired == 10){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Gems -= Required;}
				if(NPC.GetComponent<NPCInteract>().Desired == 11){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Pelt -= Required;}
				if(NPC.GetComponent<NPCInteract>().Desired == 12){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Bones -= Required;}
				if(NPC.GetComponent<NPCInteract>().Desired == 13){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Souls -= Required;}

				GameObject Item = Instantiate(ItemDrop,new Vector3(NPC.gameObject.transform.position.x + 3,NPC.gameObject.transform.position.y + 3,NPC.gameObject.transform.position.z),gameObject.transform.rotation,NPC.gameObject.transform.parent.transform) as GameObject;
				Item.gameObject.GetComponent<Items>().Other = true;
				Item.gameObject.GetComponent<Items>().OtherID = NPC.gameObject.GetComponent<NPCInteract>().Reward;

				Item.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-2f,2f),Random.Range(2f,4f));


			}
		}




	}

	void OnMouseExit()
	{
		gameObject.transform.parent.parent.gameObject.GetComponent<SpriteRenderer>().sprite = Nuetral;


	}




}
