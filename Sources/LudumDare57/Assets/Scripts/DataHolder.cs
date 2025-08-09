using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global{
	public static DataHolder Dataholder;
}
public class G{
	public static DataHolder Main;
}


public class DataHolder : MonoBehaviour {

	public GameObject Purchase_SFX;

	public List<GameObject> VoiceQueue;
	public VoiceLine CurrentVoiceLine;

	public TextMesh HudText;
	public HUD_Portrait HUDPortrait;
	public PlayerMovement PMov;
	public GravableTile GravTileA;
	public GravableTile GravTileB;
	public bool RemoveTileANext = true;
	public Transform MiddleOfTheMap;

	public GameObject GravBallPop;
	public float GravForce = 16f;
	public Camera MainCamera;
	public CameraController CamMov;

	public bool GameEnded; // the cave cutscene just happened;
	public GameObject EndingScreenFlash;
	public Shockwave ShockwaveObject;
	public GameObject Necromooncer1ParentObject;

	public Gem[] Gems;

	public Vector3 Minus500 = new Vector3(0,0,-500);

	public bool Upgrade_SpeedBoost;
	public bool Upgrade_JumpBoost;
	public bool Upgrade_StardustRemover;
	public bool Upgrade_Speed2;
	public bool Upgrade_Mustache;
	public GameObject PlayerMustache;
	public SpriteRenderer BlueWorkshop;
	public Sprite BlueMushtache;
	public bool WorkshopOpen;
	public GameObject PauseMenu;
	public bool Paused;

	public float ReconstructedPercent;

	public ShopButton SelectedShopButton;

	public GravableTile[] CompleteListOfTiles;

	public HudButton TheHudButton;

	public SpriteRenderer RollingTransition;

	public bool NecromooncerDefeated;

	public bool EndedPseudogame;
	public SpriteRenderer PseudogameFadeToBlack;

	public GameObject Game;
	public GameObject Pseudogame;
	public GameObject EndingCutscene;
	public bool InGame;

	public GameObject LeadIntoBooWomp;
	public GameObject SkeletonBreaker;
	public bool SpawnSkeletonBreaker;
	public float SkeletonBreakerTimer;

	public bool InitiateTutorial;
	public bool TutorialModeNoMoving;
	public bool TutorialModeNoClimbing;
	public GameObject TUTVoice1;
	public GameObject ShootHereIndicator;
	public bool InFinaleCave;
	public float FinaleCaveTimer;

	public GameObject MusicToMute;
	public float IntroWhoopTimer;
	public SpriteRenderer Rolling2;

	public Volume MainMusic;
	public Volume AcapellaTrack;
	public float AcapellaTimer;

	void Start () {
		print (" YOU HAVE NOT CREATED Purchase_SFX");

	}

	void LateUpdate()
	{

	}

	// Update is called once per frame
	void Update () 
	{

		if (WorkshopOpen) {
			AcapellaTimer += Time.deltaTime;
		} else {
			AcapellaTimer -= Time.deltaTime;
		}
		AcapellaTimer = Mathf.Clamp01 (AcapellaTimer);
		if (MainMusic != null) {
			MainMusic.ForceMultt = 1 - AcapellaTimer;
		}
		AcapellaTrack.ForceMultt = AcapellaTimer;



		if (InFinaleCave && !EndedPseudogame) {
			FinaleCaveTimer += Time.deltaTime;
			Super.Dataholder.MusicMultiplier = 1 - FinaleCaveTimer;

		} else if (InFinaleCave && EndedPseudogame) {
			Super.Dataholder.MusicMultiplier = 0.8f;
		}


		if (InitiateTutorial) {
			InGame = true;
			InitiateTutorial = false;
			VoiceQueue.Add (TUTVoice1);
			MainCamera.transform.position = new Vector3 (-31, 400,-100);
			PMov.transform.position = new Vector3 (-31, 400,0);
			CamMov.TargetPos = new Vector2 (-31, 400);
			TutorialModeNoMoving = true;
			TutorialModeNoClimbing = true;
			PMov.PreventMovementForCutscenePurposes_AndShooting = true;
			PMov.FireDelay = 14;
			IntroWhoopTimer = 0.5f;
		}

		if (InGame) {
			IntroWhoopTimer += Time.deltaTime;
			Rolling2.color = new Vector4 (0, 0, 0, IntroWhoopTimer);
		}

		if (SpawnSkeletonBreaker) {
			SkeletonBreakerTimer += Time.deltaTime;
			if (SkeletonBreakerTimer > 1) {
				SpawnSkeletonBreaker = false;
				SkeletonBreakerTimer = 1;
			}
			SkeletonBreaker.transform.localScale = Vector3.one * SinLerp(0,1,SkeletonBreakerTimer,1);
		}


		if (CurrentVoiceLine == null || CurrentVoiceLine.Done) {
			if (VoiceQueue.Count > 0) {
				if (CurrentVoiceLine != null) {
					Destroy (CurrentVoiceLine.gameObject);
				}
				CurrentVoiceLine =	Instantiate (VoiceQueue [0]).GetComponent<VoiceLine>();
				CurrentVoiceLine.transform.parent = transform;
				VoiceQueue.RemoveAt (0);
			}
		}

		if (CurrentVoiceLine == null || CurrentVoiceLine.Done) {
			Super.Dataholder.MusicMultiplier = 1;

		} else {
			Super.Dataholder.MusicMultiplier = 0.45f;
		}


		if (InGame && !EndedPseudogame) {

			SpeedrunTime += Time.deltaTime;

		}

		if (Input.GetKeyDown (KeyCode.Escape)) {

			if (WorkshopOpen) {
				//close workshop
				WorkshopOpen = false;
				BlueWorkshop.gameObject.SetActive (false);
			} else {
				if (Paused) {
					PauseMenu.SetActive (false);
					Paused = false;
				} else {
					if (InGame) {
						Paused = true;
						PauseMenu.SetActive (true);
					}
				}
			}



		}


		if (Upgrade_SpeedBoost) {
			PMov.TopSpeed = 18;
			PMov.Accelleration = 40;
		}
		if (Upgrade_Speed2) { // nobody is going to check what happens if you get this one first
			PMov.TopSpeed = 28;
			PMov.Accelleration = 64;
		}
		if (Upgrade_JumpBoost) {
			PMov.JumpHeight = 18;
		}
		if (Upgrade_Mustache) {
			PlayerMustache.SetActive (true);
			BlueWorkshop.sprite = BlueMushtache;
		}
	}

	public float ComputeReconstructionPercentage()
	{
		int total = 0;
		for (int i = 0; i < CompleteListOfTiles.Length; i++) {

			if (CompleteListOfTiles [i].Set) {
				total++;
			}
		}
		float percent = (total + 0f) / (CompleteListOfTiles.Length-1);
		if (percent > 1) {
			percent = 1;
		}
		ReconstructedPercent = percent;

		return percent;


	}

	public int CountGems()
	{
		int count = 0;
		for (int i = 0; i < Gems.Length; i++) {
			if (Gems [i].Collected) {
				count++;
			}
		}
		return count;
	}

	void FixedUpdate()
	{
		if (GravTileA != null && GravTileB != null) {
			if (GravTileA.Active && GravTileB.Active) {

				if (GravTileA.GameEnder || GravTileB.GameEnder) {
					if (LeadIntoBooWomp != null) {
						LeadIntoBooWomp.SetActive (true);
					}
				}



				if (!GravTileA.Static) {
					if (!GravTileA.NeedsGem) {						
						if (GravTileA.RB == null) {
							GravTileA.RB = GravTileA.gameObject.AddComponent<Rigidbody2D> ();
							GravTileA.RB.constraints = RigidbodyConstraints2D.FreezeRotation;
							GravTileA.RB.gravityScale = 0;
						}
						GravTileA.gameObject.layer = 13;
					
						Vector3 Diff = (GravTileB.transform.position + GravTileB.LocalGravProjCenter) - (GravTileA.transform.position + GravTileA.LocalGravProjCenter);
						GravTileA.RB.bodyType = RigidbodyType2D.Dynamic;
						GravTileA.RB.mass = GravTileA.Mass;
						for (int i = 0; i < GravTileA.transform.childCount; i++) {
							GravableTile GT = GravTileA.transform.GetChild (i).GetComponent<GravableTile> ();
							if (GT != null) {
								GravTileA.RB.mass += GT.Mass;
							}
						}
						GravTileA.RB.AddForce (Diff.normalized * GravForce * 100);
					}
				}
				if (!GravTileB.Static) {
					if (!GravTileB.NeedsGem) {
						if (GravTileB.RB == null) {
							GravTileB.RB = GravTileB.gameObject.AddComponent<Rigidbody2D> ();
							GravTileB.RB.constraints = RigidbodyConstraints2D.FreezeRotation;
							GravTileB.RB.gravityScale = 0;
						}
						GravTileB.gameObject.layer = 13;
					
						Vector3 Diff = (GravTileA.transform.position + GravTileA.LocalGravProjCenter) - (GravTileB.transform.position + GravTileB.LocalGravProjCenter);
						GravTileB.RB.bodyType = RigidbodyType2D.Dynamic;
						GravTileB.RB.mass = GravTileB.Mass;
						for (int i = 0; i < GravTileB.transform.childCount; i++) {
							GravableTile GT = GravTileB.transform.GetChild (i).GetComponent<GravableTile> ();
							if (GT != null) {
								GravTileB.RB.mass += GT.Mass;
							}
						}
						GravTileB.RB.AddForce (Diff.normalized * GravForce * 100);
					}
				}
			}
		}

	}

	public void FreezeGravTiles()
	{
		if (GravTileA.RB != null || GravTileB.RB != null) {
			if (PMov.PCol_Feet.cols.Count > 0) {
				for (int i = 0; i < PMov.PCol_Feet.cols.Count; i++) {
					Rigidbody2D RB2D = PMov.PCol_Feet.cols [i].GetComponent<Rigidbody2D> ();
					if (RB2D == null && PMov.PCol_Feet.cols [i].transform.parent != null) {
						RB2D = PMov.PCol_Feet.cols [i].transform.parent.GetComponent<Rigidbody2D> ();
					}
					if (RB2D != null) {
						// somehow inherit the speed of this thing?
						if (RB2D.gameObject == GravTileA.gameObject) {
							if (GravTileA.RB != null) {
								PMov.RB.AddForce (GravTileA.RB.velocity * 2);
							}}
						if (RB2D.gameObject == GravTileB.gameObject) {
							if (GravTileB.RB != null) {								
								PMov.RB.AddForce (GravTileB.RB.velocity * 2);
							}
						}
					}
				}
			}
			if (GravTileA.RB != null) {
				Destroy (GravTileA.RB);
			}
			if (GravTileB.RB != null) {
				Destroy (GravTileB.RB);
			}
			if (GravTileA.gameObject.layer != 10) {
				GravTileA.gameObject.layer = 0;
			}
			if (GravTileB.gameObject.layer != 10) {
				GravTileB.gameObject.layer = 0;
			}
		}
	}

	void Awake()
	{
		Global.Dataholder = this;
		G.Main = this;

	}

	void OnEnable()
	{
		Global.Dataholder = this;
		G.Main = this;

	}

	[ContextMenu("Set Global")]
	void SetGlobal()
	{
		Global.Dataholder = this;
		G.Main = this;

	}



	public float SpeedrunTime;



	public static float ParabolicLerp(float sPos, float dPos, float t, float dur)
	{
		return (((sPos-dPos)*Mathf.Pow(t,2))/Mathf.Pow(dur,2))-(2*(sPos-dPos)*(t))/(dur)+sPos;
	}
	public static float SinLerp(float sPos, float dPos, float t, float dur)
	{
		return Mathf.Sin((Mathf.PI*(t))/(2*dur))*(dPos-sPos) + sPos;
	}
	public static float TwoCurveLerp(float sPos, float dPos, float t, float dur)
	{
		return -Mathf.Cos(Mathf.PI*t*(1/dur))*0.5f*(dPos-sPos)+0.5f*(sPos+dPos);
	}
	// Converts a float in seconds to a string in MN:SC.DC format
	// example: 68.1234 becomes "1:08.12"
	public static string StringifyTime(float time)
	{
		string s = "";
		int min = 0;
		while(time >= 60){time-=60;min++;}
		time = Mathf.Round(time*100f)/100f;
		s = "" + time;
		if(!s.Contains(".")){s+=".00";}
		else{if(s.Length == s.IndexOf(".")+2){s+="0";}}
		if(s.IndexOf(".") == 1){s = "0" + s;}
		s = min + ":" + s;
		return s;
	}

	public static string StringifyTimeInteger(float time)
	{
		time = Mathf.Ceil (time);
		string s = "";
		int min = 0;
		while(time >= 60){time-=60;min++;}
		time = Mathf.Round(time*100f)/100f;
		s = "" + time;
		if(s.Length == 1){s = "0" + s;}
		s = min + ":" + s;
		return s;
	}

	public static string StringifyTimeWithHours(float time,int minutes)
	{
		string s = "";
		int min = minutes%60;
		int hour = minutes/60;
		time = Mathf.Round(time*100f)/100f;
		s = "" + time;
		if(!s.Contains(".")){s+=".00";}
		else{if(s.Length == s.IndexOf(".")+2){s+="0";}}
		if(s.IndexOf(".") == 1){s = "0" + s;}
		s = (hour>0?(""+hour+":"):(""))+ ((min>9 || hour<1)?(""+min):("0"+min)) + ":" + s;
		return s;
	}




}
