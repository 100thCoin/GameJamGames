using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteract : MonoBehaviour {

	public bool Trade;

	public bool Quest;

	public bool Buy;

	public bool Sell;

	public GameObject Player;

	public int Desired;
	public int WieViele;
	public int Reward;

	public GameObject TradeIn;
	public GameObject TradeOut;
	public GameObject TradeConfirm;

	public GameObject TradeIn2;
	public GameObject TradeIn3;
	public GameObject TradeConfirm2;
	public GameObject TradeConfirm3;
	public int WieViele2;
	public int WieViele3;

	public bool FetchQuest;
	public bool KillQuest;
	public bool GraveQuest;
	public bool RockQuest;
	public bool TreeQuest;

	public bool Override;
	public int DesO;
	public int RewO;

	public Sprite[] List;

	public bool Locked;

	public int FetchX;
	public string FetchName;
	public int QuestAmount;

	public bool AlreadyStartedHere;

	// Use this for initialization
	void Start () {
		Player = GameObject.Find("Player").gameObject;
		if(Trade)
		{
			TradeIn = GameObject.Find("NPCInfoTrade").gameObject.transform.FindChild("Info").FindChild("Axe (12)").gameObject;
			TradeOut = GameObject.Find("NPCInfoTrade").gameObject.transform.FindChild("Info").FindChild("Axe (13)").gameObject;
			TradeConfirm = GameObject.Find("NPCInfoTrade").gameObject.transform.FindChild("Info").FindChild("GameObject").gameObject;
			Desired = Random.Range(2,14);


			Reward = Random.Range(0,18);

			if(Override)
			{

				Desired = DesO;
				Reward = RewO;
			}


			if(Reward == 0){WieViele = 10;}
			if(Reward == 1){WieViele = 30;}
			if(Reward == 2){WieViele = 10;}
			if(Reward == 3){WieViele = 30;}
			if(Reward == 4){WieViele = 10;}
			if(Reward == 5){WieViele = 30;}
			if(Reward == 6){WieViele = 60;}
			if(Reward == 7){WieViele = 100;}
			if(Reward == 8){WieViele = 10;}
			if(Reward == 9){WieViele = 35;}
			if(Reward == 10){WieViele = 30;}
			if(Reward == 11){WieViele = 100;}
			if(Reward == 12){WieViele = 120;}
			if(Reward == 13){WieViele = 150;}
			if(Reward == 14){WieViele = 300;}
			if(Reward == 15){WieViele = 20;}
			if(Reward == 16){WieViele = 40;}
			if(Reward == 17){WieViele = 60;}

			if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Town == 0)
			{
				if(Desired == 2 || Desired == 5 || Desired == 8 || Desired == 11)
				{
					WieViele = WieViele / 10;
				}
			}


			if(Desired == 3 || Desired == 6 || Desired == 9 || Desired == 12)
			{
				WieViele = WieViele / 3;
			}
			if(Desired == 4 || Desired == 7 || Desired == 10 || Desired == 13)
			{
				WieViele = WieViele / 10;
			}
			WieViele = WieViele + Random.Range(0,3);
		}

		if(Buy)
		{
			TradeOut = GameObject.Find("NPCInfoBuy").gameObject.transform.FindChild("Info").FindChild("Axe (13)").gameObject;
			TradeConfirm = GameObject.Find("NPCInfoBuy").gameObject.transform.FindChild("Info").FindChild("GameObject").gameObject;
			Reward = Random.Range(0,18);
			if(Override)
			{
				Reward = RewO;
			}
			if(Reward == 0){WieViele = 30;}
			if(Reward == 1){WieViele = 70;}
			if(Reward == 2){WieViele = 30;}
			if(Reward == 3){WieViele = 70;}
			if(Reward == 4){WieViele = 30;}
			if(Reward == 5){WieViele = 70;}
			if(Reward == 6){WieViele = 120;}
			if(Reward == 7){WieViele = 220;}
			if(Reward == 8){WieViele = 10;}
			if(Reward == 9){WieViele = 100;}
			if(Reward == 10){WieViele = 100;}
			if(Reward == 11){WieViele = 160;}
			if(Reward == 12){WieViele = 240;}
			if(Reward == 13){WieViele = 400;}
			if(Reward == 14){WieViele = 1000;}
			if(Reward == 15){WieViele = 50;}
			if(Reward == 16){WieViele = 100;}
			if(Reward == 17){WieViele = 150;}

			WieViele = WieViele + Random.Range(-15,20);


		}

		if(Sell)
		{
			TradeIn = GameObject.Find("NPCInfoSell").gameObject.transform.FindChild("Info").FindChild("Axe (12)").gameObject;
			TradeIn2 = GameObject.Find("NPCInfoSell").gameObject.transform.FindChild("Info").FindChild("Axe (13)").gameObject;
			TradeIn3 = GameObject.Find("NPCInfoSell").gameObject.transform.FindChild("Info").FindChild("Axe (14)").gameObject;

			TradeConfirm = GameObject.Find("NPCInfoSell").gameObject.transform.FindChild("Info").FindChild("GameObject").gameObject;
			TradeConfirm2 = GameObject.Find("NPCInfoSell").gameObject.transform.FindChild("Info").FindChild("GameObject (1)").gameObject;
			TradeConfirm3 = GameObject.Find("NPCInfoSell").gameObject.transform.FindChild("Info").FindChild("GameObject (2)").gameObject;


			Desired = Random.Range(0,4);

			WieViele = Random.Range(1,2);
			WieViele2 = Random.Range(2,6);
			WieViele3 = Random.Range(10,30);


			//WieViele = WieViele + Random.Range(0,3);
		}

		if(Quest)
		{
			TradeConfirm = GameObject.Find("NPCInfoQuest").gameObject.transform.FindChild("Info").FindChild("GameObject").gameObject;

			int R = Random.Range(0,9);

			if(R == 0){FetchQuest = true;}
			if(R == 1){FetchQuest = true;}
			if(R == 2){KillQuest = true;}
			if(R == 3){KillQuest = true;}
			if(R == 4){RockQuest = true;}
			if(R == 5){RockQuest = true;}
			if(R == 6){TreeQuest = true;}
			if(R == 7){TreeQuest = true;}
			if(R == 8){GraveQuest = true;}

			if(RockQuest || TreeQuest || KillQuest)
			{
				QuestAmount = Random.Range(3,7);
			}
			if(GraveQuest)
			{
				QuestAmount = 1;
			}

			if(FetchQuest)
			{
				FetchX = Random.Range(2,14);

				if(FetchX == 2 || FetchX == 5 || FetchX == 8 || FetchX == 11)
				{
					QuestAmount = Random.Range(15,35);
				}
				if(FetchX == 3 || FetchX == 6 || FetchX == 9 || FetchX == 12)
				{
					QuestAmount = Random.Range(5,15);
				}
				if(FetchX == 4 || FetchX == 7 || FetchX == 10 || FetchX == 13)
				{
					QuestAmount = Random.Range(2,5);
				}

				if(FetchX == 2){FetchName = "acorns";}
				if(FetchX == 3){FetchName = "sticks";}
				if(FetchX == 4){FetchName = "logs";}
				if(FetchX == 5){FetchName = "berries";}
				if(FetchX == 6){FetchName = "onions";}
				if(FetchX == 7){FetchName = "pumpkins";}
				if(FetchX == 8){FetchName = "rocks";}
				if(FetchX == 9){FetchName = "iron";}
				if(FetchX == 10){FetchName = "gems";}
				if(FetchX == 11){FetchName = "pelts";}
				if(FetchX == 12){FetchName = "bones";}
				if(FetchX == 13){FetchName = "souls";}
			}





			//WieViele = WieViele + Random.Range(0,3);
		}


	}
	


	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag == "Player" && !Locked)
		{
			if(Trade)
			{
			GameObject.Find("NPCInfoTrade").gameObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y + 7f, -9.4f);
			GameObject.Find("NPCInfoTrade").gameObject.transform.FindChild("Info").gameObject.GetComponent<TextMesh>().text = "" + WieViele;
			int CurrentAmount = 0;
			if(Desired == 2){CurrentAmount = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Acorns;}
			if(Desired == 3){CurrentAmount = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Sticks;}
			if(Desired == 4){CurrentAmount = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Logs;}
			if(Desired == 5){CurrentAmount = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Berries;}
			if(Desired == 6){CurrentAmount = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Onions;}
			if(Desired == 7){CurrentAmount = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Pumpkins;}
			if(Desired == 8){CurrentAmount = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Rocks;}
			if(Desired == 9){CurrentAmount = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Iron;}
			if(Desired == 10){CurrentAmount = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Gems;}
			if(Desired == 11){CurrentAmount = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Pelt;}
			if(Desired == 12){CurrentAmount = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Bones;}
			if(Desired == 13){CurrentAmount = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Souls;}
			TradeIn.gameObject.GetComponent<SpriteRenderer>().sprite = List[Desired];
			TradeOut.gameObject.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().OtherIDSprites[Reward];
			TradeOut.gameObject.GetComponent<InventoryHoverDisplay>().OtherItemID = Reward;
			TradeConfirm.gameObject.GetComponent<TradeRequest>().Required = WieViele;
			TradeConfirm.gameObject.GetComponent<TradeRequest>().Have = CurrentAmount;
			TradeConfirm.gameObject.GetComponent<TradeRequest>().NPC = gameObject;
			}
			if(Buy)
			{
				GameObject.Find("NPCInfoBuy").gameObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y + 7f, -9.4f);
				GameObject.Find("NPCInfoBuy").gameObject.transform.FindChild("Info").gameObject.GetComponent<TextMesh>().text = "" + WieViele;
				int CurrentAmount = 0;
				CurrentAmount = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Gold;
				TradeOut.gameObject.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().OtherIDSprites[Reward];
				TradeOut.gameObject.GetComponent<InventoryHoverDisplay>().OtherItemID = Reward;
				TradeConfirm.gameObject.GetComponent<TradeRequest>().Required = WieViele;
				TradeConfirm.gameObject.GetComponent<TradeRequest>().Have = CurrentAmount;
				TradeConfirm.gameObject.GetComponent<TradeRequest>().NPC = gameObject;
			}

			if(Sell)
			{

				int CurrentAmount = 0;
				int CurrentAmount2 = 0;
				int CurrentAmount3 = 0;


				if(Desired == 0){CurrentAmount = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Acorns;
				CurrentAmount2 = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Sticks;
				CurrentAmount3 = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Logs;}
				if(Desired == 1){CurrentAmount = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Berries;
				CurrentAmount2 = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Onions;
				CurrentAmount3 = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Pumpkins;}
				if(Desired == 2){CurrentAmount = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Rocks;
				CurrentAmount2 = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Iron;
				CurrentAmount3 = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Gems;}
				if(Desired == 3){CurrentAmount = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Pelt;
				CurrentAmount2 = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Bones;
				CurrentAmount3 = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Souls;}



				GameObject.Find("NPCInfoSell").gameObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y + 7f, -9.4f);
				GameObject.Find("NPCInfoSell").gameObject.transform.FindChild("Info").gameObject.GetComponent<TextMesh>().text = "" + WieViele * CurrentAmount + "g\n" + WieViele2*CurrentAmount2 + "g\n" + WieViele3 *CurrentAmount3 + "g"; 


				TradeIn.gameObject.GetComponent<SpriteRenderer>().sprite = List[3 * (Desired+1)- 3 +2];
				TradeIn2.gameObject.GetComponent<SpriteRenderer>().sprite = List[3 * (Desired+1)- 2 +2];
				TradeIn3.gameObject.GetComponent<SpriteRenderer>().sprite = List[3 * (Desired+1)- 1 +2];

				int ItemID1 = 3 * (Desired+1)- 3 +2;
				int ItemID2 = 3 * (Desired+1)- 2 +2;
				int ItemID3 = 3 * (Desired+1)- 1 +2;

				TradeConfirm.gameObject.GetComponent<SellMenu>().SellPrice = WieViele;
				TradeConfirm.gameObject.GetComponent<SellMenu>().Required = CurrentAmount;
				TradeConfirm.gameObject.GetComponent<SellMenu>().Have = CurrentAmount;
				TradeConfirm.gameObject.GetComponent<SellMenu>().NPC = gameObject;
				TradeConfirm.gameObject.GetComponent<SellMenu>().ItemId = ItemID1;


				TradeConfirm2.gameObject.GetComponent<SellMenu>().SellPrice = WieViele2;
				TradeConfirm2.gameObject.GetComponent<SellMenu>().Required = CurrentAmount2;
				TradeConfirm2.gameObject.GetComponent<SellMenu>().Have = CurrentAmount2;
				TradeConfirm2.gameObject.GetComponent<SellMenu>().NPC = gameObject;
				TradeConfirm2.gameObject.GetComponent<SellMenu>().ItemId = ItemID2;

				TradeConfirm3.gameObject.GetComponent<SellMenu>().SellPrice = WieViele3;
				TradeConfirm3.gameObject.GetComponent<SellMenu>().Required = CurrentAmount3;
				TradeConfirm3.gameObject.GetComponent<SellMenu>().Have = CurrentAmount3;
				TradeConfirm3.gameObject.GetComponent<SellMenu>().NPC = gameObject;
				TradeConfirm3.gameObject.GetComponent<SellMenu>().ItemId = ItemID3;

			}

			if(Quest)
			{
				GameObject.Find("NPCInfoQuest").gameObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y + 7f, -9.4f);
			
				if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().QuestInProgress)
				{
					GameObject.Find("NPCInfoQuest").gameObject.transform.FindChild("Info").gameObject.GetComponent<TextMesh>().text = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Quest;
				
					GameObject Main = GameObject.Find("Main").gameObject;
				
					TradeConfirm.gameObject.GetComponent<QuestMenu>().QuestAmount = Main.GetComponent<DataHolder>().QuestMany;
					TradeConfirm.gameObject.GetComponent<QuestMenu>().FetchName =  Main.GetComponent<DataHolder>().QuestFetch;
					TradeConfirm.gameObject.GetComponent<QuestMenu>().Fetch =  Main.GetComponent<DataHolder>().Fetch;
					TradeConfirm.gameObject.GetComponent<QuestMenu>().FetchID =  Main.GetComponent<DataHolder>().FetchItemID;
					TradeConfirm.gameObject.GetComponent<QuestMenu>().Graves =  Main.GetComponent<DataHolder>().RobGraveQuest;
					TradeConfirm.gameObject.GetComponent<QuestMenu>().Trees =  Main.GetComponent<DataHolder>().TreeQuest;
					TradeConfirm.gameObject.GetComponent<QuestMenu>().Rocks =  Main.GetComponent<DataHolder>().MineQuest;
					TradeConfirm.gameObject.GetComponent<QuestMenu>().Kills =  Main.GetComponent<DataHolder>().HuntQuest;
				
				
				
				}
				else
				{
					if(FetchQuest)
					{
						GameObject.Find("NPCInfoQuest").gameObject.transform.FindChild("Info").gameObject.GetComponent<TextMesh>().text = "Bring me " + QuestAmount + "\n" + FetchName;
					}
					if(RockQuest)
					{
						GameObject.Find("NPCInfoQuest").gameObject.transform.FindChild("Info").gameObject.GetComponent<TextMesh>().text = "Mine through " + QuestAmount + "\nrocks";
					}
					if(TreeQuest)
					{
						GameObject.Find("NPCInfoQuest").gameObject.transform.FindChild("Info").gameObject.GetComponent<TextMesh>().text = "Cut down " + QuestAmount + "\ntrees";
					}
					if(KillQuest)
					{
						GameObject.Find("NPCInfoQuest").gameObject.transform.FindChild("Info").gameObject.GetComponent<TextMesh>().text = "Destroy " + QuestAmount + "\nmonsters";
					}
					if(GraveQuest)
					{
						GameObject.Find("NPCInfoQuest").gameObject.transform.FindChild("Info").gameObject.GetComponent<TextMesh>().text = "Rob a grave";
					}
				
				
					TradeConfirm.gameObject.GetComponent<QuestMenu>().QuestAmount = QuestAmount;
					TradeConfirm.gameObject.GetComponent<QuestMenu>().FetchName = FetchName;
					TradeConfirm.gameObject.GetComponent<QuestMenu>().Fetch = FetchQuest;
					TradeConfirm.gameObject.GetComponent<QuestMenu>().FetchID = FetchX;
					TradeConfirm.gameObject.GetComponent<QuestMenu>().Graves = GraveQuest;
					TradeConfirm.gameObject.GetComponent<QuestMenu>().Trees = TreeQuest;
					TradeConfirm.gameObject.GetComponent<QuestMenu>().Rocks = RockQuest;
					TradeConfirm.gameObject.GetComponent<QuestMenu>().Kills = KillQuest;
				
				
				}





				TradeConfirm.gameObject.GetComponent<QuestMenu>().NPC = gameObject;
			}


		}

		if(Locked)
		{
			if(Trade)
			{
				GameObject.Find("NPCInfoTrade").gameObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y + 4f, -500f);
			}
			if(Buy)
			{
				GameObject.Find("NPCInfoBuy").gameObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y + 4f, -500f);
			}
			if(Sell)
			{
				GameObject.Find("NPCInfoSell").gameObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y + 4f, -500f);
			}
			if(Quest)
			{
				GameObject.Find("NPCInfoQuest").gameObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y + 4f, -500f);
			}
		}



	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			if(Trade)
			{
			GameObject.Find("NPCInfoTrade").gameObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y + 4f, -500f);
			}
			if(Buy)
			{
			GameObject.Find("NPCInfoBuy").gameObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y + 4f, -500f);
			}
			if(Sell)
			{
				GameObject.Find("NPCInfoSell").gameObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y + 4f, -500f);
			}
			if(Quest)
			{
				GameObject.Find("NPCInfoQuest").gameObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y + 4f, -500f);
			}
		}



	}



}
