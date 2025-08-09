using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour {


	public bool Dead;
	public bool LockCamera;

	public GameObject ItemStar;
	public LevelTransitiounUISwooshing SwooshControl;

	public bool FireBG;

	public bool HasKey;

	public GameObject[] Pickups;
	public int HeldPickup;

	public Sprite ItemPickupGoalLevel;

	public int Level;
	public GameObject[] Bgs;

	public bool UnlockedSword;


	public bool StartedGame;
	public bool Victory;
	public int Deaths;
	public float Speedruntime;
	public float Speedruntime_M;

	public bool DragonBeat;

	public GameObject WinScreen;

	public GameObject[] Levels;


	public GameObject LevelMap;
	public GameObject InGame;

	public GameObject CurrentLoadedLevelObject;

	public string[] LevelName;
	public TextMesh LevelNameObject;

	public Map MapScript;

	public bool OnMap;

	public GameObject TitleScreen;

	public GameObject Lore;


	public float Volume;

	public bool DimMusic;

	public AudioSource Aud;
	public GameObject TitleAud;

	public AudioClip[] LevelMusic;
	public GameObject[] LevelIntro;

	public AudioClip Map;

	public AudioClip Clear;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(DimMusic)
		{
			Volume-= Time.deltaTime;
			if(Volume <= 0)
			{
				Volume = 0;
			}
		}
		else
		{
			Volume+= Time.deltaTime;
			if(Volume >= 1)
			{
				Volume = 1;
			}
		}



		int i = 0;
		while(i < Bgs.Length)
		{
			Bgs[i].SetActive(i == Level);
			i++;
		}

		if(StartedGame && !Victory)
		{
			Speedruntime += Time.deltaTime;
			if(Speedruntime >60)
			{
				Speedruntime_M+= 1;
				Speedruntime -= 60;
			}


		}

	}


	public void ReadyLevel()
	{
		SwooshControl.transform.localPosition = new Vector3(0,0,0);
		SwooshControl.Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
		SwooshControl.Player.GetComponent<Rigidbody>().isKinematic = false;
		SwooshControl.Player.GetComponent<PlayerController>().CutsceneNoMove = false;

		SwooshControl.Player.transform.position = new Vector3(-20,0,0);
		InGame.SetActive(true);
		LevelMap.SetActive(false);
		CurrentLoadedLevelObject = Instantiate(Levels[Level],new Vector3(0,0,0),gameObject.transform.rotation,InGame.transform);
		SwooshControl.PreLevelEnter = true;
		SwooshControl.Step[0] = true;
		LevelNameObject.color = new Vector4(1,1,1,1);
		LevelNameObject.text = LevelName[Level];
		OnMap = false;
	}

	public void ReloadLevel()
	{
		Deaths++;
		SwooshControl.Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
		SwooshControl.Player.GetComponent<Rigidbody>().isKinematic = false;
		SwooshControl.Player.GetComponent<PlayerController>().CutsceneNoMove = false;
		Dead = false;


		SwooshControl.Player.transform.position = new Vector3(0,0,0);
		Destroy(CurrentLoadedLevelObject);
		CurrentLoadedLevelObject = Instantiate(Levels[Level],new Vector3(0,0,0),gameObject.transform.rotation,InGame.transform);
		OnMap = false;
	}
	public void ExitLevel()
	{
		Level++;
		SwooshControl.Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
		SwooshControl.Player.GetComponent<Rigidbody>().isKinematic = true;
		SwooshControl.Player.transform.position = new Vector3(-20,0,0);
		GameObject.Find("Main Camera").transform.parent.position = new Vector3(0,0,-20);
		InGame.SetActive(false);
		LevelMap.SetActive(true);
		if(CurrentLoadedLevelObject != null)
		{
			Destroy(CurrentLoadedLevelObject);
		}
		OnMap = true;

		if(Level == 1)
		{
			MapScript.Fire1.SetActive(true);
		}
		if(Level == 2)
		{
			MapScript.Fire2.SetActive(true);
			MapScript.GetComponent<SpriteRenderer>().sprite = MapScript.Rubble;
		}
		ItemStar.SetActive(false);
	}

}
