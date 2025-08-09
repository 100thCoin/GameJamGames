using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulb : MonoBehaviour {

	public SpriteRenderer Bug;
	public SpriteRenderer SR;

	public bool bloom;
	public bool Bugged;
	public int ID;

	public Sprite[] Blooms;
	public RuntimeAnimatorController[] Bugs;

	public float Timer;

	public GameObject Player;

	public DataHolder Main;
	public EnterCombat EC;

	public bool Mercy;

	public SpriteRenderer Highlight;

	public bool DoOnce;

	public GameObject[] Projectiles;

	public float BLinkTimer;

	public GameObject UprootText;

	public BulbPlot Location;

	public Farm F;

	// Use this for initialization
	void Start () {
		Main = GameObject.Find("Main").GetComponent<DataHolder>();
		EC = Main.EC;
		Timer = Random.Range(3f,6f);
		Player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {

		if(ID == 3 && (Main.Progress == 3.3f || Main.Progress == 3.33f || Main.Progress == 3.34f))
		{
			Main.Progress = 3.3451f;

		}

		if(ID == 2 && (Main.Progress == 3))
		{
			Main.Progress = 3.05f;

		}


		if(ID != 15 && ID != 5)
		{
			Timer -= Time.deltaTime;
		}
		if(Timer < 0 && !bloom)
		{

			bloom = true;
			SR.sprite = Blooms[ID];
			GameObject P = Instantiate(Projectiles[ID],transform.position - new Vector3(0,0,1),transform.rotation,transform);

		}

		float XDist = Mathf.Abs(transform.position.x - Player.transform.position.x);

		if(XDist > 14.5f && bloom)
		{
			if(GameObject.Find("Main").GetComponent<DataHolder>().Progress > 3)
			{
				if(ID != 3 && ID !=8)
				{
					Bugged = true;
				}
			}
		}

		if(XDist > 14.5f && Main.Progress == 3.05f)
		{
			if(GameObject.Find("Main").GetComponent<DataHolder>().Progress == 3.05f)
			{
				Main.WL.DEBUGREAD(Main.WL.GetComponent<DialogueHolder>().WaitUpMeetMiggs);
				Player.transform.position -= new Vector3(0.1f,0,0);
				if(ID != 3 && ID !=8)
				{
					Bugged = true;
				}
			}

		}

		if(Bugged)
		{
			Bug.gameObject.SetActive(true);
			Bug.enabled = true;
			Bug.GetComponent<Animator>().runtimeAnimatorController = Bugs[ID];
		}

		if(!Mercy)
		{

			Highlight.enabled = false;


		}
		Mercy = false;
		SR.enabled = true;

	}





	void OnMouseOver()
	{
		if(Bugged)
		{
			Mercy = true;
			Highlight.enabled = true;

			if(Input.GetKeyDown(KeyCode.Mouse0) &&!DoOnce && !Main.NoMoving)
			{

				DoOnce = true;
				EC.BugFlowerID = ID;
				EC.enabled = true;
				EC.gameObject.SetActive(true);
				EC.Bug.sprite = Bug.sprite;

				EC.AB.Win = false;
				EC.AB.NoteWin.transform.localScale = new Vector3(0,0,0);
				EC.AB.Leavespeed = 0;
				EC.AB.Opponant.transform.localPosition = new Vector3(0,0.6f,-1);
				EC.AB.Opponant.GetComponent<Animator>().runtimeAnimatorController = Bug.GetComponent<Animator>().runtimeAnimatorController;
				Location.Filled = false;
				Location.FilledOnce = false;

				EC.AB.PlayerHP = 3;
				EC.AB.OpponantHP = 2;
				if(ID == 3 || ID == 13)
				{
					EC.AB.OpponantHP = 4;
				}
				if(ID > 10) //not including 10
				{
					EC.AB.OpponantHP++;

				}

				Main.SeedStock = F.SeedStock;

				if(Main.SeedStock < 2)
				{
					Main.SeedStock = 2;
				}


				Destroy(gameObject);
			}

		}
		else if(bloom)
		{
			BLinkTimer += Time.deltaTime;
			if(BLinkTimer > 0.2f)
			{
				SR.enabled = false;

				if(BLinkTimer > 0.4f)
				{
					BLinkTimer -= 0.4f;
				}

			}
			else
			{
				SR.enabled = true;

			}

			if(Input.GetKeyDown(KeyCode.Mouse0) && Main.Progress != 3.05f)
			{

				GameObject Uproot = Instantiate(UprootText,transform.position + new Vector3(0,1,0),transform.rotation,transform.parent);
				Location.Filled = false;
				Location.FilledOnce = false;

				int bonus = 3;

				if(ID == 0){bonus = 15;Main.UnlockedRed = true;}
				if(ID == 2){bonus = 4;Main.UnlockedYellow = true;}
				if(ID == 3){bonus = 12;Main.UnlockedGreen = true;}
				if(ID == 6){bonus = 2;Main.UnlockedWhite = true;}

				if(ID > 10){bonus += 5;}


				Uproot.GetComponent<SeedBonus>().howmany = bonus;

				F.SeedStock += bonus;

				if(GameObject.Find("Main").GetComponent<DataHolder>().Progress == 1.2f)
				{
					GameObject.Find("Main").GetComponent<DataHolder>().Progress = 1.3f;
					Main.WL.DEBUGREAD(Main.WL.GetComponent<DialogueHolder>().TildeHarvestMakeYellow);

				}

				if(GameObject.Find("Main").GetComponent<DataHolder>().Progress == 3.7f || GameObject.Find("Main").GetComponent<DataHolder>().Progress == 3.71f)
				{
					GameObject.Find("Main").GetComponent<DataHolder>().Progress = 3.8f;
					Main.UnlockedPurple = true;
				}

				Destroy(gameObject);


			}
			else if(Input.GetKeyDown(KeyCode.Mouse0) && Main.Progress == 3.05f)
			{
				Main.WL.DEBUGREAD(Main.WL.GetComponent<DialogueHolder>().Monoshowtilde);

			}

		}
	}

}
