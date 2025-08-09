using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Line{

	public int Talking;
	public string Quote;
	public Sprite Emotion;
	public int Action;	//cutscenes. every action has a unite ID so I can make this game in 48 hours lol
	public float Mod;
	public float Progress;
}




public class WriteLine : MonoBehaviour {

	public SpriteRenderer Deathbloom;

	public AudioSource TildeAmbiance;
	public AudioSource MiggsAmbiance;
	public AudioSource Ambiance;
	public AudioClip TildeFightMusic;

	public float Tilfevolume;
	public float MiggsVolume;

	public Animator Miggy;
	public RuntimeAnimatorController MiggyDead;


	public bool DEBUGNEXT;

	public bool DEBUGOPENING;

	public bool DEBUGBADIDEA;

	public bool DEBUGPLANTONE;


	public DataHolder Main;
	public DialogueHolder DHold;

							// int Talking
	public Font Monologue;	//0
	public Font Tilde;		//1
	public Font TildeAngry;	//2
	public Font Miggs;		//3

	public GameObject MonologueHost;
	public GameObject MiggsHost;
	public GameObject TildeHost;

	public TextMesh Mono;
	public TextMesh Tild;
	public TextMesh Migg;

	public Sprite CalmTildeBox;
	public Sprite AngryTildeBox;

	public Line[] CurrentCutscene;
	public int CurrentLine;

	public bool InDialogue;

	public SpriteRenderer MiggsSR;
	public SpriteRenderer TildeSR;
	public SpriteRenderer CurrentSR;

	public Font CurrentFont;
	public Color TextColor;

	public bool ReadingLine;
	public float Timer;

	public float Timing;
	public float Delay;

	public string Reading;

	public TextMesh CurrentTM;

	public GameObject CheckMono;
	public GameObject CheckTilde;
	public GameObject CheckMiggs;
	public GameObject CurrentCheck;

	public float currentProgress;

	public float CutsceneTimer;
	public bool CutsceneBool;
	public bool CutsceneBool2;
	public bool CutsceneBool3;
	public bool CutsceneBool4;
	public bool CutsceneBool5;

	public int Action;


	public GameObject Poof;
	public SpriteRenderer LifeBulb;
	public Sprite LBOpen;
	public BornGreen Borngr;
	public Animator P;
	public GameObject Player;
	public RuntimeAnimatorController Idle;
	public RuntimeAnimatorController Appear;

	public NPC TildeNPC;
	public NPC MiggsNPC;

	public float CurrentMod;

	public Sprite TildeWow;

	public SpriteRenderer NullSR;

	public MoveFarmInPlace MFIP;

	public int Speakerr;

	public RuntimeAnimatorController DemonTildeNPC;
	public RuntimeAnimatorController TIldeIdleNPC;
	public RuntimeAnimatorController GrumpyIdleTilde;


	public SpriteRenderer MiggsTable;
	public Sprite MiggsMacaroni;
	public Sprite MiggsMacaroni2;
	public Sprite MiggsMacaroni3;

	public Farm F;

	public EnterCombat PseudoCombTildeMiggs;
	public EnterCombat TildeFight;

	public RuntimeAnimatorController SwordShing;
	public Animator TildeWSword;

	public bool DEBUGMAC;


	public Material Decay;

	public GameObject world;

	public Sprite TildeSurp;


	public bool POSTPONE;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(!InDialogue)
		{
			Speakerr = 0;
		}


		if(Speakerr == 1 || Speakerr == 2)
		{
			Tilfevolume += Time.deltaTime * 0.3f;
		}
		else
		{
			Tilfevolume -= Time.deltaTime * 0.3f;

		}

		if(Tilfevolume >1)
		{
			Tilfevolume = 1;
		}
		if(Tilfevolume <0)
		{
			Tilfevolume = 0;
		}


		if(Speakerr == 3)
		{
			MiggsVolume += Time.deltaTime * 0.3f;
		}
		else
		{
			MiggsVolume -= Time.deltaTime * 0.3f;

		}

		MiggsAmbiance.volume = MiggsVolume;
		TildeAmbiance.volume = Tilfevolume;



		if(MiggsVolume >1)
		{
			MiggsVolume = 1;
		}
		if(MiggsVolume <0)
		{
			MiggsVolume = 0;
		}


		if(Input.GetKeyDown(KeyCode.Mouse0))
		{

			DEBUGNEXT = true;

		}




		if(Action == 1)
		{
			Decay.SetFloat("_wither",3);

			if(!CutsceneBool)
			{
				CutsceneTimer += Time.deltaTime;
				Main.Cam.transform.position += new Vector3(Time.deltaTime*2,0,0);

				Main.Cam.FadeBlack.color = new Vector4(0,0,0,1-CutsceneTimer*0.5f);

				if(CutsceneTimer > 2.5f)
				{
					CutsceneBool = true;
					CutsceneTimer = 0;
				}
				POSTPONE = true;
			}
			else if(!CutsceneBool2)
			{
				float XDuration = 1;


				if(CutsceneTimer < XDuration)
				{
					CutsceneTimer += Time.deltaTime;
					float LastXpos = 30.5f;
					float TargetXpos = 32;
					if(CutsceneTimer >= XDuration)
					{
						CutsceneTimer = XDuration;
					}
					float XPos = (((LastXpos - TargetXpos) * Mathf.Pow(CutsceneTimer,2))/Mathf.Pow(XDuration,2) - ((2*LastXpos - 2*TargetXpos) * CutsceneTimer)/XDuration + LastXpos);
					Main.Cam.transform.position = new Vector3(XPos,1.5f,-60);

				}

				if(CutsceneTimer >= XDuration && !POSTPONE)
				{
					CutsceneTimer = 0;
					CutsceneBool2 = true;

				}
			}
			else if(!CutsceneBool3)
			{
				CutsceneTimer += Time.deltaTime;

				if(CutsceneTimer > 1)
				{
					CutsceneTimer = 0;
					CutsceneBool3 = true;

				}
			}
			else if(!CutsceneBool4)
			{
				CutsceneBool4 = true;
				Instantiate(Poof,LifeBulb.transform.position + new Vector3(0,0,0.1f),transform.rotation,LifeBulb.transform);
				P.runtimeAnimatorController = Appear;
				Player.transform.position = LifeBulb.transform.position + new Vector3(0,0,-0.2f);
				LifeBulb.sprite = LBOpen;
				Borngr.DoIt = true;
			}
			else if(!CutsceneBool5)
			{
				CutsceneTimer += Time.deltaTime;

				if(CutsceneTimer > 0.5f)
				{
					CutsceneTimer = 0;
					CutsceneBool5 = true;
					Player.transform.position = new Vector3(Player.transform.position.x,0,Player.transform.position.z);
					P.runtimeAnimatorController = Idle;
					Action = 0;
					DEBUGNEXT = true;
				}



			}



		}
		////////////////////////////////////////////
		else if(Action == 2)
		{
			TildeNPC.WalkDest = CurrentMod;

		}
		else if(Action == 3)
		{
			MiggsNPC.WalkDest = CurrentMod;

		}
		else if(Action == 4)
		{
			MFIP.DoIt = true;

		}

		else if(Action == 5)
		{
			TildeNPC.Anim.runtimeAnimatorController = DemonTildeNPC;
		}
		else if(Action == 6)
		{
			TildeNPC.Anim.runtimeAnimatorController = TIldeIdleNPC;
		}
		else if(Action == 7)
		{
			Player.GetComponent<SpriteRenderer>().flipX = true;
		}

		else if(Action == 8)
		{
			CutsceneTimer += Time.deltaTime;
			Main.Cam.FadeBlack.gameObject.SetActive(true);
			Main.Cam.FadeBlack.color = new Vector4(0,0,0,CutsceneTimer);
			if(CutsceneTimer > 1.5f)
			{
				Action = 0;
				DEBUGNEXT = true;
				CutsceneTimer = 0;

			}
		}

		else if(Action == 9)
		{
			CutsceneTimer += Time.deltaTime;
			Main.Cam.FadeBlack.color = new Vector4(0,0,0,1-CutsceneTimer);
			if(CutsceneTimer > 1.5f)
			{
				Action = 0;
				DEBUGNEXT = true;
				CutsceneTimer = 0;

			}
		}

		else if(Action == 10)
		{
			MiggsTable.sprite = MiggsMacaroni;
		}

		else if(Action == 11)
		{
			F.PlacingBulb = true;
			F.BulbID = 0;
			Instantiate(F.BulbVis[0],F.PSeedHolder.transform.position,transform.rotation,F.PSeedHolder.transform);

			TildeNPC.transform.position = new Vector3(Main.Cam.transform.position.x + 16,TildeNPC.transform.position.y,TildeNPC.transform.position.z);

			Action = 0;
		}

		else if(Action == 12)
		{
			PseudoCombTildeMiggs.gameObject.SetActive(true);
			F.PlacingBulb = false;


			F.BulbID = -1;
			F.SelectedSeed = false;
			F.ChangeSeedVisual(-1);
			Main.UnlockedRed = true;
		}

		else if(Action == 13)
		{
			TildeWSword.runtimeAnimatorController = SwordShing;
			CutsceneTimer += Time.deltaTime;
			if(CutsceneTimer > 1.5f)
			{
				Action = 0;
				DEBUGNEXT = true;
				CutsceneTimer = 0;
			}

		}

		else if(Action == 14)
		{
			world.SetActive(true);
			PseudoCombTildeMiggs.gameObject.SetActive(false);
			Player.transform.position = new Vector3(30,0,Player.transform.position.z);
			Main.Cam.transform.position = new Vector3(30,Main.Cam.transform.position.y,Main.Cam.transform.position.z);
			Miggy.runtimeAnimatorController = MiggyDead;
			CutsceneTimer += Time.deltaTime;
			Main.Cam.FadeBlack.color = new Vector4(0,0,0,1-CutsceneTimer);
			if(CutsceneTimer > 1.5f)
			{
				Action = 0;
				DEBUGNEXT = true;
				CutsceneTimer = 0;

				world.SetActive(true);
			}
		}



		else if(Action == 16)
		{

			Decay.SetFloat("_wither",3-CutsceneTimer);
			if(CutsceneTimer > 2)
			{
				TildeNPC.GetComponent<Animator>().runtimeAnimatorController = DemonTildeNPC;
				Ambiance.clip = TildeFightMusic;
				Ambiance.enabled = true;

			}
			CutsceneTimer += Time.deltaTime;
			if(CutsceneTimer > 6)
			{
				Action = 0;
				DEBUGNEXT = true;
				CutsceneTimer = 0;
				TildeFight.gameObject.SetActive(true);

			}
		}






		if(DEBUGOPENING)
		{
			CurrentCutscene = GetComponent<DialogueHolder>().OpeningTilde;
			DEBUGNEXT = true;
			DEBUGOPENING = false;
			CurrentLine = 0;

		}


		if(DEBUGBADIDEA)
		{
			CurrentCutscene = GetComponent<DialogueHolder>().MonoBadIdea;
			DEBUGNEXT = true;
			DEBUGBADIDEA = false;
			CurrentLine = 0;

		}

		if(DEBUGPLANTONE)
		{
			CurrentCutscene = GetComponent<DialogueHolder>().TildeGrewFlower;
			DEBUGNEXT = true;
			DEBUGPLANTONE = false;
			CurrentLine = 0;

		}

		if(DEBUGMAC)
		{
			CurrentCutscene = GetComponent<DialogueHolder>().MiggsMacaroni;
			DEBUGNEXT = true;
			DEBUGMAC = false;
			CurrentLine = 0;
		}


		if(DEBUGNEXT)
		{
			InDialogue = true;
			DEBUGNEXT = false;
			if(Reading.Length == 0 && Action != 1 && Action != 8 && Action != 9 && Action != 14 && Action != 13 && Action != 16)
			{
				if(CurrentLine < CurrentCutscene.Length)
				{
					Main.NoMoving = true;

					NextLine();
				}
				else
				{
					Close();
				}
			}
			else
			{
				if(Action != 1 && Action != 8 && Action != 9 && Action != 14 && Action != 13 && Action != 16)
				{

					Reading = Reading.Replace("#","\n");

					CurrentCheck.SetActive(true);
					CurrentTM.text += Reading;
					Reading = "";
				}
			}
		}

		if(ReadingLine && Speakerr != -1)
		{
			Timer += Time.deltaTime;
			if(Timer > Timing + Delay)
			{
				Timer -= (Timing + Delay);
				if(Reading.Length > 0)
				{
					string NextChar = Reading.Substring(0,1);

					if(NextChar == "#")
					{
						NextChar = "\n";
					}

					CurrentTM.text += NextChar;


				Reading = Reading.Substring(1,Reading.Length-1);
				}

				if(Reading.Length == 0)
				{
					if(Action == 15)
					{
						CurrentSR.sprite = TildeSurp;
						Ambiance.enabled = false;
						MiggsAmbiance.enabled = false;
						TildeAmbiance.enabled = false;
						TildeAmbiance.volume = 0;
						Deathbloom.enabled = true;
					}


					ReadingLine = false;

					CurrentCheck.SetActive(true);
					if(currentProgress != 0)
					{
						Main.Progress = currentProgress;
					}
						

				}


			}

		}


		if(Speakerr == -1)
		{
			DEBUGNEXT = true;
		}


	}

	void NextLine()
	{
		CutsceneBool = false;
		CutsceneBool2 = false;
		CutsceneBool3 = false;
		CutsceneBool4 = false;
		CutsceneBool5 = false;

		if(CurrentCheck != null)
		{
			CurrentCheck.SetActive(false);
		}
		if(CurrentTM != null)
		{
			CurrentTM.text = "";
		}
		Line ThisLine = CurrentCutscene[CurrentLine];
		int Speaker = ThisLine.Talking;
		if(Speaker == 0)
		{
			MiggsHost.SetActive(false);
			TildeHost.SetActive(false);
			MonologueHost.SetActive(true);
			CurrentSR = NullSR;
			CurrentFont = Monologue;
			CurrentCheck = CheckMono;
			CurrentTM = Mono;
		}
		else if(Speaker == 3)
		{
			MiggsHost.SetActive(true);
			TildeHost.SetActive(false);
			MonologueHost.SetActive(false);
			CurrentSR = MiggsSR;
			CurrentFont = Miggs;
			CurrentCheck = CheckMiggs;
			CurrentTM = Migg;
		}
		else if(Speaker != -1)
		{
			MiggsHost.SetActive(false);
			TildeHost.SetActive(true);
			MonologueHost.SetActive(false);
			CurrentSR = TildeSR;
			CurrentFont = (Speaker == 1) ? Tilde : TildeAngry;
			TildeHost.GetComponent<SpriteRenderer>().sprite = (Speaker == 1) ? CalmTildeBox : AngryTildeBox;

			CurrentCheck = CheckTilde;
			CurrentTM = Tild;
		}
		else
		{
			MiggsHost.SetActive(false);
			TildeHost.SetActive(false);
			MonologueHost.SetActive(false);
		}

		CurrentMod = ThisLine.Mod;
		currentProgress = ThisLine.Progress;
		;
		CurrentSR.sprite = ThisLine.Emotion;


		if(CurrentCheck != null)
		{
			CurrentCheck.SetActive(false);
		}
		if(CurrentTM != null)
		{
			CurrentTM.text = "";
		}

		Reading = ThisLine.Quote;
		ReadingLine = true;
		CurrentLine++;

		Action = ThisLine.Action;

		Speakerr = Speaker;

		if(Speakerr == 1 || Speakerr == 2)
		{
			Vector3 Walkin = new Vector3(TildeNPC.transform.position.x - Player.transform.position.x,0,0).normalized;

			TildeNPC.GetComponent<SpriteRenderer>().flipX = Walkin.x < 0;
		}

		if(Speakerr == 3)
		{
			Vector3 Walkin = new Vector3(MiggsNPC.transform.position.x - Player.transform.position.x,0,0).normalized;

			MiggsNPC.GetComponent<SpriteRenderer>().flipX = Walkin.x > 0;
		}


		if(Speakerr == -1)
		{
			if(currentProgress != 0)
			{
				Main.Progress = currentProgress;
			}
		}


		if(Action == 1 || Action == 8 || Action == 9 || Action == 14 || Action == 13 || Action == 16)
		{
			// game opening
			CurrentCheck.SetActive(false);
			CurrentTM.text = "";
			MiggsHost.SetActive(false);
			TildeHost.SetActive(false);
			MonologueHost.SetActive(false);

			if(Action == 1 )
			{
				Main.Cam.transform.position = new Vector3( 25.5f,1.5f,-50);
			}
		}




	}



	void Close()
	{
		CutsceneBool = false;
		CutsceneBool2 = false;
		CutsceneBool3 = false;
		CutsceneBool4 = false;
		CutsceneBool5 = false;

		Main.NoMoving = false;
		CurrentCheck.SetActive(false);
		CurrentTM.text = "";

		InDialogue = false;
		MiggsHost.SetActive(false);
		TildeHost.SetActive(false);
		MonologueHost.SetActive(false);
	}

	void StartRead(Line[] Dialogue)
	{
		CurrentCutscene = Dialogue;
		CurrentLine = 0;
		InDialogue = true;

		DEBUGNEXT = true;

	}


	public void DEBUGREAD(Line[] Cutsceneeeee)
	{
		CurrentCutscene = Cutsceneeeee;
		DEBUGNEXT = true;
		CurrentLine = 0;

	}


}
