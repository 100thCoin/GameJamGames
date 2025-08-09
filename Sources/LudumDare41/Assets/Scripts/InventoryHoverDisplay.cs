using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHoverDisplay : MonoBehaviour {

	public GameObject Info;


	public int OtherItemID;


	public string Name;
	public string Details;

	public int SignificantNumber;
	public int ReqLev;


	public int InventorySlot;

	public bool Reload;

	public bool NotInventory;


	// Use this for initialization
	void Start () {
		
	}




	// Update is called once per frame
	void Update () {
		if(!NotInventory)
		{
		if( GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ReloadInventory = true)
		{
			Reload = true;
		}



		if(Input.GetKeyDown(KeyCode.Escape) || Reload)
		{
			OtherItemID = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().OtherInventory[InventorySlot];
		}
		if((Input.GetKeyDown(KeyCode.Escape) || Reload) && OtherItemID != -1)
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().OtherIDSprites[OtherItemID];
		}
		if((Input.GetKeyDown(KeyCode.Escape) || Reload) && OtherItemID == -1)
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = null;
		}

		GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ReloadInventory = false;
		}
		else
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().OtherIDSprites[OtherItemID];


		}
	}





	void OnMouseOver()
	{
		if(OtherItemID != -1)
		{
		Vector2 MousePos = GameObject.Find("Main Camera").gameObject.GetComponent<Camera>().ScreenToWorldPoint (Input.mousePosition);

		Info.gameObject.transform.position = new Vector3(MousePos.x - 4,MousePos.y - 1f, -9.68f);

		Name = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().OtherIdNames[OtherItemID];
		Details = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().OtherIDInfo[OtherItemID];
		ReqLev = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().OtherIDLev[OtherItemID];

			if(ReqLev != 0)
			{
		Info.gameObject.transform.FindChild("Info").gameObject.GetComponent<TextMesh>().text = "" + Name + "\n" + Details + "\nNeed Level:\n" + ReqLev;
			}
			else
			{
				Info.gameObject.transform.FindChild("Info").gameObject.GetComponent<TextMesh>().text = "" + Name + "\n" + Details;

			}
		
			if(Input.GetKeyDown(KeyCode.Mouse0) && GameObject.Find("Main").gameObject.GetComponent<DataHolder>().level >= ReqLev && !NotInventory)
			{
				if(OtherItemID == 0 || OtherItemID == 1)
				{
					int TEMP = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().OtherInventory[12];
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().OtherInventory[12] = OtherItemID;
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().OtherInventory[InventorySlot] = TEMP;
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ReloadInventory = true;
				}
				if(OtherItemID == 2 || OtherItemID == 3)
				{
					int TEMP = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().OtherInventory[13];
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().OtherInventory[13] = OtherItemID;
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().OtherInventory[InventorySlot] = TEMP;
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ReloadInventory = true;
				}
				if(OtherItemID == 4 || OtherItemID == 5)
				{
					int TEMP = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().OtherInventory[14];
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().OtherInventory[14] = OtherItemID;
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().OtherInventory[InventorySlot] = TEMP;
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ReloadInventory = true;
				}
				if(OtherItemID == 6 || OtherItemID == 7)
				{
					int TEMP = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().OtherInventory[16];
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().OtherInventory[16] = OtherItemID;
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().OtherInventory[InventorySlot] = TEMP;
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ReloadInventory = true;
				}
				if(OtherItemID == 8 || OtherItemID == 9 || OtherItemID == 10 || OtherItemID == 11 || OtherItemID == 12 || OtherItemID == 13 || OtherItemID == 14)
				{
					int TEMP = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().OtherInventory[15];
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().OtherInventory[15] = OtherItemID;
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().OtherInventory[InventorySlot] = TEMP;
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ReloadInventory = true;
				}

				if(OtherItemID == 15 || OtherItemID == 16 || OtherItemID == 17)
				{
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().HP += OtherItemID - 14;

					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().OtherInventory[InventorySlot] = -1;
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ReloadInventory = true;
				}


			}







		}

	}
	void OnMouseExit()
	{
		Info.gameObject.transform.position = new Vector3(0,0, -500f);


	}



}
