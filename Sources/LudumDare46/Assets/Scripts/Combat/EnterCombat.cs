using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterCombat : MonoBehaviour {

	public SpriteRenderer WhiteFlash;
	public SpriteRenderer Bug;

	public GameObject Player;
	public GameObject Opponant;

	public float Timer;
	public float Timer2;
	public float Timer3;
	public float Timer4;

	public GameObject Combat;

	public GameObject World;

	public GameObject BugFlower;

	public GameObject[] BugFlowers;
	public int BugFlowerID;
	public AttackButton AB;

	public DataHolder Main;

	public bool FirstCombat;
	public bool MiggsDeath;

	public bool Tilde;

	float Height;
	void OnEnable()
	{
		Height = GameObject.Find("Main Camera").transform.position.y;
		Timer = 0;
		Timer2 = 0;
		Timer3 = 0;
		Timer4 = 0.5f;

	}


	void Update () {

		if(Timer < 0.11f)
		{
			Timer += Time.deltaTime;
			if(Timer > 0.11f)
			{
				Timer = 0.11f;
			}

			Player.transform.localPosition = new Vector3((Timer - 0.11f)*100,0,-5);
			Opponant.transform.localPosition = new Vector3(-(Timer - 0.11f)*100,0,-5);

		}
		else if(Timer2 < 1)
		{
			Timer2 += Time.deltaTime;

			WhiteFlash.color = new Vector4(1,1,1,1-Timer2*3);
		}
		else if(Timer3 < 0.33f)
		{
			Timer3 += Time.deltaTime;
			if(Timer3 >= 0.33f)
			{
				Combat.SetActive(true);
				World.SetActive(false);

				if(!MiggsDeath)
				{
				if(BugFlower.transform.childCount > 0)
				{
					Destroy(BugFlower.transform.GetChild(0).gameObject);

				}
				GameObject BF = Instantiate(BugFlowers[BugFlowerID],BugFlower.transform.position + new Vector3(0,0.5f,-1),transform.rotation,BugFlower.transform);
				CombatFlower CF = BF.GetComponent<CombatFlower>();
					CF.F = AB.F;
					CF.DH = Main;
					CF.Enemy = true;
					CF.Main = AB;
					AB.EnemyFlowerList[0] = CF;
					AB.Farm2.gameObject.SetActive(true);
					AB.Win = false;
					AB.WinOnce = false;
					AB.OpponantHP = 2;
					AB.NoteWin.SetActive(false);

				}
			}
		}
		else if(Timer4 > 0)
		{
			Timer4 -= Time.deltaTime;

			float LocalYPos = (-(((16- (0)) * Mathf.Pow(Timer4,2))/Mathf.Pow(0.5f,2) - ((2*16 - 2*(-0)) * Timer4)/0.5f)-16)*1.5f;
			float LocalYPosO = ((((16 - (0)) * Mathf.Pow(Timer4,2))/Mathf.Pow(0.5f,2) - ((2*16 - 2*(0)) * Timer4)/0.5f)+16)*1.5f;

			Player.transform.localPosition  = new Vector3(LocalYPos,0,-5);
			Opponant.transform.localPosition  = new Vector3(LocalYPosO,0,-5);


			if(Timer4 <= 0)
			{
				if(!FirstCombat)
				{
					FirstCombat = true;
					if(!MiggsDeath && !Tilde)
					{
						Main.WL.DEBUGREAD(Main.WL.GetComponent<DialogueHolder>().MiggCombat);
					}
					else if(!Tilde)
					{
						Main.WL.DEBUGREAD(Main.WL.GetComponent<DialogueHolder>().TilfeFightMiggs);
					}
				}

			}

		}

	}
}
