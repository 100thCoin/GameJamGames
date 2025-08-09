using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public SpriteRenderer SR;
	public Animator Anim;

	public RuntimeAnimatorController Idle;
	public RuntimeAnimatorController Run;
	public RuntimeAnimatorController Attack;
	public RuntimeAnimatorController AttackFast;

	public SpriteRenderer White;
	public Animator WhiteAnim;
	public RuntimeAnimatorController WhiteIdle;
	public RuntimeAnimatorController WhiteRun;
	public RuntimeAnimatorController WhiteAttack;
	public RuntimeAnimatorController WhiteAttackFast;

	public Rigidbody RB;

	public float Speed;

	public float VelX;
	public float VelY;

	public int Health;

	public Camera Cam;

	public GameObject HurtSFX;

	public bool XVelRise;
	public bool YVelRise;

	public CamMover CMov;

	public Transform Shadow;

	public bool Attacking;
	public float AttackingTimer;
	public Transform AttackCol;
	public Transform AttackColDir;

	int inhousegradient;
	public Transform Inhouse;
	public Material Whoosh;
	public float sleepTimer = -1;

	public TextMesh SleepText1;
	public TextMesh SleepText2;

	public ShopButton ShopButt;



	public string[] SleepMessages1;
	public string[] SleepMessages2;

	public bool ActiveShop;
	public float ActiveShopTimer;
	public Transform Shop;


	public Transform CornShop;
	public bool ActiveCornShop;
	public float ActiveCornShopTimer;

	public bool Caught;
	public float CaughtTimer;
	public NPC CaughtBy;
	public GameObject CaughtSFX;

	public bool WinGame;
	public float winGameTimer;
	public SpriteRenderer WinScreenR;
	public GameObject Victory;
	public Sprite Win2;
	public Sprite Win3;
	public GameObject WinStuffs;

	public bool SayVictoryVoice;
	public GameObject VictoryVoice;

	public bool badCrop;
	public float BadCropTimer;
	public AudioSource Farmin;
	public AudioSource Paranoia;

	public float ParanoidScale;
	public Material Filter;
	public bool doBadCrop;

	public float KillFilter;
	public float CaughtFilter;

	public float StealFilter;

	public bool StealVoiceOnce;
	public GameObject Voice_Steal;
	public GameObject Voice_SeeWhatYouGrew;
	public GameObject Voice_NothingGrew;
	public GameObject voiceItfigures;


	public bool doBadCropagain;
	// Use this for initialization
	void Start () {

	}


	void Update()
	{

		if (Main.Data.Day == 1 &&!doBadCrop) {
			if (Cam.transform.position.y < 5) {
				badCrop = true;
				doBadCrop = true;

				if (Main.Data.CurrentVoiceLine == null) {
					Main.Data.CurrentVoiceLine = Instantiate (Voice_NothingGrew);
				} else {
					Main.Data.QueuedVoiceLine = Voice_NothingGrew;
				}

			}

		}

		if (Main.Data.Day == 2 &&!doBadCropagain) {
			if (Cam.transform.position.y < 5) {
				doBadCropagain = true;

				if (Main.Data.CurrentVoiceLine == null) {
					Main.Data.CurrentVoiceLine = Instantiate (voiceItfigures);
				} else {
					Main.Data.QueuedVoiceLine = voiceItfigures;
				}

			}

		}

		if (badCrop) {
			Main.Data.EarlySteal = true;
			BadCropTimer += Time.deltaTime;
			Farmin.volume -= Time.deltaTime;
			Farmin.GetComponent<Volume> ().enabled = false;

			ParanoidScale += Time.deltaTime * 0.05f;

			if (BadCropTimer > 4) {

				ParanoidScale = 0.2f;

			}


			if (BadCropTimer > 11) {
				if (!StealVoiceOnce) {
					StealVoiceOnce = true;
					if (Main.Data.CurrentVoiceLine == null) {
						Main.Data.CurrentVoiceLine = Instantiate (Voice_Steal);
					} else {
						Main.Data.QueuedVoiceLine = Voice_Steal;
					}
				}
			}

			if (BadCropTimer > 12.25f) {

				Paranoia.gameObject.SetActive (true);

				badCrop = false;

			}

		}


		KillFilter -= Time.deltaTime * 0.1f;

		KillFilter = Mathf.Clamp01 (KillFilter);


		Filter.SetFloat ("_Power", ParanoidScale + KillFilter + CaughtFilter);

		if (WinGame) {
			Main.Data.MusicVolume -= Time.deltaTime*0.3f;
			winGameTimer += Time.deltaTime;
			Main.Data.ToDoList.SetActive (false);

			Whoosh.SetFloat ("_Pos",winGameTimer);

			if (winGameTimer > 2) {
				Victory.SetActive (true);
				if (!SayVictoryVoice) {
					SayVictoryVoice = true;
					Instantiate (VictoryVoice);
				}

				WinScreenR.enabled = true;

			}

			if (winGameTimer > 3.8f) {

				WinScreenR.sprite = Win2;
			}
			if (winGameTimer > 5.1f) {

				WinScreenR.sprite = null;
			}
			if (winGameTimer > 6) {

				WinScreenR.sprite = Win3;
				WinStuffs.SetActive (true);
			}
			return;
		}

		if(Main.Data.Paused)
		{
			if (Input.GetKeyDown (KeyCode.Escape)) {

				Main.Data.Paused = !Main.Data.Paused;
				Main.Data.PauseMenu.SetActive (Main.Data.Paused);
				Main.Data.ToDoList.SetActive (!Main.Data.Paused);

			}

			return;
		}

		if (Caught) {
			Main.Data.ToDoList.SetActive (false);

			if (CaughtTimer == 0) {
				if (CaughtSFX != null) {
					//TODO Add this SFX
					Instantiate (CaughtSFX);

				}
				CMov.CaughtCam = true;
				CMov.CaughtTarg = CaughtBy.transform;
			}

			CaughtTimer += Time.deltaTime;
			CaughtFilter += Time.deltaTime*0.4f;
			CaughtFilter = Mathf.Clamp (CaughtFilter,0, 0.6f);
			if (CaughtTimer > 1) {
				Whoosh.SetFloat ("_Pos",(CaughtTimer-1)*3);
			}

			if (CaughtTimer > 1.7f) {
				CMov.transform.position = new Vector3 (0, 0, CMov.transform.position.z);
				Whoosh.SetFloat ("_Pos",1.29f - (CaughtTimer-1.7f)*3);
				CMov.CaughtCam = false;
				CMov.CaughtTarg = null;
				transform.position = new Vector3 (0, 0, 0);
				Anim.runtimeAnimatorController = Idle;
				if (CaughtBy != null) {
					CaughtBy.Alerted = false;
					CaughtBy.AlertRenderer.enabled = false;
				}
				CaughtFilter = 0;

			}

			if (CaughtTimer > 2.2f) {
				Caught = false;
				Main.Data.ToDoList.SetActive (true);
				Whoosh.SetFloat ("_Pos",-100);
			}


			CMov.ManualUpdate ();

			return;
		}
		else if (sleepTimer >= 0) {
			sleepTimer += Time.deltaTime;
			Main.Data.ToDoList.SetActive (false);
			SleepText1.text = "Day " + (Main.Data.Day+2);
			SleepText2.text = "Rise and shine!";

			if (sleepTimer > 1) {
				SleepText1.color = new Vector4 (1, 1, 1, sleepTimer - 1);
			}

			if (sleepTimer > 2.5f) {
				SleepText2.color = new Vector4 (1, 1, 1, sleepTimer - 2.5f);
			}

			if (sleepTimer > 5f) {
				SleepText1.color = new Vector4 (1, 1, 1, 6 - sleepTimer);
				SleepText2.color = new Vector4 (1, 1, 1, 6 - sleepTimer);
			}

			if (sleepTimer > 6f) {
				Whoosh.SetFloat ("_Pos", 1.29f - ((sleepTimer - 6) * 0.8f) * ((sleepTimer - 6) * 0.8f));
			}

			if (sleepTimer > 6.5f) {
				RB.velocity = new Vector3 (0, -4, 0);
				Anim.runtimeAnimatorController = Run;
				WhiteAnim.runtimeAnimatorController = WhiteRun;
				CMov.ManualUpdate ();
			}
			if (sleepTimer > 7.5f) {
				Main.Data.HouseOpen = false;
				Anim.runtimeAnimatorController = Idle;
				WhiteAnim.runtimeAnimatorController = WhiteIdle;
				sleepTimer = -1;

				Main.Data.Day++;

				if(Main.Data.Day == 1)
				{
					if (Main.Data.CurrentVoiceLine == null) {
						Main.Data.CurrentVoiceLine = Instantiate (Voice_SeeWhatYouGrew);
					} else {
						Main.Data.QueuedVoiceLine = Voice_SeeWhatYouGrew;
					}
				}

				int d = 0;
				while (d < Main.Data.DaySpecificNPCs.Length) {
					if (Main.Data.DaySpecificNPCs [d] != null) {
						Main.Data.DaySpecificNPCs [d].SetActive (Main.Data.Day == d);
					}

					d++;
				}

				if (Main.Data.Day == 4) {

					Main.Data.FinalTodd.transform.position = Main.Data.SpecialOneForYourPumpkin.transform.position + new Vector3 (-3, -2);

				}

				if (Main.Data.Day == 5) {
					Main.Data.CornLadyForDay6.SetActive (false);
				}

				int i = 0;
				while (i < 12) {
					if (Main.Data.TheFarm [i].CropState == 2) {
						Main.Data.TheFarm [i].CropState = 3;
						Main.Data.TheFarm [i].ChangeState ();
					}

					i++;
				}
				Main.Data.ToDoList.SetActive (true);
				Main.Data.Obj_Eat = false;
				Main.Data.Obj_EatChicken = false;
				Main.Data.Obj_CheckCrops = false;
				Main.Data.Obj_WaterPumpkin = false;
				if (Main.Data.Day == 4 || Main.Data.Day == 5) {
					Main.Data.SpecialOneForYourPumpkin.PumpkinStage++;
					Main.Data.SpecialOneForYourPumpkin.CropState = 1;
					Main.Data.SpecialOneForYourPumpkin.Tiles [1].sprite = Main.Data.SpecialOneForYourPumpkin.PumpkinStage1;
				}
			}


		} else {

			if (Input.GetKeyDown (KeyCode.Escape)) {

				Main.Data.Paused = !Main.Data.Paused;
				Main.Data.PauseMenu.SetActive (Main.Data.Paused);
				Main.Data.ToDoList.SetActive (!Main.Data.Paused);

			}

		}


		if (inhousegradient >= 0 && Main.Data.DayComplete) {
			Whoosh.SetFloat ("_Pos",  ((transform.position.y - Inhouse.position.y)/2) * (1.29f/1.8f)+0.5f);
		}

		if (Input.GetKeyDown (KeyCode.Mouse1) && !Attacking && Main.Data.ItemCounts[1] > 0 && transform.position.y > -25f) {

			Attacking = true;
			Anim.runtimeAnimatorController = Attack;
			WhiteAnim.runtimeAnimatorController = WhiteAttack;
			AttackingTimer = 0;
		}

		if (inhousegradient <0) {
			White.color = Vector4.zero;
			Whoosh.SetFloat ("_Pos", 0.5f);
		}


		if (Main.Data.InvSlotItems [Main.Data.CurrentInvSlot] == 4) {
			if(Input.GetKeyDown(KeyCode.Mouse0))
			{
				if (!Main.Data.Obj_Eat) {
					Main.Data.Obj_Eat = true;
					Main.Data.ItemCounts [4]--;
					if (Main.Data.ItemCounts [4] == 0) {
						Main.Data.RemoveItem (4);
					}
				}
			}

		}
		if (Main.Data.InvSlotItems [Main.Data.CurrentInvSlot] == 5) {
			if (Input.GetKeyDown (KeyCode.Mouse0)) {
				if (!Main.Data.Obj_Eat) {
					Main.Data.Obj_Eat = true;
					Main.Data.ItemCounts [5]--;
					if (Main.Data.ItemCounts [5] == 0) {
						Main.Data.RemoveItem (5);
					}
				}
			}
		}
		if (Main.Data.InvSlotItems [Main.Data.CurrentInvSlot] == 6) {
			if (Input.GetKeyDown (KeyCode.Mouse0)) {
				if (!Main.Data.Obj_EatChicken) {
					Main.Data.Obj_EatChicken = true;
					Main.Data.ItemCounts [6]--;
					if (Main.Data.ItemCounts [6] == 0) {
						Main.Data.RemoveItem (6);
					}
				}
			}
		}

	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		if (WinGame) {
			return;
		}

		ActiveShopTimer += Time.fixedDeltaTime * (ActiveShop ? 4 : -4);	
		ActiveShopTimer = Mathf.Clamp01 (ActiveShopTimer);
		Shop.transform.localPosition = new Vector3 (0, DataHolder.SinLerp (-24, 0, ActiveShopTimer, 1), 10);

		ActiveCornShopTimer += Time.fixedDeltaTime * (ActiveCornShop ? 4 : -4);	
		ActiveCornShopTimer = Mathf.Clamp01 (ActiveCornShopTimer);
		CornShop.transform.localPosition = new Vector3 (0, DataHolder.SinLerp (-24, 0, ActiveCornShopTimer, 1), 10);

		if(Health <= 0)
		{
			CMov.ManualUpdate ();
			return;
		}
		if(Main.Data.Paused || sleepTimer >=0 || Caught)
		{
			if (Main.Data.Paused || Caught) {
				RB.velocity = Vector3.zero;
			}
			return;
		}
		RB.velocity = Vector3.zero;

		if (Attacking) {

			if (AttackingTimer == 0) {

				if (VelX != 0 || VelY != 0) {
					AttackingTimer = 0.4001f;
					Anim.runtimeAnimatorController = AttackFast;
					WhiteAnim.runtimeAnimatorController = WhiteAttackFast;

				}

			}

			AttackingTimer += Time.fixedDeltaTime;

			if (AttackingTimer > 0.4f) {

				VelX = CMov.XDir;
				VelY = CMov.YDir;

			}

			if (AttackingTimer > 0.5f && AttackingTimer < 0.66f) {

				if (SR.flipX) {
					AttackCol.transform.localPosition = new Vector3 (2, -1.25f, 0);
				} else {
					AttackCol.transform.localPosition = new Vector3 (-2, -1.25f, 0);
				}
				AttackColDir.transform.localPosition = new Vector3(0,-1.25f,0) + new Vector3(VelX,VelY,0).normalized * 2;
			} else {
				AttackCol.transform.localPosition = new Vector3 (AttackCol.transform.localPosition.x, AttackCol.transform.localPosition.y, -500);
				AttackColDir.transform.localPosition = new Vector3 (AttackColDir.transform.localPosition.x, AttackColDir.transform.localPosition.y, -500);

			}

			if (AttackingTimer > 0.6666f) { //not 5, obvious temp

				Attacking = false;
			
				VelX = 0;
				VelY = 0;
				Anim.runtimeAnimatorController = Idle;
				WhiteAnim.runtimeAnimatorController = WhiteIdle;

				XVelRise = false;
				YVelRise = false;
			}

			RB.velocity += new Vector3(VelX,0);
			RB.velocity += new Vector3(0,VelY);
			RB.velocity = RB.velocity.normalized * (Speed * (0.6666f - AttackingTimer)*4);


			CMov.ManualUpdate ();
			return;
		}



		if(Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
		{
			if(VelX >0)
			{
				VelX -=0.2f;
				XVelRise = true;
			}
			else if(VelX > -1)
			{
				VelX -=0.1f;
				XVelRise = true;

			}

			Anim.runtimeAnimatorController = Run;
			WhiteAnim.runtimeAnimatorController = WhiteRun;

			SR.flipX = false;
			White.flipX = false;
		}
		else if(!Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
		{
			if(VelX <0)
			{
				VelX +=0.2f;
				XVelRise = true;

			}
			else if(VelX < 1)
			{
				VelX +=0.1f;
				XVelRise = true;

			}
			Anim.runtimeAnimatorController = Run;
			WhiteAnim.runtimeAnimatorController = WhiteRun;

			SR.flipX = true;
			White.flipX = true;

		}
		else
		{
			if(VelX <0)
			{
				VelX +=0.2f;
				XVelRise = false;

			}
			else if(VelX > 0)
			{
				VelX -=0.2f;
				XVelRise = false;

			}
			if(Mathf.Abs(VelX) < 0.2)
			{
				VelX = 0;
				XVelRise = false;

			}
		}

		if(Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
		{
			if(VelY >0)
			{
				VelY -=0.2f;
				YVelRise = true;

			}
			else if(VelY > -1)
			{
				VelY -=0.1f;
				YVelRise = true;

			}

			Anim.runtimeAnimatorController = Run;
			WhiteAnim.runtimeAnimatorController = WhiteRun;

		}
		else if(!Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.W))
		{
			if(VelY <0)
			{
				VelY +=0.2f;
				YVelRise = true;

			}
			else if(VelY < 1)
			{
				VelY +=0.1f;
				YVelRise = true;

			}
			Anim.runtimeAnimatorController = Run;
			WhiteAnim.runtimeAnimatorController = WhiteRun;

		}
		else
		{
			if(VelY <0)
			{
				VelY +=0.2f;
				YVelRise = false;

			}
			else if(VelY > 0)
			{
				VelY -=0.2f;
				YVelRise = false;

			}
			if(Mathf.Abs(VelY) < 0.2)
			{
				VelY = 0;
				YVelRise = false;

			}
		}

		RB.velocity += new Vector3(VelX,0);

		RB.velocity += new Vector3(0,VelY);

		RB.velocity = RB.velocity.normalized * Speed;
		if((!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D)
			|| Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D)
			|| !Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)
		))//not moving
		{
			Anim.runtimeAnimatorController = Idle;
			WhiteAnim.runtimeAnimatorController = WhiteIdle;

		}

		Shadow.transform.localPosition = new Vector3 (SR.flipX ? 0.0625f : -0.0625f, -1.8125f, 0);

		CMov.ManualUpdate ();
		inhousegradient--;
		ActiveShop = false;
		ActiveCornShop = false;
	}


	void OnTriggerStay(Collider other)
	{
		if (other.name == "EnterHouse") {
			Inhouse = other.transform;
			White.color = new Vector4 (0, 0, 0, (transform.position.y - other.transform.position.y)/2);
			inhousegradient = 2;

			if (Main.Data.DayComplete) {
				Whoosh.SetFloat ("_Pos", ((transform.position.y - other.transform.position.y) / 2) * (1.29f / 1.8f) + 0.5f);
			}
			if (White.color.a >= 1 && Main.Data.DayComplete) {

				Whoosh.SetFloat ("_Pos", 1.3f);

				Main.Data.DayComplete = false;
				sleepTimer = 0;
			
			}
		}

		if (other.name == "SHOPSHOP0") {
			ActiveShop = true;
			ShopButt.ShopID = 0;
		}
		else if (other.name == "SHOPSHOP1") {
			ActiveShop = true;
			ShopButt.ShopID = 1;
		}
		else if (other.name == "SHOPSHOP2") {
			ActiveShop = true;
			ShopButt.ShopID = 2;
		}
		else if (other.name == "SHOPSHOP3") {
			ActiveShop = true;
			ShopButt.ShopID = 3;
		}
		if (other.name == "SHOPSHOPCORN") {
			ActiveCornShop = true;
		}
	}


}
