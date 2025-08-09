using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dataholder : MonoBehaviour {

	public TownLore _TownLore;
	public TownName _TownName;

	public float AudioVolume;

	public MoveCharacter Char;

	public CamControl Cam;

	public AudioSource MainMusic;

	public bool StartDragonFight;

	public GameObject MainMap;
	public GameObject TownMap;
	public GameObject DragonMapPrefab;
	public GameObject DragonMap;

	public LoadDragonRoom LoadTheDragon;
	public bool CantEtnerDragonRoomNow;

	public GameObject PauseWindow;
	public bool Paused;
	public bool MenuOnScreen;

	public GameObject TitleScreen;
	public GameObject TitlePrefab;

	public bool InGame;

	public GameObject DeadScreen;
	public GameObject DeadScreen2;
	public GameObject WinScreen;

	public bool Win;
	public bool Dead;

	public float WinTimer;

	public float SpeedrunTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(InGame)
		{
			if(Input.GetKeyDown(KeyCode.Escape) && !MenuOnScreen && !Win)
			{
				Paused = !Paused;
				PauseWindow.SetActive(Paused);

			}

		}

		if(InGame && !Win)
		{
			SpeedrunTime+= Time.deltaTime;
		}

		if(Win)
		{
			WinTimer += Time.deltaTime;

			if(WinTimer > 3)
			{
				WinScreen.SetActive(true);
				Paused = true;
			}

		}


	}
}
