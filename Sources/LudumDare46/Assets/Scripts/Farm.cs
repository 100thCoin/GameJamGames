using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour {

	public bool InFarm;

	public int SeedStock;

	public SpriteRenderer SR;
	public Sprite Highlight;
	public Sprite Dark;

	public bool MercyFrame;
	public bool InvisMercyFrame;

	public DataHolder Main;

	public GameObject Player;
	public float XDist;

	public bool SelectedSeed;
	public int SeedID;
	public bool FirstSeed;

	public GameObject[] SeedVisuals;
	public GameObject[] Seeds;

	public GameObject PSeedHolder;
	public GameObject SeedHolder;

	public int[] TypeCount;

	public int OpenSlots;

	public bool PlacingBulb;

	public GameObject[] BulbVis;
	public GameObject[] Bulbs;

	public int BulbID;


	public bool CombatFarm;

	public GameObject BulbHolder;

	public AttackButton AttackB;

	public bool invisMercy2;

	// Use this for initialization
	void Start () {
	}

	void OnEnable()
	{
		Main = GameObject.Find("Main").GetComponent<DataHolder>();

		SeedStock = Main.SeedStock;
	}
	
	// Update is called once per frame
	void Update () {

		if(!CombatFarm)
		{
		if(PlacingBulb)
		{
			InFarm = false;
			MercyFrame = false;
		}

		if(!MercyFrame)
		{
			SR.sprite = Dark;
		}
		MercyFrame = false;
		XDist = Mathf.Abs(transform.position.x - Player.transform.position.x);
		}
		if(InFarm && !CombatFarm)
		{
			if(XDist > 9)
			{
				InFarm = false;
				Main.ExitFarm = true;
			}
		}

		if(!invisMercy2)
		{
			InvisMercyFrame = false;
		}
		invisMercy2 = false;


	}


	void OnMouseOver()
	{
		InvisMercyFrame = true;
		invisMercy2 = true;
		if(!CombatFarm)
		{
		if(!InFarm)
		{
			SR.sprite = Highlight;
			MercyFrame = true;

			if(Input.GetKeyDown(KeyCode.Mouse0) && XDist < 9)
			{
				InFarm = true;
				Main.EnterFarm = true;

			}
		}
		else
		{
			if(Input.GetKeyDown(KeyCode.Mouse0))
			{
				if(FirstSeed)
				{
					if(SeedID != -1)
					{

						PlantSeed(SeedID,null);


					}
				}
			}

		}
		}
		else
		{
			if(Input.GetKeyDown(KeyCode.Mouse0))
			{
				if(FirstSeed)
				{
					if(SeedID != -1)
					{

						PlantSeed(SeedID,null);


					}
				}
			}
		}
	}


	public void ChangeSeedVisual(int NewSeed)
	{
		if(PSeedHolder.transform.childCount > 0)
		{
			Destroy(PSeedHolder.transform.GetChild(0).gameObject);
		}
		if(NewSeed != -1)
		{
			GameObject Svis = Instantiate(SeedVisuals[NewSeed],PSeedHolder.transform.position,transform.rotation,PSeedHolder.transform);
			Svis.name = "seed";
		}
	}



	public void PlantSeed(int ID, Transform Target)
	{

		if(Target == null)
		{
			Target = SeedHolder.transform;

			GameObject s = Instantiate(Seeds[ID],PSeedHolder.transform.position + new Vector3(0,0,0.2f),transform.rotation,Target);
			s.name = "" + Mathf.Round(s.transform.position.x) + "," + Mathf.Round(s.transform.position.y);

			FirstSeed = false;
		}
		else
		{

			GameObject s = Instantiate(Seeds[ID],new Vector3(Target.transform.position.x,Target.transform.position.y,Target.transform.parent.position.z),transform.rotation,SeedHolder.transform);
			s.name = "" + Mathf.Round(s.transform.position.x) + "," + Mathf.Round(s.transform.position.y);

			FirstSeed = false;
		}


		SeedID = -1;
		SelectedSeed = false;
		ChangeSeedVisual(-1);
		SeedStock--;
		TypeCount[ID]++;
	}


	public void PlantBulb(int ID, Transform Target)
	{

		GameObject s = Instantiate(Bulbs[ID],new Vector3(Target.transform.position.x,Target.transform.position.y,Target.transform.parent.position.z-1),transform.rotation,Target.transform.parent);
		//s.name = "" + Mathf.Round(s.transform.position.x) + "," + Mathf.Round(s.transform.position.y);

		s.GetComponent<Bulb>().Location = Target.GetComponent<BulbPlot>();
		s.GetComponent<Bulb>().F = this;
		if(CombatFarm)
		{
			s.GetComponent<CombatFlower>().Main = AttackB;

		}

		PlacingBulb = false;


		BulbID = -1;
		SelectedSeed = false;
		ChangeSeedVisual(-1);
	}


	public void PlantCombatBulb(int ID, Transform Target,int Location)
	{
		print(ID);
		GameObject s = Instantiate(Bulbs[ID],new Vector3(Target.transform.position.x,Target.transform.position.y + 0.5f,Target.transform.parent.position.z-1),transform.rotation,Target.transform);
		//s.name = "" + Mathf.Round(s.transform.position.x) + "," + Mathf.Round(s.transform.position.y);

		if(CombatFarm)
		{
			s.GetComponent<CombatFlower>().Main = AttackB;
			s.GetComponent<CombatFlower>().F = this;
			s.GetComponent<CombatFlower>().DH = Main;

		}

		PlacingBulb = false;
		AttackB.FlowerList[Location] = s.GetComponent<CombatFlower>();


		BulbID = -1;
		SelectedSeed = false;
		ChangeSeedVisual(-1);
	}



	public void ReturnSeeds()
	{
		int SeedCount = SeedHolder.transform.childCount;
		int i = 0;
		while(i < SeedCount)
		{
			Destroy(SeedHolder.transform.GetChild(i).gameObject);
			i++;
		}

		SeedStock += SeedCount;
		FirstSeed = true;
		OpenSlots = 0;

		i = 0;
		while(i<TypeCount.Length)
		{
			TypeCount[i] = 0;
			i++;
		}

	}


}
