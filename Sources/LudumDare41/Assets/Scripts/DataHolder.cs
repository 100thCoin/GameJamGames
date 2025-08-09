using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour {

	public int Town;
	public int HP;

	public bool FastMode;

	public int QuestKill;
	public int QuestRock;
	public int QuestTree;
	public int QuestDig;
	public int RobGrave;
	public int QuestItem;

	public float ParallaxMult;

	public int Gold;
	public int Keys;
	public int Acorns;
	public int Sticks;
	public int Logs;
	public int Berries;
	public int Onions;
	public int Pumpkins;
	public int Rocks;
	public int Iron;
	public int Gems;
	public int Pelt;
	public int Bones;
	public int Souls;

	public int ShovelTier;
	public int PickaxeTier;
	public int AxeTier;
	public int SwordDamage;
	public int ArmorClass;

	public int level;

	public float ExpReq;
	public float ExpCur;

	public int[] OtherInventory;

	public bool CannotOpenInventory;////////////////////////////////////////////////////////////////////////////////////

	public string[] OtherIdNames;
	public string[] OtherIDInfo;
	public int[] OtherIDLev;
	public Sprite[] OtherIDSprites;


	public bool ReloadInventory;

	public GameObject Hearts;
	public Sprite[] Heart;

	public string Quest;
	GameObject QuestText;

	public bool Fetch;
	public int QuestMany;
	public string QuestFetch;
	public int FetchItemID;

	public bool QuestComplete;

	public bool MultiTownQuest;

	public bool MineQuest;
	public bool TreeQuest;
	public bool HuntQuest;
	public bool RobGraveQuest;
	public bool QuestInProgress;
	public GameObject dedMessage;
	// Use this for initialization
	void Start () {

		if(Screen.width >- 1536)
		{


		Screen.SetResolution(1536,1024,false);
		}
		else
		{
			Screen.SetResolution(1536/2,1024/2,false);

		}



		ExpReq = 100;
		gameObject.name = "Main";
	}
	public bool DieOnce;
	// Update is called once per frame
	void Update () {

		Hearts.gameObject.GetComponent<SpriteRenderer>().sprite = Heart[HP];

		if(HP == 0 && !DieOnce)
		{
			DieOnce = true;
			CannotOpenInventory = true;
			GameObject.Find("Player").gameObject.transform.position = new Vector3(0,0,-500);
			Instantiate(dedMessage,new Vector3(GameObject.Find("Main Camera").gameObject.transform.position.x,GameObject.Find("Main Camera").gameObject.transform.position.y,GameObject.Find("Main Camera").gameObject.transform.position.z + 0.61f),gameObject.transform.rotation,GameObject.Find("Main Camera").gameObject.transform);
		}



		if(Fetch)
		{
			Quest = "Bring me " + QuestMany +"\n" + QuestFetch;
			MultiTownQuest = false;

			if(FetchItemID ==2){QuestItem = Acorns;}
			if(FetchItemID ==3){QuestItem = Sticks;}
			if(FetchItemID ==4){QuestItem = Logs;}
			if(FetchItemID ==5){QuestItem = Berries;}
			if(FetchItemID ==6){QuestItem = Onions;}
			if(FetchItemID ==7){QuestItem = Pumpkins;}
			if(FetchItemID ==8){QuestItem = Rocks;}
			if(FetchItemID ==9){QuestItem = Iron;}
			if(FetchItemID ==10){QuestItem = Gems;}
			if(FetchItemID ==11){QuestItem = Pelt;}
			if(FetchItemID ==12){QuestItem = Bones;}
			if(FetchItemID ==13){QuestItem = Souls;}


			if(FetchItemID ==2 && Acorns >= QuestMany){QuestComplete = true;}
			if(FetchItemID ==3 && Sticks >= QuestMany){QuestComplete = true;}
			if(FetchItemID ==4 && Logs >= QuestMany){QuestComplete = true;}
			if(FetchItemID ==5 && Berries >= QuestMany){QuestComplete = true;}
			if(FetchItemID ==6 && Onions >= QuestMany){QuestComplete = true;}
			if(FetchItemID ==7 && Pumpkins >= QuestMany){QuestComplete = true;}
			if(FetchItemID ==8 && Rocks >= QuestMany){QuestComplete = true;}
			if(FetchItemID ==9 && Iron >= QuestMany){QuestComplete = true;}
			if(FetchItemID ==10 && Gems >= QuestMany){QuestComplete = true;}
			if(FetchItemID ==11 && Pelt >= QuestMany){QuestComplete = true;}
			if(FetchItemID ==12 && Bones >= QuestMany){QuestComplete = true;}
			if(FetchItemID ==13 && Souls >= QuestMany){QuestComplete = true;}

			Quest = Quest + "\n" + QuestItem + "/" + QuestMany;
		}

		if(MineQuest)
		{
			Quest = "Mine " + QuestMany + " rocks.\n"+ QuestRock + "/" + QuestMany;
			if(QuestRock >= QuestMany){QuestComplete = true;}
		}

		if(TreeQuest)
		{
			Quest = "Chop down " + QuestMany + " trees.\n"+ QuestTree + "/" + QuestMany;
			if(QuestTree >= QuestMany){QuestComplete = true;}

		}

		if(HuntQuest)
		{
			Quest = "Kill " + QuestMany + " monsters.\n"+ QuestKill + "/" + QuestMany;
			if(QuestKill >= QuestMany){QuestComplete = true;}

		}

		if(RobGraveQuest)
		{
			Quest = "Rob a grave.\n"+ RobGrave + "/" + QuestMany;
			if(RobGrave >= QuestMany){QuestComplete = true;}

		}


		if(OtherInventory[12] == 0)
		{
			GameObject.Find("Sword").gameObject.GetComponent<ItemChange>().Axe = OtherIDSprites[0];
			AxeTier = 1;
		}
		if(OtherInventory[12] == 1)
		{
			GameObject.Find("Sword").gameObject.GetComponent<ItemChange>().Axe = OtherIDSprites[1];
			AxeTier = 2;
		}

		if(OtherInventory[13] == 2)
		{
			GameObject.Find("Sword").gameObject.GetComponent<ItemChange>().Pick = OtherIDSprites[2];

			PickaxeTier = 1;
		}
		if(OtherInventory[13] == 3)
		{
			GameObject.Find("Sword").gameObject.GetComponent<ItemChange>().Pick = OtherIDSprites[3];

			PickaxeTier = 2;
		}

		if(OtherInventory[14] == 4)
		{
			GameObject.Find("Sword").gameObject.GetComponent<ItemChange>().Shovel = OtherIDSprites[4];

			ShovelTier = 1;
		}
		if(OtherInventory[14] == 5)
		{
			GameObject.Find("Sword").gameObject.GetComponent<ItemChange>().Shovel = OtherIDSprites[5];

			ShovelTier = 2;
		}

		if(OtherInventory[16] == 6)
		{
			GameObject.Find("Player").gameObject.GetComponent<CharMover>().ArmorClass = 1;

			ArmorClass = 1;
		}
		if(OtherInventory[16] == 7)
		{
			GameObject.Find("Player").gameObject.GetComponent<CharMover>().ArmorClass = 2;

			ArmorClass = 2;
		}

		if(OtherInventory[15] == 8)
		{
			GameObject.Find("Sword").gameObject.GetComponent<ItemChange>().Sword = OtherIDSprites[8];

			SwordDamage = 4;
		}
		if(OtherInventory[15] == 9)
		{
			GameObject.Find("Sword").gameObject.GetComponent<ItemChange>().Sword = OtherIDSprites[9];

			SwordDamage = 8;
		}
		if(OtherInventory[15] == 10)
		{
			GameObject.Find("Sword").gameObject.GetComponent<ItemChange>().Sword = OtherIDSprites[10];

			SwordDamage = 7;
		}
		if(OtherInventory[15] == 11)
		{
			GameObject.Find("Sword").gameObject.GetComponent<ItemChange>().Sword = OtherIDSprites[11];

			SwordDamage = 12;
		}
		if(OtherInventory[15] == 12)
		{
			GameObject.Find("Sword").gameObject.GetComponent<ItemChange>().Sword = OtherIDSprites[12];

			SwordDamage = 15;
		}
		if(OtherInventory[15] == 13)
		{
			GameObject.Find("Sword").gameObject.GetComponent<ItemChange>().Sword = OtherIDSprites[13];

			SwordDamage = 18;
		}
		if(OtherInventory[15] == 14)
		{
			GameObject.Find("Sword").gameObject.GetComponent<ItemChange>().Sword = OtherIDSprites[14];

			SwordDamage = 30;
		}






	}
}
