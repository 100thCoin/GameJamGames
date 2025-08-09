using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoMovement : MonoBehaviour {

	public GameObject Projectile;
	public GameObject Projectile2;
	public GameObject Projectile3;
	public GameObject Projectile4;
	public GameObject Projectile5;

	public GameObject ProjectileObject;

	public float MaxHp;
	public float Health;

	public float IntHP;

	public float DefaultMaxHp;
	public float musicDrain = 3000;
	public float MusicSpeed;
	public GameObject Music;

	public bool Run;
	public bool Idle;

	public int Direction;
	public string WhatDir;

	public int DoDirOnce;
	public bool DoStartRunOnce;
	public bool DoStartIdleOnce;

	public RuntimeAnimatorController RunFL;
	public RuntimeAnimatorController RunFR;
	public RuntimeAnimatorController RunBL;
	public RuntimeAnimatorController RunBR;

	public RuntimeAnimatorController IdleFL;
	public RuntimeAnimatorController IdleFR;
	public RuntimeAnimatorController IdleBL;
	public RuntimeAnimatorController IdleBR;

	public float speed;

	public Vector2 LastPos;

	public GameObject HPBar;
	public GameObject HPText;


	public float ItemType1;
	public float ItemType2;
	public float ItemType3;
	public float ItemType4;

	public float NewItemType;
	public float NewItemID;

	public float DefenseReduction;

	public float DamageBoost;

	public float DamageMultiplier;
	public float DamageMore;

	public float ManaDrainMult;
	public float ManaDrainReduce;
	public float ManaDrainMore;

	public bool HoldIt;
	public bool HoldBridge;

	public bool musicstop;

	public GameObject DEAD;
	public bool DieOnce;

	// Use this for initialization
	void Start () {

	}





	// Update is called once per frame
	void Update () {





		if (ItemType4 == 0) {DefenseReduction = 1;}
		if (ItemType4 == 1) {DefenseReduction = 0.7f;}
		if (ItemType4 == 2) {DefenseReduction = 0.5f;}
		if (ItemType4 == 3) {DefenseReduction = 0.3f;}

		if (ItemType3 == 0) {DamageMultiplier = 1;ManaDrainMult = 1;}
		if (ItemType3 == 1) {DamageMultiplier = 1;ManaDrainMult = 1;}
		if (ItemType3 == 2) {DamageMultiplier = 1.2f; ManaDrainMult = 0.4f;}
		if (ItemType3 == 3) {DamageMultiplier = 1.5f;ManaDrainMult = 0.8f;}
		if (ItemType3 == 4) {DamageMultiplier = 2.5f;ManaDrainMult = 2.5f;}
		if (ItemType3 == 5) {DamageMultiplier = 1.2f;ManaDrainMult = 1;}
		if (ItemType3 == 6) {DamageMultiplier = 2;ManaDrainMult = 1.5f;}

		if (ItemType1 == 1) {DamageBoost = 1;ManaDrainReduce = 0;}
		if (ItemType1 == 2) {DamageBoost = 2;ManaDrainReduce = -22;}
		if (ItemType1 == 3) {DamageBoost = 0.8f;ManaDrainReduce = -10;}
		if (ItemType1 == 4) {DamageBoost = 0.2f;ManaDrainReduce = 22;}
		if (ItemType1 == 5) {DamageBoost = 0.1f;ManaDrainReduce = 18;}

		if (ItemType2 == 1) {DamageMore = 1;ManaDrainMore = 1;MaxHp = 10000;}
		if (ItemType2 == 2) {DamageMore = 2;ManaDrainMore = 0.5f;MaxHp = 7500;}
		if (ItemType2 == 3) {DamageMore = 0.8f;ManaDrainMore = 0.25f;MaxHp = 15000;}


		if (ItemType3 != 4) {

			if (Health > MaxHp) {
				Health = MaxHp;
			}

		}

		HPBar.gameObject.GetComponent<HPBar> ().precent = Health / MaxHp * 100;

		HPText.gameObject.GetComponent<TextMesh>().text = "" + IntHP + "/" + MaxHp;

		Health = Health - Time.deltaTime * 10;
		IntHP = Mathf.Round (Health);


		if (Input.GetKeyDown (KeyCode.Mouse0) && ItemType1 <=3 && !musicstop) {

			Health = Health - (25 - ManaDrainReduce) * ManaDrainMult * ManaDrainMore;

			if (ItemType1 == 1) {
				ProjectileObject = Instantiate (Projectile, gameObject.transform.position, gameObject.transform.rotation, GameObject.Find ("Weapon").transform) as GameObject;
			}
			if (ItemType1 == 2) {
				ProjectileObject = Instantiate (Projectile2, gameObject.transform.position, gameObject.transform.rotation, GameObject.Find ("Weapon").transform) as GameObject;
			}
			if (ItemType1 == 3) {
				ProjectileObject = Instantiate (Projectile3, gameObject.transform.position, gameObject.transform.rotation, GameObject.Find ("Weapon").transform) as GameObject;
			}
			//if (ItemType1 == 4) {
			//	ProjectileObject = Instantiate (Projectile4, gameObject.transform.position, gameObject.transform.rotation, GameObject.Find ("Weapon").transform) as GameObject;
			//}
			//if (ItemType1 == 5) {
			//	ProjectileObject = Instantiate (Projectile5, gameObject.transform.position, gameObject.transform.rotation, GameObject.Find ("Weapon").transform) as GameObject;
			//}
			ProjectileObject.transform.localPosition = new Vector2 (0, 2);
			ProjectileObject.transform.localEulerAngles = new Vector3 (0, 0, 0);
			ProjectileObject.transform.parent = null;

		}
		if (Input.GetKey (KeyCode.Mouse0) && ItemType1 >=4 && !musicstop) {


			if (HoldIt && !HoldBridge) {
				HoldBridge = true;
				HoldIt = false;
			}

			if (!HoldIt && !HoldBridge) {
				Health = Health - (25 - ManaDrainReduce) * ManaDrainMult * ManaDrainMore;

				//if (ItemType1 == 1) {
				//	ProjectileObject = Instantiate (Projectile, gameObject.transform.position, gameObject.transform.rotation, GameObject.Find ("Weapon").transform) as GameObject;
				//}
				//if (ItemType1 == 2) {
				//	ProjectileObject = Instantiate (Projectile2, gameObject.transform.position, gameObject.transform.rotation, GameObject.Find ("Weapon").transform) as GameObject;
				//}
				//if (ItemType1 == 3) {
				//	ProjectileObject = Instantiate (Projectile3, gameObject.transform.position, gameObject.transform.rotation, GameObject.Find ("Weapon").transform) as GameObject;
				//}
				if (ItemType1 == 4) {
					ProjectileObject = Instantiate (Projectile4, gameObject.transform.position, gameObject.transform.rotation, GameObject.Find ("Weapon").transform) as GameObject;
				}
				if (ItemType1 == 5) {
					ProjectileObject = Instantiate (Projectile5, gameObject.transform.position, gameObject.transform.rotation, GameObject.Find ("Weapon").transform) as GameObject;
				}
				ProjectileObject.transform.localPosition = new Vector2 (0, 2);
				ProjectileObject.transform.localEulerAngles = new Vector3 (0, 0, 0);
				ProjectileObject.transform.parent = null;
				HoldIt = true;
			}

			if (!HoldIt && HoldBridge) {
				HoldBridge = false;

			}


		}

		if (musicstop) {
			gameObject.GetComponent<Animator> ().runtimeAnimatorController = null;
			MusicSpeed = MusicSpeed - Time.deltaTime * 0.5f;
			if (MusicSpeed < 0) {
				MusicSpeed = 0;
			}

		}

		if (Health < 0) {
			musicstop = true;
			Health = 0;
			if (!DieOnce) {
				DieOnce = true;
				StartCoroutine (DED ());
			}


		}

		if (Health > 0) {
			DieOnce = false;

			musicstop = false;
		}
		if (Health < musicDrain && Health > 0) 
		{
			MusicSpeed = (4000 + Health*2) * 0.0001f;
		}

		if(Health >=3000 && Health <= 10000)
		{
			MusicSpeed = 1;
		}

		if (Health > 10000) 
		{
			MusicSpeed = (Health) * 0.0001f;
		}
		speed = MusicSpeed * 60;
		Music.gameObject.GetComponent<AudioSource> ().pitch = MusicSpeed;
		//End music junk

		if ((Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.D)) ||
		   (Input.GetKey (KeyCode.D) && !Input.GetKey (KeyCode.A)) ||
		   (Input.GetKey (KeyCode.W) && !Input.GetKey (KeyCode.S)) ||
		   (Input.GetKey (KeyCode.S) && !Input.GetKey (KeyCode.W))) {
			Run = true;
			Idle = false;

		}

		if((Input.GetKey (KeyCode.A) && Input.GetKey (KeyCode.D) && 
			!((Input.GetKey (KeyCode.W) && !Input.GetKey (KeyCode.S)) 
			||(Input.GetKey (KeyCode.S) && !Input.GetKey (KeyCode.W)))) ||
			(Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.S) &&
			!((Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.D)) 
			||(Input.GetKey (KeyCode.D) && !Input.GetKey (KeyCode.A)))) ||
			(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W))
		)
			{
				Run = false;
				Idle = true;
			}


		if(Run && !musicstop)
		{
			if (!DoStartRunOnce) 
			{
				DoStartRunOnce = true;
				DoStartIdleOnce = false;
				DoDirOnce = 0;
			}
			if (Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.D) && Direction == 3) {Direction = 4;}
			if (Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.D) && Direction == 2) {Direction = 1;}
			if (Input.GetKey (KeyCode.S) && !Input.GetKey (KeyCode.W) && Direction == 4) {Direction = 1;}
			if (Input.GetKey (KeyCode.S) && !Input.GetKey (KeyCode.W) && Direction == 3) {Direction = 2;}
			if (Input.GetKey (KeyCode.S) && !Input.GetKey (KeyCode.W) && Direction == 4) {Direction = 1;}
			if (Input.GetKey (KeyCode.W) && !Input.GetKey (KeyCode.S) && Direction == 1) {Direction = 4;}
			if (Input.GetKey (KeyCode.W) && !Input.GetKey (KeyCode.S) && Direction == 2) {Direction = 3;}
			if (Input.GetKey (KeyCode.D) && !Input.GetKey (KeyCode.A) && Direction == 1) {Direction = 2;}
			if (Input.GetKey (KeyCode.D) && !Input.GetKey (KeyCode.A) && Direction == 4) {Direction = 3;}

			if (Direction == 1 && DoDirOnce != 1) {DoDirOnce = 1; gameObject.GetComponent<Animator> ().runtimeAnimatorController = RunFL;}
			if (Direction == 2 && DoDirOnce != 2) {DoDirOnce = 2; gameObject.GetComponent<Animator> ().runtimeAnimatorController = RunFR;}
			if (Direction == 3 && DoDirOnce != 3) {DoDirOnce = 3; gameObject.GetComponent<Animator> ().runtimeAnimatorController = RunBR;}
			if (Direction == 4 && DoDirOnce != 4) {DoDirOnce = 4; gameObject.GetComponent<Animator> ().runtimeAnimatorController = RunBL;}



		}

		if(Idle && !musicstop)
		{
			if (!DoStartIdleOnce) 
			{
				DoStartRunOnce = false;
				DoStartIdleOnce = true;
				DoDirOnce = 0;
			}

			if (Direction == 1 && DoDirOnce != 1) {DoDirOnce = 1; gameObject.GetComponent<Animator> ().runtimeAnimatorController = IdleFL;}
			if (Direction == 2 && DoDirOnce != 2) {DoDirOnce = 2; gameObject.GetComponent<Animator> ().runtimeAnimatorController = IdleFR;}
			if (Direction == 3 && DoDirOnce != 3) {DoDirOnce = 3; gameObject.GetComponent<Animator> ().runtimeAnimatorController = IdleBR;}
			if (Direction == 4 && DoDirOnce != 4) {DoDirOnce = 4; gameObject.GetComponent<Animator> ().runtimeAnimatorController = IdleBL;}

		}

		if (Direction == 1) {WhatDir = "Front Left";}
		if (Direction == 2) {WhatDir = "Front Right";}
		if (Direction == 3) {WhatDir = "Back Right";}
		if (Direction == 4) {WhatDir = "Back Left";}






	}



	void FixedUpdate()
	{
		gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (gameObject.GetComponent<Rigidbody> ().velocity.x * 0.5f, gameObject.GetComponent<Rigidbody> ().velocity.y * 0.5f);

		if (Run) {

	
			LastPos = gameObject.transform.position;

//			if (Input.GetKey (KeyCode.A)&& !Input.GetKey (KeyCode.W) && !Input.GetKey (KeyCode.S)) {gameObject.transform.position = new Vector3 (gameObject.transform.position.x - speed * 0.3f, gameObject.transform.position.y, 0);}
//			if (Input.GetKey (KeyCode.A)&& Input.GetKey (KeyCode.W) && !Input.GetKey (KeyCode.S)) {gameObject.transform.position = new Vector3 (gameObject.transform.position.x - speed * 0.15f, gameObject.transform.position.y + speed * 0.15f, 0);}
//			if (Input.GetKey (KeyCode.A)&& !Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.S)) {gameObject.transform.position = new Vector3 (gameObject.transform.position.x - speed * 0.15f, gameObject.transform.position.y - speed * 0.15f, 0);}
//
//			if (Input.GetKey (KeyCode.D)&& !Input.GetKey (KeyCode.W) && !Input.GetKey (KeyCode.S)) {gameObject.transform.position = new Vector3 (gameObject.transform.position.x + speed * 0.3f, gameObject.transform.position.y, 0);}
//			if (Input.GetKey (KeyCode.D)&& Input.GetKey (KeyCode.W) && !Input.GetKey (KeyCode.S)) {gameObject.transform.position = new Vector3 (gameObject.transform.position.x + speed * 0.15f, gameObject.transform.position.y + speed * 0.15f, 0);}
//			if (Input.GetKey (KeyCode.D)&& !Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.S)) {gameObject.transform.position = new Vector3 (gameObject.transform.position.x + speed * 0.15f, gameObject.transform.position.y - speed * 0.15f, 0);}
//		
//			if (Input.GetKey (KeyCode.W)&& !Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.D)) {gameObject.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y + speed * 0.3f, 0);}
//			if (Input.GetKey (KeyCode.W)&& Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.D)) {gameObject.transform.position = new Vector3 (gameObject.transform.position.x - speed * 0.15f, gameObject.transform.position.y + speed * 0.15f, 0);}
//			if (Input.GetKey (KeyCode.W)&& !Input.GetKey (KeyCode.A) && Input.GetKey (KeyCode.D)) {gameObject.transform.position = new Vector3 (gameObject.transform.position.x + speed * 0.15f, gameObject.transform.position.y + speed * 0.15f, 0);}
//
//			if (Input.GetKey (KeyCode.S)&& !Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.D)) {gameObject.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y - speed * 0.3f, 0);}
//			if (Input.GetKey (KeyCode.S)&& Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.D)) {gameObject.transform.position = new Vector3 (gameObject.transform.position.x - speed * 0.15f, gameObject.transform.position.y - speed * 0.15f, 0);}
//			if (Input.GetKey (KeyCode.S)&& !Input.GetKey (KeyCode.A) && Input.GetKey (KeyCode.D)) {gameObject.transform.position = new Vector3 (gameObject.transform.position.x + speed * 0.15f, gameObject.transform.position.y - speed * 0.15f, 0);}
//
	
			if (Input.GetKey (KeyCode.A)&& !Input.GetKey (KeyCode.W) && !Input.GetKey (KeyCode.S)) {gameObject.GetComponent<Rigidbody>().velocity = new Vector3 (-speed * 0.3f, gameObject.GetComponent<Rigidbody>().velocity.y, 0);}
			if (Input.GetKey (KeyCode.A)&& Input.GetKey (KeyCode.W) && !Input.GetKey (KeyCode.S)) {gameObject.GetComponent<Rigidbody>().velocity = new Vector3 (-speed * 0.2f, speed * 0.2f, 0);}
			if (Input.GetKey (KeyCode.A)&& !Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.S)) {gameObject.GetComponent<Rigidbody>().velocity = new Vector3 (-speed * 0.2f, -speed * 0.2f, 0);}
			
			if (Input.GetKey (KeyCode.D)&& !Input.GetKey (KeyCode.W) && !Input.GetKey (KeyCode.S)) {gameObject.GetComponent<Rigidbody>().velocity = new Vector3 (speed * 0.3f, gameObject.GetComponent<Rigidbody>().velocity.y, 0);}
			if (Input.GetKey (KeyCode.D)&& Input.GetKey (KeyCode.W) && !Input.GetKey (KeyCode.S)) {gameObject.GetComponent<Rigidbody>().velocity = new Vector3 (speed * 0.2f, speed * 0.2f, 0);}
			if (Input.GetKey (KeyCode.D)&& !Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.S)) {gameObject.GetComponent<Rigidbody>().velocity = new Vector3 (speed * 0.2f, -speed * 0.2f, 0);}
					
			if (Input.GetKey (KeyCode.W)&& !Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.D)) {gameObject.GetComponent<Rigidbody>().velocity = new Vector3 (gameObject.GetComponent<Rigidbody>().velocity.x, speed * 0.3f, 0);}
			if (Input.GetKey (KeyCode.W)&& Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.D)) {gameObject.GetComponent<Rigidbody>().velocity = new Vector3 (-speed * 0.2f,  speed * 0.2f, 0);}
			if (Input.GetKey (KeyCode.W)&& !Input.GetKey (KeyCode.A) && Input.GetKey (KeyCode.D)) {gameObject.GetComponent<Rigidbody>().velocity = new Vector3 ( speed * 0.2f,  speed * 0.2f, 0);}
			
			if (Input.GetKey (KeyCode.S)&& !Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.D)) {gameObject.GetComponent<Rigidbody>().velocity = new Vector3 (gameObject.GetComponent<Rigidbody>().velocity.x, -speed * 0.3f, 0);}
			if (Input.GetKey (KeyCode.S)&& Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.D)) {gameObject.GetComponent<Rigidbody>().velocity = new Vector3 (-speed * 0.2f, -speed * 0.2f, 0);}
			if (Input.GetKey (KeyCode.S)&& !Input.GetKey (KeyCode.A) && Input.GetKey (KeyCode.D)) {gameObject.GetComponent<Rigidbody>().velocity = new Vector3 ( speed * 0.2f, -speed * 0.2f, 0);}
			




		}


	}


	void OnTriggerStay(Collider other)
	{

//		if (other.gameObject.tag == "Wall") {
//
//			gameObject.transform.position = LastPos;
//
//
//
//		}

		if (other.gameObject.tag == "PowerGrid" && other.gameObject.name == "GridOn") {

			Health = Health + Time.deltaTime * 110;



		}		
		if (other.gameObject.tag == "Instadeath") {

			Health = Health =0;



		}
		if (other.gameObject.tag == "Damage") {

			if (other.gameObject.name == "Crawlb") {

				Health = Health - 10 * DefenseReduction / (GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Level * 0.1f);

			}
			if (other.gameObject.name == "Iron") {

				Health = Health - 150 * DefenseReduction / (GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Level * 0.1f);

			}
			if (other.gameObject.name == "Iron") {

				Health = Health - 50 * DefenseReduction / (GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Level * 0.1f);

			}

			if (other.gameObject.name == "Outlet") {

				Health = Health - 10 * DefenseReduction / (GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Level * 0.1f);

			}
			if (other.gameObject.name == "EnemyZap") {

				Health = Health - 100 * DefenseReduction / (GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Level * 0.1f);

			}
			if (other.gameObject.name == "EnemyZap2") {

				Health = Health - 200 * DefenseReduction / (GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Level * 0.1f);

			}
			if (other.gameObject.name == "Lamp") {

				Health = Health - 100 * DefenseReduction / (GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Level * 0.1f);

			}
			if (other.gameObject.name == "Phone") {

				Health = Health - 50 * DefenseReduction / (GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Level * 0.1f);

			}
		}
	}

	public IEnumerator PickUp()
	{
		yield return new WaitForEndOfFrame ();

		if (NewItemType == 1) {
			GameObject.Find ("ActiveObject").gameObject.GetComponent<Item> ().ID = ItemType1;
			GameObject.Find ("ActiveObject").gameObject.name = "Item";

			ItemType1 = NewItemID;
		}
		if (NewItemType == 2) {
			GameObject.Find ("ActiveObject").gameObject.GetComponent<Item> ().ID = ItemType2;
			GameObject.Find ("ActiveObject").gameObject.name = "Item";

			ItemType2 = NewItemID;
		}
		if (NewItemType == 3) {
			GameObject.Find ("ActiveObject").gameObject.GetComponent<Item> ().ID = ItemType3;
			GameObject.Find ("ActiveObject").gameObject.name = "Item";

			ItemType3 = NewItemID;

		}
		if (NewItemType == 4) {
			GameObject.Find ("ActiveObject").gameObject.GetComponent<Item> ().ID = ItemType4;
			GameObject.Find ("ActiveObject").gameObject.name = "Item";

			ItemType4 = NewItemID;
		}


	}



	void LateUpdate()
	{
		if (Health < 0) {
			musicstop = true;
			Health = 0;
		}

	}

	IEnumerator DED()
	{
		yield return new WaitForSeconds(2);
		if (Health == 0) {
			Instantiate (DEAD,new Vector3( GameObject.Find("MoveCamera").gameObject.transform.position.x, GameObject.Find("MoveCamera").gameObject.transform.position.y,-4),gameObject.transform.rotation);
		}

	}


}
