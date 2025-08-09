using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class VariableBars{

	public UIBar PlayerXBar;
	public UIBar PlayerYBar;
	public UIBar PlayerHold;
	public UIBar PlayerDead;
	public UIBar PlayerCoins;
	public UIBar YourName;
	public UIBar DoorOpen;
	public UIBar ButtonPress;
	public UIBar CrateState;



}



public class DataHolder : MonoBehaviour {

	public float PlayerPosX;
	public float PlayerPosY;
	public float PlayerPosXTimer;
	public float PlayerPosYTimer;
	public string PlayerHeld;
	public float PlayerHeldTimer;
	public int PlayerDead;
	public float PlayerDeadTimer;

	public int DoorOpen;
	public float DoorOpenTimer;

	public string PlayerName;
	public float PlayerNameTimer;

	public int Coins;
	public float CoinTimer;

	public int ButtonPressed;
	public float ButtonTimer;

	public string CrateState;
	public float CrateStateTimer;



	public Char_Move Player;

	public bool PauseTimers;

	public VariableBars VB;





	public bool LoadNextRoom;
	public float LoadNextRoomTimer;
	public int currentlevel;
	public GameObject[] LevelList;
	public int[] LevelBonusLengths;
	public GameObject CurrentLoadedLevel;
	public GameObject TempLevel;
	public Transform MapHolder;
	public CameraMove Cam;

	public SpriteRenderer FadeWhite;
	public float FadeTimer;

	public bool closingDoor;
	 
	public bool PseudoPause;

	public Nametag NameTagObject;

	public bool[] OccupiedFillBars;
	public UIBar[] Bars;
	public float[] BarZeroTimers;

	public GameObject outOfNametags;
	public float OutOfNametagsTimer;

	public Vending Vend;

	public GameObject VendExpensive;
	public GameObject VendAllOut;
	public GameObject ReflectionText;

	public bool closingDoorNextFrame;

	public Object_Move CrateObject;

	public GameObject NametagParticles;

	public GameObject PostNametag;

	public GameObject TeleAway;
	public GameObject TeleHere;

	public GameObject SFX_Tele;
	public GameObject SFX_Key1;
	public GameObject SFX_Key2;
	public GameObject SFX_Door;
	public GameObject SFX_Vend;
	public GameObject SFX_Coin;
	public GameObject SFX_Dead;

	public GameObject MUS_PSwitch;
	public GameObject SpawnedPMusic;
	public AudioSource MainMusic;
	public float MainMusicVolume;
	public float MusicTimer;
	public bool playingPMusic;

	public string FinalPlayerName;
	public float SpeedrunTimer;

	public GameObject VictorySequence;

	public GameObject FadeWhiter;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		SpeedrunTimer+=Time.fixedDeltaTime;
		if(ButtonPressed == 1)
		{
			if(!playingPMusic)
			{
				playingPMusic = true;
				SpawnedPMusic = Instantiate(MUS_PSwitch,transform.position,transform.rotation,transform);
				MusicTimer = -8;
			}


		}

		if(MusicTimer < 1)
		{
			MusicTimer +=Time.fixedDeltaTime*3;
			if(MusicTimer > 0)
			{
				MainMusic.pitch = MusicTimer;
				MainMusic.volume = MusicTimer;
				if(MusicTimer >= 1)
				{
					MainMusic.pitch = 1;
					MainMusic.volume = 1;
				}
			}
			else
			{
				MainMusic.pitch = 0;
				MainMusic.volume = 0;
			}
		}


		OutOfNametagsTimer+= Time.deltaTime;
		if(OutOfNametagsTimer > 3)
		{
			PostNametag.SetActive(false);
			outOfNametags.SetActive(false);
			VendExpensive.SetActive(false);
			VendAllOut.SetActive(false);
			ReflectionText.SetActive(false);
		}

		if(CrateObject != null)
		{
			CrateState = CrateObject.PhysState;
		}
		PlayerPosX = Player.PosLBX + Player.PosHBX/16f;
		PlayerPosY = Player.PosLBY + Player.PosHBY/16f;
		PlayerHeld = Player.HeldObject != null && Player.Holding ? Player.HeldObject.name : "";
		PlayerDead = Player.Dead ? 1 : 0;

		if(!PauseTimers && !PseudoPause && !LoadNextRoom)
		{
			if(!Player.Dead)
			{
				//// Player X Position ////
				if(PlayerPosX != 0){
					if(VB.PlayerXBar.Index == -1)
					{
						PositionFillBar(VB.PlayerXBar);
					}
					PlayerPosXTimer+= Time.fixedDeltaTime;
					if(PlayerPosXTimer > 3){
						Player.PosLBX = 0;
						Player.PosHBX = 0;
						PlayerPosXTimer = 0;
						PlayerPosX = 0;
						Instantiate(TeleAway,Player.transform.position,transform.rotation,Player.transform.parent);
						Instantiate(TeleHere,Player.transform.position,transform.rotation,Player.transform);
						Instantiate(SFX_Tele,transform.position,transform.rotation,transform);
						Player.SR.enabled = false;
					}
				}else{
					PlayerPosXTimer = 0;
				}
				VB.PlayerXBar.TM.text = "X Position\n\n" + (Mathf.Round(PlayerPosX*100)/100f);
				VB.PlayerXBar.Timer=PlayerPosXTimer;

				//// Player Y Position ////
				if(PlayerPosY != 0){
					if(VB.PlayerYBar.Index == -1)
					{
						PositionFillBar(VB.PlayerYBar);
					}
					PlayerPosYTimer+= Time.fixedDeltaTime;
					if(PlayerPosYTimer > 3){
						Player.PosLBY = 0;
						Player.PosHBY = 0;
						PlayerPosYTimer = 0;
						PlayerPosY = 0;
						Instantiate(TeleAway,Player.transform.position,transform.rotation,Player.transform.parent);
						Instantiate(TeleHere,Player.transform.position,transform.rotation,Player.transform);
						Player.SR.enabled = false;
						Instantiate(SFX_Tele,transform.position,transform.rotation,transform);

					}
				}else{
					PlayerPosYTimer = 0;
				}
				VB.PlayerYBar.TM.text = "Y Position\n\n" + (Mathf.Round(PlayerPosY*100)/100f);
				VB.PlayerYBar.Timer=PlayerPosYTimer;

				//// Player Held Object ////
				if(PlayerHeld != ""){
					if(VB.PlayerHold.Index == -1)
					{
						PositionFillBar(VB.PlayerHold);
					}
					PlayerHeldTimer+= Time.fixedDeltaTime;
					if(PlayerHeldTimer > 3){
						Destroy(Player.HeldObject.gameObject);
						Player.Holding = false;
						Player.HeldObject = null;
						PlayerHeld = "";
						PlayerHeldTimer = 0;
					}
				}else{
					PlayerHeldTimer = 0;
				}
				VB.PlayerHold.TM.text = "Held Object\n\n" + PlayerHeld;
				VB.PlayerHold.Timer=PlayerHeldTimer;
			} //if Player !dead
			//// Player Dead ////
			if(PlayerDead == 1){
				if(VB.PlayerDead.Index == -1)
				{
					PositionFillBar(VB.PlayerDead);
				}
				PlayerDeadTimer+= Time.fixedDeltaTime;
				if(PlayerDeadTimer > 3){
					ReloadLevel();
					PlayerDead = 0;
					PlayerDeadTimer = 0;
				}
			}else{
				PlayerDeadTimer = 0;
			}
			VB.PlayerDead.TM.text = "Player Dead\n\n" + PlayerDead;
			VB.PlayerDead.Timer=PlayerDeadTimer;

			//// Player Name ////
			if(PlayerName != ""){
				if(VB.YourName.Index == -1)
				{
					PositionFillBar(VB.YourName);
				}
				PlayerNameTimer+= Time.fixedDeltaTime;
				if(PlayerNameTimer > 3){
					GameObject NT = Instantiate(NametagParticles,Player.transform.position,Player.transform.rotation);
					NT.GetComponent<NametagParticle>().Name.text = PlayerName;
					if(PlayerName != "")
					{
						FinalPlayerName = PlayerName;
					}
					PlayerName = "";
					PlayerNameTimer = 0;
					PostNametag.SetActive(true);
					OutOfNametagsTimer = 0;
				}
			}else{
				PlayerNameTimer = 0;
			}
			VB.YourName.TM.text = "Your Name\n\n" + PlayerName;
			VB.YourName.Timer=PlayerNameTimer;

			//// Door Open ////
			if(DoorOpen == 1){
				if(VB.DoorOpen.Index == -1)
				{
					PositionFillBar(VB.DoorOpen);
				}
				DoorOpenTimer+= Time.fixedDeltaTime;
				if(DoorOpenTimer > 3){
					closingDoor = true;
					DoorOpenTimer = 0;
					DoorOpen = 0;
				}
			}else{
				DoorOpenTimer = 0;
			}
			VB.DoorOpen.TM.text = "Door Opened\n\n" + DoorOpen;
			VB.DoorOpen.Timer=DoorOpenTimer;

			//// Coins ////
			if(Coins > 0){
				if(VB.PlayerCoins.Index == -1)
				{
					PositionFillBar(VB.PlayerCoins);
				}
				CoinTimer+= Time.fixedDeltaTime;
				if(CoinTimer > 3){
					CoinTimer = 0;
					Coins = 0;
				}
			}else{
				CoinTimer = 0;
			}
			VB.PlayerCoins.TM.text = "Coins\n\n" + Coins;
			VB.PlayerCoins.Timer=CoinTimer;


			//// Button ////
			if(ButtonPressed != 0){
				if(VB.ButtonPress.Index == -1)
				{
					PositionFillBar(VB.ButtonPress);
				}
				ButtonTimer+= Time.fixedDeltaTime;
				if(ButtonTimer > 3){
					ButtonTimer = 0;
					ButtonPressed = 0;
					playingPMusic = false;
				}
			}else{
				ButtonTimer = 0;
			}
			VB.ButtonPress.TM.text = "P-Switch\n\n" + ButtonPressed;
			VB.ButtonPress.Timer=ButtonTimer;

			//// Crate State ////
			if(CrateState != ""){
				if(VB.CrateState.Index == -1)
				{
					PositionFillBar(VB.CrateState);
				}
				CrateStateTimer+= Time.fixedDeltaTime;
				if(CrateStateTimer > 3){
					if(CrateObject != null)
					{
						Destroy(CrateObject.gameObject);
					}
					CrateStateTimer = 0;
					CrateState = "";
				}
			}else{
				CrateStateTimer = 0;
			}
			VB.CrateState.TM.text = "Crate State\n\n" + CrateState;
			VB.CrateState.Timer = CrateStateTimer;

		}

		if(!PseudoPause)
		{
		int i = 0;
			while(i < 8)
			{
				if(OccupiedFillBars[i])
				{
					if(Bars[i].Timer == 0)
					{
						BarZeroTimers[i] += Time.fixedDeltaTime;
					}
					else
					{
						BarZeroTimers[i] = 0;
					}
					if(BarZeroTimers[i] > 2)
					{
						OccupiedFillBars[i] = false;
						Bars[i].transform.parent.parent.localPosition = new Vector3(16,16,20);
						BarZeroTimers[i] = 0;
						Bars[i].Index = -1;
					}
				}

				i++;
			}
		}

	}

	void LateUpdate()
	{
		if(closingDoorNextFrame)
		{
			closingDoor = false;
			closingDoorNextFrame = false;
		}
		if(LoadNextRoom)
		{
			if(LoadNextRoomTimer == 0)
			{
				CurrentLoadedLevel.transform.position -= new Vector3(24 + Cam.ExtraRoomSize,0,0);
				Player.transform.position -= new Vector3(24 + Cam.ExtraRoomSize,0,0);
				Cam.transform.position -= new Vector3(24 + Cam.ExtraRoomSize,0,0);

				//instantiate new level
				currentlevel++;
				Cam.ExtraRoomSize = LevelBonusLengths[currentlevel];
				TempLevel = Instantiate(LevelList[currentlevel],new Vector3(0,0,0),transform.rotation,MapHolder);
			}
			LoadNextRoomTimer += Time.deltaTime;

			float CamPos = Cam.transform.position.x;
			float CamDest = 10;
			float PlayerPos = Player.transform.position.x;
			float PlayerDest = 0;

			float Duration = 1;

			float CamMove = (((CamPos - CamDest) * Mathf.Pow(LoadNextRoomTimer,2))/Mathf.Pow(Duration,2) - ((2*CamPos - 2*CamDest) * LoadNextRoomTimer)/Duration + CamPos);
			float PlayerMove = (((PlayerPos - PlayerDest) * Mathf.Pow(LoadNextRoomTimer,2))/Mathf.Pow(Duration,2) - ((2*PlayerPos - 2*PlayerDest) * LoadNextRoomTimer)/Duration + PlayerPos);

			Player.transform.position = new Vector3(PlayerMove,Player.transform.position.y,Player.transform.position.z);
			Cam.transform.position = new Vector3(CamMove,Cam.transform.position.y,Cam.transform.position.z);

			if(LoadNextRoomTimer > 0.25f)
			{
				Player.PosHBX = 0;
				Player.PosLBX = 0;
				LoadNextRoom = false;
				Destroy(CurrentLoadedLevel.gameObject);
				CurrentLoadedLevel = TempLevel;
				LoadNextRoomTimer = 0;
			}

		}
		else //can press r to reload
		{
			if(!PseudoPause)
			{
				if(Input.GetKeyDown(KeyCode.R))
				{
					ReloadLevel();

				}
			}
		}
		if(FadeTimer > 0)
		{
			FadeWhite.color = new Vector4(1,1,1,FadeTimer);
			FadeTimer-=Time.deltaTime*4;
			if(FadeTimer <= 0)
			{
				FadeWhite.color = new Vector4(1,1,1,0);
			}
		}
	}


	public void ReloadLevel()
	{
		Destroy(CurrentLoadedLevel.gameObject);
		Player.SR.enabled = true;
		Player.FaceSR.enabled = true;
		Player.Holding = false;
		Player.Dead = false;
		Player.DeathTimer = 0;
		Player.PosHBX = 0;
		Player.PosLBX = 0;
		Player.PosHBY = 0;
		Player.PosLBY = 0;
		Player.SpeedX = 0;
		Player.SpeedY = 0;
		ButtonPressed = 0;
		MusicTimer = 0.99f;

		if(playingPMusic)
		{
			playingPMusic = false;
			Destroy(SpawnedPMusic);
			MainMusicVolume = 1;
		}

		if(PlayerName != "")
		{
			FinalPlayerName = PlayerName;
		}
		PlayerName = "";
		Coins = 0;
		Player.transform.position = new Vector3(0,0,Player.transform.position.z);
		Cam.transform.position = new Vector3(10,Cam.transform.position.y,Cam.transform.position.z);
		FadeTimer = 1;
		FadeWhite.color = new Vector4(1,1,1,1);
		CurrentLoadedLevel = Instantiate(LevelList[currentlevel],new Vector3(0,0,0),transform.rotation,MapHolder);
		Cam.ExtraRoomSize = LevelBonusLengths[currentlevel];

	}

	void PositionFillBar(UIBar Bar)
	{
		int i = 0;
		while(i < 8)
		{
			if(!OccupiedFillBars[i])
			{
				break;
			}
			i++;
		}
		OccupiedFillBars[i] = true;

		Bar.transform.parent.parent.localPosition = new Vector3(16,6-i*2,20);
		Bars[i] = Bar;
		Bar.Index = i;
	}


	public void LoadVictorySequence()
	{

		GameObject VictoryS = Instantiate(VictorySequence,transform.position,transform.rotation);

		VictoryText VT = VictoryS.GetComponent<JankReference>().VT; //it's a gam jam, please don't critique my code.

		VT.PlayerName = FinalPlayerName;
		VT.FinalTime = SpeedrunTimer;

		Destroy(gameObject); //rest in piece, DataHolder. You will not die in vain.

	}



}
