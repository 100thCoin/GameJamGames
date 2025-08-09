using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour {

	public int Score;
	public int NewestScore;
	public float FreezeTimer;
	public bool FreezeUntilFalse;
	public TextMesh ScoreText;
	public Building Build;
	public MoveCharacter P;
	public AudioSource Music;
	public  bool DestroyAllSprites;
	public int ItemsCollected;

	public int FireballsPresent;

	public Transform HazardMap;
	public bool PlayerHasPlacedSomething;
	public bool AllowThreeFires;

	public AudioClip Victory;

	public float DeadTimer;
	public float DeathAnimationtimer;
	public GameObject DeadMusic;
	public GameObject DeadMusicObject;
	public GameObject DeathPenalty;
	public bool DieOnce;

	public float Volume = 0.8f;

	public bool YouWin;
	public bool WinOnce;
	public GameObject VictotyHeart;

	public float WinTimer;
	public bool StronkyFallDownGoBoom;
	public float SFDGBTimer;

	public SpriteRenderer StronkyHimself;
	public GameObject StronkyExplosion;

	public bool DestroyAllItems;

	public GameObject Inventory;
	public GameObject WinScreen;
	public float SpeedrunTimer;
	public TextMesh TimerShow;
	public bool Explode;
	public float ExpldodeTimer;

	// Use this for initialization
	void Start () {
		
	}

	void Update()
	{
		SpeedrunTimer += Time.deltaTime;
		if(YouWin)
		{
			if(!WinOnce)
			{
				Inventory.SetActive(false);
				int minutes = 0;
				float speedtimeCopy = SpeedrunTimer;

				while(speedtimeCopy > 60)
				{
					minutes++;
					speedtimeCopy -= 60;
				}


				float seconds = SpeedrunTimer % 60;
				seconds = seconds*100;
				seconds = Mathf.Round(seconds) / 100f;
				string SpeedString = "" +minutes;
				string secondString = ""+seconds;
				if(seconds < 10)
				{
					secondString = "0" + secondString;
				}
				SpeedString = SpeedString + ":" + secondString;
				TimerShow.text += " " + SpeedString;


				P.Anim.runtimeAnimatorController = P.Idle;
				WinOnce = true;
				Music.clip = Victory;
				Music.enabled = false;
				Music.enabled = true;
				Music.loop = false;
				WinTimer = 2.75f;
				VictotyHeart.SetActive(true);
				DestroyAllSprites = true;
				DestroyAllItems = true;
			}

			WinTimer -= Time.deltaTime;

			if(WinTimer < 0)
			{
				StronkyFallDownGoBoom = true;
			}

		}

		if(StronkyFallDownGoBoom && StronkyHimself != null)
		{
			StronkyHimself.flipY = true;
			SFDGBTimer += Time.deltaTime;
			StronkyHimself.transform.position = new Vector3(-8.5f,4.5f - SFDGBTimer*(10/2.75f),0);
			if(SFDGBTimer > 2.75f)
			{
				Instantiate(StronkyExplosion,StronkyHimself.transform.position,StronkyHimself.transform.rotation);
					
				Destroy(StronkyHimself.gameObject);
				Explode = true;
				StronkyFallDownGoBoom = false;
			}



		}

		if(Explode)
		{
			ExpldodeTimer += Time.deltaTime;
			if(ExpldodeTimer > 1.875f)
			{
				WinScreen.SetActive(true);
			}
		}


		ScoreText.text = "" + Score;
		while(ScoreText.text.Length < 6)
		{
			ScoreText.text = "0" + ScoreText.text;
		}

		if(P.Dead)
		{
			P.Hammering = false;
			P.HammerTimer = 0;
			DeadTimer+= Time.deltaTime;
			Music.pitch = 0;
			if(DeadTimer > 1 && !DieOnce)
			{
				DieOnce = true;
				DestroyAllSprites = true;
				DeathAnimationtimer = 2;
				DeadMusicObject = Instantiate(DeadMusic,transform.position,transform.rotation,transform);
				DeathPenalty.SetActive(true);
				Score -= 1000;
				P.Anim.runtimeAnimatorController = P.DeadAnim;
				if(Score < 0)
				{
					Score = 0;
				}
			}

			if(DeathAnimationtimer > 0)
			{
				DeathAnimationtimer -= Time.deltaTime;
				if(DeathAnimationtimer <= 0)
				{
					Destroy(DeadMusicObject);
					P.Dead = false;
					FreezeUntilFalse = false;
					Music.pitch = 1;
					DeathPenalty.SetActive(false);
					DestroyAllSprites = false;
					DieOnce = false;
					DeadTimer = 0;
					FireballsPresent = 0;
				}
			}
		}

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(FreezeUntilFalse)
		{
			FreezeTimer = 0.01f;
		}
		else
		{
			if(FreezeTimer >0)
			{
				FreezeTimer -= Time.fixedDeltaTime;
			}
		}

	}
}
