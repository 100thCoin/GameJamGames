using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ItemStat{

	public string Name;
	public string Desc;
	public Sprite Icon;
	public float Price;
	public int Count;
	public TextMesh DHMOText;
	public GameObject AssociatedGO;



}


public class DataHolder : MonoBehaviour {

	public bool Dead;
	public float DeadTimer;

	public int TotalEarned;
	public int TotalHeart;
	public int TotalSpent;

	public bool FreezeCam;
	public bool WinGame;

	public bool Outside;

	public bool IsPlayerTele;
	public GameObject[] LightSources;

	public bool UpdateLights;
	public GameObject DarkPlane;

	public ItemStat[] Items;
	public int Money;
	public int RefinedLadderite;
	public int Lanterns;

	public UIPlaceItems UIPlace;

	public GameObject Player;

	public bool UnlockTele;

	public bool UnlockedPick;

	public bool PostTUT;
	public bool TUTCollectedRocks;
	public bool TUTHasEverLadderite;
	public bool TUTHasEverLantern;

	public bool UnlockLantern;

	public GameObject ShopLanterns;
	public GameObject ShopLadderite;

	public GameObject DiaguoLad10;

	public float TimeElaspsed;

	public float WinTimer;

	public GameObject WInScreen;
	public GameObject DeadScreen;

	public GameObject Deathparts;
	public bool DieOnce;

	public AudioSource Layer;

	public bool WinOnce;
	public AudioClip Victory;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		TimeElaspsed += Time.deltaTime;
		if(WinGame)
		{

			if(!WinOnce)
			{
				WinOnce = true;
				Layer.clip = Victory;
				Layer.loop = false;
				Layer.enabled = false;
				Layer.enabled = true;
			}

			WinTimer+= Time.deltaTime;
		
			if(WinTimer > GetComponent<HeartBeat>().ContantHeartbeatDuration)
			{
				WInScreen.SetActive(true);
			}
		
		}


		if(Dead)
		{
			if(!DieOnce)
			{
				Instantiate(Deathparts,Player.transform.position,gameObject.transform.rotation);

				Instantiate(GetComponent<HeartBeat>().OofCoin,Player.transform.position,gameObject.transform.rotation);
				Instantiate(GetComponent<HeartBeat>().OofCoin,Player.transform.position,gameObject.transform.rotation);
				Instantiate(GetComponent<HeartBeat>().OofCoin,Player.transform.position,gameObject.transform.rotation);
				Instantiate(GetComponent<HeartBeat>().OofCoin,Player.transform.position,gameObject.transform.rotation);
				Instantiate(GetComponent<HeartBeat>().OofCoin,Player.transform.position,gameObject.transform.rotation);
				Instantiate(GetComponent<HeartBeat>().OofCoin,Player.transform.position,gameObject.transform.rotation);
				Instantiate(GetComponent<HeartBeat>().OofCoin,Player.transform.position,gameObject.transform.rotation);
				Instantiate(GetComponent<HeartBeat>().OofCoin,Player.transform.position,gameObject.transform.rotation);
				Instantiate(GetComponent<HeartBeat>().OofCoin,Player.transform.position,gameObject.transform.rotation);
				Instantiate(GetComponent<HeartBeat>().OofCoin,Player.transform.position,gameObject.transform.rotation);



				Destroy(Player);
				DieOnce = true;



			}

			DeadTimer += Time.deltaTime;
			if(DeadTimer > GetComponent<HeartBeat>().ContantHeartbeatDuration)
			{
				DeadScreen.SetActive(true);
			}
		}


		if(UpdateLights)
		{
			DarkPlane.GetComponent<DarknessShader>().UpdateArrays();
			UpdateLights = false;
		}

		int i = 0;
		while( i < Items.Length)
		{
			if(Items[i].DHMOText != null)
			{
				Items[i].DHMOText.text = "x" + Items[i].Count;
			}
			i++;
		}



	}


	public void AddLightSource(GameObject NewLight)
	{
		GameObject[] Temp = new GameObject[LightSources.Length+1];
		int i = 0;
		while(i < LightSources.Length)
		{
			Temp[i] = LightSources[i];
			i++;
		}
		Temp[i] = NewLight;

		LightSources = Temp;
		UpdateLights = true;
	}
	public void ClearLightSources()
	{
		LightSources = new GameObject[0];
		UpdateLights = true;
	}


	public void AddItem(ItemStat ItemToAdd)
	{
		int theItem = HasItem(ItemToAdd);
		if(theItem != -1)
		{
			Items[theItem].Count++;
		}
		else
		{
			ItemStat[] Temp = new ItemStat[Items.Length+1];
			int i = 0;
			while(i < Items.Length)
			{
				Temp[i] = new ItemStat();

				Temp[i].Name = Items[i].Name;
				Temp[i].Count = Items[i].Count;
				Temp[i].Desc = Items[i].Desc;
				Temp[i].Price = Items[i].Price;
				Temp[i].Icon = Items[i].Icon;
				Temp[i].DHMOText = Items[i].DHMOText;
				Temp[i].AssociatedGO = Items[i].AssociatedGO;

				i++;
			}

			Temp[i] = new ItemStat();

			Temp[i].Name = ItemToAdd.Name;
			Temp[i].Count = 1;
			Temp[i].Desc = ItemToAdd.Desc;
			Temp[i].Price = ItemToAdd.Price;
			Temp[i].Icon = ItemToAdd.Icon;
			Temp[i].AssociatedGO = GameObject.Find("PrefabBacks").transform.Find(ItemToAdd.Name).gameObject;
			Items = Temp;

			UIPlace.AddItemToScreen(ItemToAdd);
		}
	}

	public void RemoveItem(ItemStat ItemToRemove)
	{
		int theItem = HasItem(ItemToRemove);

		ItemStat[] Temp = new ItemStat[Items.Length-1];
		int i = 0;
		int j = 0;

		while(j < Temp.Length)
		{
			if(i == theItem)
			{
				i++;
			}
			if(i > theItem && i < Items.Length)
			{
				Items[i].DHMOText.transform.parent.position= new Vector3(Items[i].DHMOText.transform.parent.position.x,Items[i].DHMOText.transform.parent.position.y+1,Items[i].DHMOText.transform.parent.position.z);
			}
			Temp[j] = new ItemStat();
			Temp[j].Name = Items[i].Name;
			Temp[j].Count = Items[i].Count;
			Temp[j].Desc = Items[i].Desc;
			Temp[j].Price = Items[i].Price;
			Temp[j].Icon = Items[i].Icon;
			Temp[j].DHMOText = Items[i].DHMOText;
			Temp[j].AssociatedGO = Items[i].AssociatedGO;

			j++;
			i++;


		}

		Items = Temp;

	}

	public int HasItem(ItemStat ItemToCheck)
	{
		int i = 0;
		bool HadIt = false;

		while (i < Items.Length)
		{
			if(ItemToCheck.Name == Items[i].Name)
			{
				HadIt = true;
				break;
			}
			i++;
		}

		if(!HadIt)
		{
			i = -1;
		}

		return i;
	}


	public void SpawnShop1Ladderite()
	{
		if(!TUTCollectedRocks)
		{
			TUTCollectedRocks = true;
			Instantiate(ShopLadderite,new Vector3(-40.5f,-7.8f,-1),gameObject.transform.rotation,gameObject.transform);
		}
	}
		
	public void SpawnLantern()
	{
		if(!UnlockLantern)
		{
			UnlockLantern = true;
			Instantiate(ShopLanterns,new Vector3(-9.9f,2,-1),gameObject.transform.rotation,gameObject.transform);
		}
	}
}
