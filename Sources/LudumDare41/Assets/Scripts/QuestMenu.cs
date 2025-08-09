using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMenu : MonoBehaviour {

	public string FetchName;
	public int QuestAmount;
	public int FetchID;

	public bool Incomplete;

	public bool QuestInProgress;


	public Sprite Fail;
	public Sprite Success;
	public Sprite Nuetral;

	public Sprite NewQuestNuetral;
	public Sprite NewQuestGreen;


	public bool Fetch;
	public bool Rocks;
	public bool Trees;
	public bool Graves;
	public bool Kills;


	public GameObject NPC;

	public GameObject Item1;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

		if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().QuestInProgress)
		{
			QuestInProgress = true;
		}
		else
		{
			QuestInProgress = false;
		}


		if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().QuestComplete)
		{
			Incomplete = false;
		}
		else
		{
			Incomplete = true;
		}


	}

	void OnMouseOver()
	{
		if(QuestInProgress)
		{
		if(Incomplete)
		{
			gameObject.transform.parent.parent.gameObject.GetComponent<SpriteRenderer>().sprite = Fail;
		}
		else
		{
			gameObject.transform.parent.parent.gameObject.GetComponent<SpriteRenderer>().sprite = Success;

			if(Input.GetKeyDown(KeyCode.Mouse0))
			{
				//QUEST COMPLETE
				
					if(Fetch)
					{
						if(FetchID == 2){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Acorns -= QuestAmount;}
						if(FetchID == 3){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Sticks -= QuestAmount;}
						if(FetchID == 4){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Logs -= QuestAmount;}
						if(FetchID == 5){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Berries -= QuestAmount;}
						if(FetchID == 6){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Onions -= QuestAmount;}
						if(FetchID == 7){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Pumpkins -= QuestAmount;}
						if(FetchID == 8){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Rocks -= QuestAmount;}
						if(FetchID == 9){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Iron -= QuestAmount;}
						if(FetchID == 10){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Gems -= QuestAmount;}
						if(FetchID == 11){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Pelt -= QuestAmount;}
						if(FetchID == 12){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Bones -= QuestAmount;}
						if(FetchID == 13){GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Souls -= QuestAmount;}
					}





					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().QuestInProgress = false;
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().QuestTree = 0;
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().QuestRock = 0;
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().RobGrave = 0;
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().QuestComplete = false;
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().QuestKill = 0;
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Fetch = false;
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().TreeQuest = false;
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().MineQuest = false;
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().HuntQuest = false;
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().FetchItemID= -1;
				
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ExpCur += 100 * GameObject.Find("Main").gameObject.GetComponent<DataHolder>().level;
				
					int i = Random.Range(10,50);
					while(i > 0)
					{
						GameObject Item = Instantiate(Item1,new Vector3(NPC.gameObject.transform.position.x + 3,NPC.gameObject.transform.position.y+3,0),gameObject.transform.rotation,NPC.gameObject.transform) as GameObject;
						Item.gameObject.GetComponent<Items>().Gold = true;
						Item.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-4f,4f),Random.Range(2f,6f));
						i = i-1;
					}		
					if(NPC.gameObject.GetComponent<NPCInteract>().AlreadyStartedHere)
					{
					NPC.gameObject.GetComponent<NPCInteract>().Locked = true;
					}}
		}

		}
		else
		{

			gameObject.transform.parent.parent.gameObject.GetComponent<SpriteRenderer>().sprite = NewQuestGreen;
			if(Input.GetKeyDown(KeyCode.Mouse0))
			{


				GameObject.Find("Main").gameObject.GetComponent<DataHolder>().QuestInProgress = true;
				GameObject.Find("Main").gameObject.GetComponent<DataHolder>().QuestTree = 0;
				GameObject.Find("Main").gameObject.GetComponent<DataHolder>().QuestRock = 0;
				GameObject.Find("Main").gameObject.GetComponent<DataHolder>().RobGrave = 0;
				GameObject.Find("Main").gameObject.GetComponent<DataHolder>().QuestComplete = false;
				GameObject.Find("Main").gameObject.GetComponent<DataHolder>().QuestKill = 0;
				GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Fetch = false;
				GameObject.Find("Main").gameObject.GetComponent<DataHolder>().TreeQuest = false;
				GameObject.Find("Main").gameObject.GetComponent<DataHolder>().MineQuest = false;
				GameObject.Find("Main").gameObject.GetComponent<DataHolder>().HuntQuest = false;
				GameObject.Find("Main").gameObject.GetComponent<DataHolder>().FetchItemID= -1;
			
				NPC.GetComponent<NPCInteract>().AlreadyStartedHere = true;
			
				if(Fetch)
				{
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Fetch = true;
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().FetchItemID = FetchID;
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().QuestFetch = FetchName;
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().QuestMany = QuestAmount;
				}
			
				if(Rocks)
				{
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().MineQuest = true;
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().QuestMany = QuestAmount;
				}

				if(Trees)
				{
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().TreeQuest = true;
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().QuestMany = QuestAmount;
				}

				if(Graves)
				{
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().RobGraveQuest = true;
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().QuestMany = QuestAmount;
				}

				if(Kills)
				{
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().HuntQuest = true;
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().QuestMany = QuestAmount;
				}
			
			}




		}

	}

	void OnMouseExit()
	{
		if(QuestInProgress)
		{
		gameObject.transform.parent.parent.gameObject.GetComponent<SpriteRenderer>().sprite = Nuetral;
		}
		else
		{
			gameObject.transform.parent.parent.gameObject.GetComponent<SpriteRenderer>().sprite = NewQuestNuetral;

		}

	}


}
