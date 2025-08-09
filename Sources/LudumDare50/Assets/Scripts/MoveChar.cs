using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveChar : MonoBehaviour {



	public int Level;

	public GameObject AboveGround;

	public float BuildingCollapseTimer;
	public bool BuildingIsCollapsing;

	public bool DayComplete;
	public float DayCompleteTimer;

	public bool DayFailCollapse;
	public float FailCollapseTimer;

	public bool ClearedTutorial; //first sale
	public bool ClearedTutorial2; // gone underground
	public bool ClearedTutorial3; // Picked Up Bone
	public float Tut3Delay;

	public Text TM;
	public Text[] TMOutline;
	public Text TM2;
	public Text[] TM2Outline;

	public Text TMT;
	public Text[] TMTOutline;

	public GameObject BoneStructure;
	public Transform BoneStructHolder;

	public bool InWardrobe;
	public WardrobeManager WardMan;
	public bool MainRoom;
	public bool AtCheckout;
	public Patron Customer;
	public RegisterScanGun PriceGun;

	public bool OnBroom;
	public bool OnFeet;

	public bool Witch;

	public float DesignatedAltitude;

	public RuntimeAnimatorController WitchIdle;
	public RuntimeAnimatorController WitchBroom;
	public RuntimeAnimatorController ManagerIdle;
	public RuntimeAnimatorController ManagerRun;

	public float speedLimit;
	public float speedX;
	public float speedY;

	public Rigidbody RB;
	public SpriteRenderer SR;
	public Animator Anim;

	public float Boost;

	public GameObject MagicBullet;

	public Camera Cam;

	public float ExpandCamera;

	public int BoneCount;
	public int BoneGoal;
	public int BoneSupports;
	public int SupportGoal;
	public Transform BroomParticles;

	public GameObject Parallaxx;

	public bool HopOffBroom;
	public bool JustGotOnBroomGrace;


	public float HopOffTimer;
	public SpriteRenderer ClosetBroom;
	public Vector2 HopOffSPos;
	public Vector2 HopOffDPos;
	public Vector2 HopOffCamSpos;
	public Vector2 HopOffCamDPos;
	public float HopOffCamSScale;
	public float HopOffCamDScale;
	public float HopOffSRot;
	public float HopOffDRot;


	public Transform ClosetFloorRight;
	public Transform ClosetFloorLeft;

	public float ClosetFloorRightSPos;
	public float ClosetFloorRightDPos;
	public float ClosetFloorLeftSPos;
	public float ClosetFloorLeftDPos;

	public SpriteRenderer FloorFade;
	public float FloorFadeS;
	public float FloorFadeD;

	public bool HopOnBroom;
	public float HopOnTimer;

	public bool PrepareBroom;
	public float PrepareTimer;
	public bool ReadyForLiftoff;
	public float PrepareSRot;
	public float PrepareDRot;
	public float LiftoffDelay;

	public GameObject[] DinoLayouts;
	public GameObject CurrentDinos;



	public int FailedDays;
	public Animator FailCollapseAnim;
	public RuntimeAnimatorController BuildingCollapse;


	public int[] BoneGoals;
	public int[] StructGoals;
	public float[] TimeByLevel;

	public GameObject CollapseSFX;
	public bool LoseOnce;

	public float Screenshake;

	public float InitialTutorialSpawn =3;
	public float DelayBeforeQuake;
	public bool doThisOnce;
	public bool doThisOnce2;

	public bool BeenUnderground;

	public float UndergroundTimer;
	public float EraOpacity;

	public Text EraText;
	public string[] EraMessages;

	public bool FailReview;
	public bool RevealBadeReview;
	public float ReviewTimer;
	public Transform Review;
	public TextMesh ReviewText;
	public Transform ReviewOffset;

	public Animator VictoryVictory;
	public RuntimeAnimatorController VictoryKaboom;

	public string SpeedrunTime;
	public float TimeSinceGameStarted;
	public bool CalculateSpeedrunOnce;
	public AudioSource Music;
	public float Pitch;
	public GameObject BadReview;
	public GameObject VictoryJingle;
	public bool VictoryJingleOnce;

	public float volume = 1;
	public GameObject BoneSFX;

	public GameObject BoneSupportSFX;
	public float FadeVolume;

	public bool Paused;
	public GameObject PauseText;

	public bool CLOSE;


	// Use this for initialization
	void Start () {
		EraMessages[2] = "The Era of Television\nStreaming Services";
	}

	void Update()
	{
		if(CLOSE)
		{
			if(Input.GetKey(KeyCode.W))
			{
				Application.Quit();
			}

		}



		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Paused = !Paused;
		}
		PauseText.SetActive(Paused);
		if(Paused)
		{
			return;
		}

		TimeSinceGameStarted += Time.deltaTime;

		ReviewOffset.transform.localScale = Vector3.one * Cam.orthographicSize;

		if(FailReview)
		{
			AssignTextT("");

			Pitch -= Time.deltaTime*0.5f;
			if(Pitch < 0)
			{
				Pitch = 0;
				Music.volume = 0;
			}
			Music.pitch = Pitch;


			return;
		}

		if(BeenUnderground)
		{
			UndergroundTimer += Time.deltaTime;
			EraOpacity = -0.1f*(UndergroundTimer-4)*(UndergroundTimer-4)+1;
			EraText.text = EraMessages[Level];
			EraText.color = new Vector4(1,1,1,EraOpacity);
		}

		if(ClearedTutorial2 && !ClearedTutorial3)
		{

			Tut3Delay -= Time.deltaTime;

			if(Tut3Delay < 0 && !doThisOnce2)
			{
				doThisOnce2 = true;
				AssignTextT("We're going to need to support the building\nusing dinosaur bones.\n\n[ Left Mouse Button ] to shoot lasers.\n[ Shift ] to boost.");

			}


		}

		if(ClearedTutorial && !AtCheckout)
		{
			DelayBeforeQuake -= Time.deltaTime;
		
			if(DelayBeforeQuake < 0 && !doThisOnce)
			{
				doThisOnce = true;
				Screenshake = 0.75f;
			}

			if(DelayBeforeQuake < -1 && MainRoom &&!ClearedTutorial2)
			{
				AssignTextT("The building is collapsing! You can fix this...\nHead into the broom closet!");

			}
		
		}

		InitialTutorialSpawn -= Time.deltaTime;
		if(InitialTutorialSpawn < 0 &&!ClearedTutorial)
		{


			AssignTextT("You need to check out the VHS tapes that your\ncustomers are interested in renting.\n\nPress [ W ] to interact with the checkout stand.");

		}
				

		Screenshake -= Time.deltaTime;
		if(Screenshake > 0)
		{
			AboveGround.transform.position = new Vector3(Random.Range(Screenshake,-Screenshake),Random.Range(Screenshake,-Screenshake),0);
			FailCollapseAnim.runtimeAnimatorController = null;


		}
		else
		{
			AboveGround.transform.position = Vector3.zero;

		}


		if(BuildingIsCollapsing)
		{
			if(BoneSupports < SupportGoal)
			{
				BuildingCollapseTimer -= Time.deltaTime;

				if(BuildingCollapseTimer <=0 )
				{
					if(!DayFailCollapse)
					{
						LoseOnce = false;
						DayFailCollapse = true;
						FailCollapseTimer = 0;
					}
					BuildingCollapseTimer = 0;
				}

			}
		}
		if(BoneCount >= BoneGoal)
		{
			BoneCount -= BoneGoal;
			BoneSupports++;
			Instantiate(BoneSupportSFX);
			BuildingCollapseTimer+=60;
			Instantiate(BoneStructure,BoneStructHolder.position + new Vector3(Random.Range(-22f,20f),-170,0),BoneStructHolder.rotation,BoneStructHolder);
			if(BoneSupports >= SupportGoal)
			{

				if(Customer.PreEnterBuilding)
				{
					Customer.PreEnterBuilding = false;
					Customer.WinLevel = true;
					Customer.WinTimer = 0;
				}
			}
		}

		if(BuildingIsCollapsing && !DayComplete)
		{
			if(BoneSupports < SupportGoal)
			{
				int min =0;
				float seconds = BuildingCollapseTimer;
				string Timee = "";

				while(seconds >60)
				{
					min++;
					seconds-=60;
				}

				Timee = seconds >= 10 ? "" + min + ":" + Mathf.Round(seconds*100f)/100f : + min + ":0" + Mathf.Round(seconds*100f)/100f;

				AssignText("Building Collapse in "+Timee);
			}
			else
			{
				AssignText("Building Secure!");

			}
		}
		else
		{
			AssignText("");


		}

		if(DayFailCollapse)
		{
			Pitch -= Time.deltaTime*0.5f;
			if(Pitch < 0)
			{
				Pitch = 0;
				Music.volume = 0;
			}
			Music.pitch = Pitch;

			return;
		}
		if(DayComplete)
		{
			FadeVolume -= Time.deltaTime;

			Music.volume = FadeVolume;

			if(FadeVolume <= 0)
			{
				Music.pitch = 0;
			}

			return;
		}
		else
		{
			FadeVolume = 1;
		}
		if(BeenUnderground)
		{
			AssignText2("Bones: " + BoneCount +"/"+BoneGoal+"\nSupports: " + BoneSupports+"/"+SupportGoal);
		}
		else
		{
			AssignText2("");

		}

		Music.pitch = 1;
		Music.volume = volume;
		Pitch = 1;


		if(MainRoom)
		{
			if(AtCheckout)
			{

				return;

			}

			//AssignTextT("");
			Cam.orthographicSize = 8;
			Cam.transform.position = new Vector3(transform.position.x,300,-50);
			if(Cam.transform.position.x < -28)
			{
				Cam.transform.position = new Vector3(-28,300,-50);
			}
			else if(Cam.transform.position.x >0)
			{
				Cam.transform.position = new Vector3(0,300,-50);
			}
		}


		if(OnBroom)
		{
			if(Input.GetKeyDown(KeyCode.Mouse0))
			{
				Vector3 Mpos = Cam.ScreenToWorldPoint(Input.mousePosition);
				Vector3 Norm = -(transform.position - Mpos);
				Norm = new Vector3(Norm.x,Norm.y,0).normalized;

				MagicBolt Bullet =	Instantiate(MagicBullet,transform.position + Norm,transform.rotation).GetComponent<MagicBolt>();
				Bullet.Char = this;
				Bullet.Speed = Norm*2 + new Vector3(speedX,speedY); 
				Bullet.transform.eulerAngles =  new Vector3(0,0,Mathf.Atan2(Bullet.Speed.y,Bullet.Speed.x) * Mathf.Rad2Deg);

			}

			Cam.transform.position = transform.position + new Vector3(0,0,-50);

			if(transform.position.y > 175)
			{
				Parallaxx.SetActive(false);
			}
			else
			{
				Parallaxx.SetActive(true);

			}

			if(transform.position.y < 210)
			{
				JustGotOnBroomGrace = false;
			}

			if(transform.position.y > 210 && !JustGotOnBroomGrace)
			{
				HopOffBroom = true;
				OnBroom = false;
				HopOffSPos = transform.position;
				HopOffCamSpos = Cam.transform.position;
				HopOffCamSScale = Cam.orthographicSize;
				HopOffTimer = 0;

				ClosetFloorRightSPos = 20;
				ClosetFloorRightDPos = 15;
				ClosetFloorLeftSPos = -2;
				ClosetFloorLeftDPos = 9;

				HopOffSRot = transform.eulerAngles.z;

			}
		}
	}


	// Update is called once per frame
	void FixedUpdate () {

		if(Paused)
		{
			RB.velocity = Vector3.zero;
			return;
		}


		if(!DayFailCollapse && !FailReview)
		{
			if(InWardrobe)
			{
				RB.velocity = Vector3.zero;

				return;
			}
			if(AtCheckout)
			{
				RB.velocity = Vector3.zero;

				return;
			}
		}
		if(DayComplete)
		{
			RB.velocity = Vector3.zero;

			DayCompleteTimer += Time.fixedDeltaTime;

			if(DayCompleteTimer > 1)
			{

				Cam.transform.position = new Vector3(0,400,-50);
				Cam.orthographicSize = 8;
			}
			AssignText2("");
			AssignText("");

			if(DayCompleteTimer > 2)
			{
				if(!VictoryJingleOnce)
				{
					VictoryJingleOnce = true;

					Instantiate(VictoryJingle);

				}

				if(Level == 0)
				{
					AssignText2("                         First Day Complete!");
				}
				else if(Level == 1)
				{
					AssignText2("                        Second Day Complete!");
				}
				if(Level == 2)
				{
					AssignText2("            Your video store is unstoppable!");
				}	

			}

			if(Level == 2)
			{

				// meteor and victory screen
				if(DayCompleteTimer > 4)
				{

					VictoryVictory.runtimeAnimatorController = VictoryKaboom;

				}

				if(DayCompleteTimer > 6)
				{
					AssignText2("            ");

				}

				if(DayCompleteTimer > 9.5f)
				{
					AssignText2("            Well... that was bound to happen.");

				}

				if(DayCompleteTimer > 13)
				{
					AssignText2("            ");

				}


				if(DayCompleteTimer > 14)
				{
					if(!CalculateSpeedrunOnce)
					{
						CalculateSpeedrunOnce = true;

						int min =0;
						float seconds = TimeSinceGameStarted;

						while(seconds >60)
						{
							min++;
							seconds-=60;
						}

						SpeedrunTime = seconds >= 10 ? "" + min + ":" + Mathf.Round(seconds*100f)/100f : + min + ":0" + Mathf.Round(seconds*100f)/100f;

					

					}

					if(Input.GetKey(KeyCode.W))
					{
						Application.Quit();
					}

					AssignText2("YOU WIN!                  Press [ W ] to close the application.\nSpeedrun time: " + SpeedrunTime);
					CLOSE = true;
				}

			}
			else
			{
				if(DayCompleteTimer > 5)
				{
					
					ResetStuffForDay(true);

				}

			}


			return;
		}

		if(DayFailCollapse)
		{
			Cam.transform.position = new Vector3(0,425,-50);

			Customer.RandomArriveTimer = 1000000;
			Customer.PreEnterBuilding = true;
			Customer.EnterBuilding = false;
			Customer.SearchForMovie = false;
			Customer.WalkToMovies = false;
			Customer.AtCounter = false;
			Customer.HappyLeave = false;
			Customer.SadgeLeave = false;
			Customer.WaitingForYou = false;

			FailCollapseTimer += Time.fixedDeltaTime;

			AssignTextT("");

			if(FailCollapseTimer > 1)
			{				
				FailCollapseAnim.runtimeAnimatorController = BuildingCollapse;
			}
			if(FailCollapseTimer > 1.9f && !LoseOnce)
			{
				LoseOnce = true;
				Instantiate(CollapseSFX,transform.position,transform.rotation);
			}
			transform.position = new Vector3(-500,0,0); // to avoid hitting interactibles

			if(FailCollapseTimer > 2.5f)
			{
				AssignText2("                       Press [ W ] to try again.");
				if(Input.GetKey(KeyCode.W))
				{

					ResetStuffForDay(false);

				}

			}
		}


		if(FailReview)
		{


			float dur = 2;
			if(ReviewTimer < dur)
			{
				ReviewTimer+= Time.fixedDeltaTime;

			float d = 0;
			float s = -16;

			float NewY = Mathf.Sin((3.141592f*ReviewTimer)/(2*dur))*(d-s)+s;

			Review.localPosition = new Vector3(0,NewY,1);
				if(ReviewTimer >= dur)
				{
					Instantiate(BadReview);
				}
			}
			else
			{
				ReviewTimer+= Time.fixedDeltaTime;

			}

			if(ReviewTimer > 3.5f)
			{

				AssignText2("                       Press [ W ] to try again.");
				if(Input.GetKey(KeyCode.W))
				{

					ResetStuffForDay(false);

				}

			}

			RB.velocity = Vector3.zero;
			return;

		}



		int dirX = 0;
		int dirY = 0;

		dirX += Input.GetKey(KeyCode.D) ? 1 : 0;
		dirX += Input.GetKey(KeyCode.A) ? -1 : 0;
		dirY += Input.GetKey(KeyCode.W) ? 1 : 0;
		dirY += Input.GetKey(KeyCode.S) ? -1 : 0;

		if(dirX < 0 && speedX > 0){dirX*=3;}
		if(dirX > 0 && speedX < 0){dirX*=3;}
		if(dirY < 0 && speedY > 0){dirY*=3;}
		if(dirY > 0 && speedY < 0){dirY*=3;}


		speedX += dirX * Time.deltaTime *3;
		speedY += dirY * Time.deltaTime *3;

		if(OnBroom)
		{
			if(dirX == 0)
			{
				speedX *= 0.9f;
			}
			if(dirY == 0)
			{
				speedY *= 0.9f;
			}
		}
		else
		{
			if(dirX == 0)
			{
				speedX *= 0.8f;
			}
		}

		// limit speed
		if(speedX > speedLimit)
		{
			speedX = speedLimit; 
		}
		if(speedX < -speedLimit)
		{
			speedX = -speedLimit; 
		}
		if(speedY > speedLimit)
		{
			speedY = speedLimit; 
		}
		if(speedY < -speedLimit)
		{
			speedY = -speedLimit; 
		}


		if(OnBroom)
		{
			BroomParticles.transform.parent = SR.transform;

			BroomParticles.localPosition = new Vector3(0,0,0);
			BroomParticles.localEulerAngles = new Vector3(0,0,0);


			SR.flipX = false;

			float boost = Input.GetKey(KeyCode.LeftShift) ? Boost : 1;

			RB.velocity = new Vector3(speedX,speedY,0)*32 * boost;
			SR.transform.eulerAngles = new Vector3(0,0,Mathf.Atan2(speedY,speedX)*Mathf.Rad2Deg+180);
			SR.flipY = speedX >0;
			BroomParticles.localScale = speedX >0 ? new Vector3(1,-1,1) : new Vector3(1,1,1);

			if((dirX != 0 || dirY != 0) && Input.GetKey(KeyCode.LeftShift))
			{

				ExpandCamera += Time.fixedDeltaTime;

			}
			else
			{
				ExpandCamera -= Time.fixedDeltaTime;

			}

			float dur = 1;
			float Spos = 16;
			float Dpos = 24;
			float size = -Mathf.Cos(3.141592f*ExpandCamera*(1/dur))*0.5f*(Dpos-Spos) + 0.5f*(Spos +Dpos);

			Cam.orthographicSize = size;

			if(ExpandCamera > dur)
			{
				Cam.orthographicSize = Dpos;
				ExpandCamera = dur;
			}
			else if(ExpandCamera < 0)
			{
				ExpandCamera = 0;
				Cam.orthographicSize = Spos;
			}
		}

		if(OnFeet)
		{
			BroomParticles.position = new Vector3(HopOffDPos.x,HopOffDPos.y,0);
			BroomParticles.transform.parent = ClosetBroom.transform;
			RB.velocity = new Vector3(speedX,0,0)*16;
			SR.flipX = speedX >0;
			if(Witch)
			{
				Anim.runtimeAnimatorController = WitchIdle;
			}
			else
			{
				Anim.runtimeAnimatorController = dirX != 0 ? ManagerRun : ManagerIdle;

			}
			transform.position = new Vector3(transform.position.x,DesignatedAltitude,0);


		}



		if(HopOffBroom)
		{
			float dur = 2;

			if(HopOffTimer < dur)
			{
				SR.flipY = false;
				BroomParticles.localScale = new Vector3(1,1,1);

				RB.velocity = Vector3.zero;

				HopOffTimer += Time.fixedDeltaTime;

				float NewX = Mathf.Sin((3.141592f*HopOffTimer)/(2*dur))*(HopOffDPos.x-HopOffSPos.x)+HopOffSPos.x;
				float NewY = Mathf.Sin((3.141592f*HopOffTimer)/(2*dur))*(HopOffDPos.y-HopOffSPos.y)+HopOffSPos.y;
				float CamX = Mathf.Sin((3.141592f*HopOffTimer)/(2*dur))*(HopOffCamDPos.x-HopOffCamSpos.x)+HopOffCamSpos.x;
				float CamY = Mathf.Sin((3.141592f*HopOffTimer)/(2*dur))*(HopOffCamDPos.y-HopOffCamSpos.y)+HopOffCamSpos.y;
				float CamScale = Mathf.Sin((3.141592f*HopOffTimer)/(2*dur))*(HopOffCamDScale-HopOffCamSScale)+HopOffCamSScale;
				float Rot = Mathf.Sin((3.141592f*HopOffTimer)/(2*dur))*(HopOffDRot-HopOffSRot)+HopOffSRot;

				float FloorRight = Mathf.Sin((3.141592f*HopOffTimer)/(2*dur))*(ClosetFloorRightDPos-ClosetFloorRightSPos)+ClosetFloorRightSPos;
				float FloorLeft = Mathf.Sin((3.141592f*HopOffTimer)/(2*dur))*(ClosetFloorLeftDPos-ClosetFloorLeftSPos)+ClosetFloorLeftSPos;
				float FloorFadeColor = Mathf.Sin((3.141592f*HopOffTimer)/(2*dur))*(FloorFadeD-FloorFadeS)+FloorFadeS;


				RB.position = new Vector3(NewX,NewY,0);
				Cam.transform.position = new Vector3(CamX,CamY,-50);
				Cam.orthographicSize = CamScale;
				SR.transform.eulerAngles= new Vector3(0,0,Rot);
				ClosetFloorLeft.transform.localPosition = new Vector3(FloorLeft,8,0);
				ClosetFloorRight.transform.localPosition = new Vector3(FloorRight,8,0);
				FloorFade.color = new Vector4(0,0,0,FloorFadeColor);

				if(HopOffTimer >= dur)
				{
					if(ClearedTutorial3)
					{
						AssignTextT("");
					}

					transform.position = new Vector3(HopOffDPos.x,DesignatedAltitude,0);
					Cam.transform.position = new Vector3(HopOffCamDPos.x,HopOffCamDPos.y,-50);
					Cam.orthographicSize = HopOffCamDScale;
					OnFeet = true;
					Anim.runtimeAnimatorController = WitchIdle;
					ClosetBroom.enabled = true;
					HopOffBroom = false;
					BroomParticles.gameObject.SetActive(false);
				}
			}
		}

		if(HopOnBroom)
		{
			float dur = 1;
			SR.flipX = false;
			RB.velocity = Vector3.zero;

			HopOnTimer += Time.fixedDeltaTime;

			float NewX = Mathf.Sin((3.141592f*HopOnTimer)/(2*dur))*(HopOffDPos.x-HopOffSPos.x)+HopOffSPos.x;
			float NewY = Mathf.Sin((3.141592f*HopOnTimer)/(2*dur))*(HopOffDPos.y-HopOffSPos.y)+HopOffSPos.y;
			// move player
			transform.position = new Vector3(NewX,NewY,0);
			speedX = 0;

			if(HopOnTimer > dur)
			{
				ClosetBroom.enabled = false;
				Anim.runtimeAnimatorController = WitchBroom;

				HopOnBroom = false;
				PrepareBroom = true;
				PrepareTimer = 0;



			}


		}

		if(PrepareBroom)
		{

			// rotate broom
			// open floors
			float dur  =2;

			PrepareTimer += Time.fixedDeltaTime;
			RB.velocity = Vector3.zero;


			float Rot = Mathf.Sin((3.141592f*PrepareTimer)/(2*dur))*(PrepareDRot-PrepareSRot)+PrepareSRot;
			float FloorRight = Mathf.Sin((3.141592f*PrepareTimer)/(2*dur))*(ClosetFloorRightDPos-ClosetFloorRightSPos)+ClosetFloorRightSPos;
			float FloorLeft = Mathf.Sin((3.141592f*PrepareTimer)/(2*dur))*(ClosetFloorLeftDPos-ClosetFloorLeftSPos)+ClosetFloorLeftSPos;
			float FloorFadeColor = Mathf.Sin((3.141592f*PrepareTimer)/(2*dur))*(FloorFadeS-FloorFadeD)+FloorFadeD;

			SR.transform.eulerAngles = new Vector3(0,0,Rot);

			ClosetFloorLeft.transform.localPosition = new Vector3(FloorLeft,8,0);
			ClosetFloorRight.transform.localPosition = new Vector3(FloorRight,8,0);
			FloorFade.color = new Vector4(0,0,0,FloorFadeColor);
			if(PrepareTimer > dur)
			{
				PrepareBroom = false;
				ReadyForLiftoff = true;
				LiftoffDelay = 0.715f;
				// instantiate particles
			}



		}

		if(ReadyForLiftoff)
		{
			LiftoffDelay -= Time.fixedDeltaTime;

			if(!ClearedTutorial2)
			{
				AssignTextT("Hold [ A ] and [ S ] to launch yourself.");
			}

			if(LiftoffDelay <=0)
			{
				if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
				{
					// LIFTOFF!!!
					BroomParticles.gameObject.SetActive(true);
					BeenUnderground = true;
					speedX = -speedLimit;
					speedY = -speedLimit;
					OnBroom = true;
					ReadyForLiftoff = false;
					JustGotOnBroomGrace = true;
					if(!ClearedTutorial2)
					{
						doThisOnce = false;
						Tut3Delay = 2;
						AssignTextT("");
					}
					ClearedTutorial2 = true;
				}
			}


		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Bone")
		{
			Instantiate(BoneSFX);
			BoneCount++;
			if(!ClearedTutorial3)
			{
				AssignTextT("As more customers arrive, you will need to\nfigure out which to prioritize. Helping them,\nor collecting bones.\n\nGood luck!");
				Customer.RandomArriveTimer = 3;
			}

			ClearedTutorial3 = true;
			Destroy(other.gameObject);
		}

	}


	public void MountBroom()
	{
		OnFeet = false;

		HopOnBroom = true;
		HopOffSPos = transform.position;
		HopOffCamSpos = Cam.transform.position;
		HopOffCamSScale = Cam.orthographicSize;
		HopOnTimer = 0;

		ClosetFloorRightSPos = 15;
		ClosetFloorRightDPos = 20;
		ClosetFloorLeftSPos = 9;
		ClosetFloorLeftDPos = -2;

		HopOffSRot = transform.eulerAngles.z;

	}

	void AssignText(string s)
	{
		TM.text = s;
		int i = 0;
		while(i < TMOutline.Length)
		{
			TMOutline[i].text = s;
			i++;
		}
	}

	void AssignText2(string s)
	{
		TM2.text = s;
		int i = 0;
		while(i < TM2Outline.Length)
		{
			TM2Outline[i].text = s;
			i++;
		}
	}
	public void AssignTextT(string s)
	{
		TMT.text = s;
		int i = 0;
		while(i < TMTOutline.Length)
		{
			TMTOutline[i].text = s;
			i++;
		}
	}

	public void ResetStuffForDay(bool NewDay)
	{
		Cam.transform.position = new Vector3(-16,300,-50);
		transform.position = new Vector3(-16,298,0);
		DesignatedAltitude = 298;
		DayCompleteTimer =0;
		DayComplete = false;
		OnBroom = false;
		HopOnBroom = false;
		HopOffBroom = false;
		OnFeet = true;
		Witch = false;
		FloorFade.color = new Vector4(0,0,0,1);
		MainRoom = true;
		Review.localPosition = new Vector3(0,-16,1);
		FailReview = false;
		ReviewTimer = 0;
		BoneCount = 0;
		BoneSupports = 0;

		LoseOnce = false;
		BuildingIsCollapsing = false;
		DayFailCollapse = false;
		FailCollapseTimer = 0;
		BeenUnderground = false;
		BroomParticles.gameObject.SetActive(false);
		ClosetBroom.enabled = true;
		ClosetFloorRightSPos = 15;
		ClosetFloorLeftSPos = 9;
		ClosetFloorLeft.transform.localPosition = new Vector3(ClosetFloorLeftSPos,8,0);
		ClosetFloorRight.transform.localPosition = new Vector3(ClosetFloorRightSPos,8,0);


		Customer.RandomArriveTimer =1;
		Customer.PreEnterBuilding = true;
		Customer.WinLevel = false;
		Customer.WinTimer = 0;
		Customer.transform.localPosition = new Vector3(-27,-2,0);
		Customer.FistCustomerOfTheDay = true;
		Customer.LoseLevel = false;
		Customer.PreEnterBuilding = true;
		Customer.EnterBuilding = false;
		Customer.SearchForMovie = false;
		Customer.WalkToMovies = false;
		Customer.AtCounter = false;
		Customer.HappyLeave = false;
		Customer.SadgeLeave = false;
		Customer.WaitingForYou = false;

		Customer.leaveAccel = 0;

		WardMan.CharBody.sprite = WardMan.BodyNice;
		WardMan.CharHead.sprite = WardMan.HairNice;



		WardMan.HasBag = false;
		WardMan.HasHat = false;
		WardMan.WitchHair = false;
		WardMan.WitchDress = false;

		VictoryJingleOnce = false;

		Cam.orthographicSize = 8;

		SR.transform.eulerAngles = new Vector3(0,0,0);
		SR.flipX = false;
		SR.flipY = false;
		UndergroundTimer = 0;
		if(NewDay)
		{
			Level++;
		}

		Destroy(CurrentDinos);
		CurrentDinos = Instantiate(DinoLayouts[Level],DinoLayouts[Level].transform.position,DinoLayouts[Level].transform.rotation) as GameObject;

		BoneGoal = BoneGoals[Level];
		SupportGoal = StructGoals[Level];
		BuildingCollapseTimer = TimeByLevel[Level];
			
		int i = 0;
		int c = BoneStructHolder.childCount;
		while(i < c)
		{
			Destroy(BoneStructHolder.GetChild(i).gameObject);
			i++;
		}


	}

	public string[] PossibleReviews;


	public void GenerateBadReview()
	{

		PossibleReviews = new string[]
		{
			"Did you know the\ntapes are free?\nyou can take them\nhome. I own 458 VHS\ntapes.",
			"Terrible service.\nThe athmosphere\nisn't remotely\nhomely! Far from\nthe experience I\ncame for.",
			"The ground was\nshaking the entire\ntime I was on their\nproperty.\nOSHA? HELLO?!?!",
			"The manager wasn't\neven my type. Does\nanybody know\nof a better way to\nmeet people?",
			"Half the VHS tapes\nwere cardboard\ncutouts of better\nmovies than the\nreal VHS tapes.",
			"Piracy is so much\neasier than the\nhassle of renting\na VHS tape."
		};


		
		FailReview = true;
		ReviewTimer = 0;

		int r  = Random.Range(0,PossibleReviews.Length);

		ReviewText.text = PossibleReviews[r];

	}


}
