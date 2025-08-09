using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmSeedButton : MonoBehaviour {

	public SpriteRenderer Glow;

	public Sprite Edge;
	public Sprite Highlight;

	public bool Selected;

	public Farm F;
	public int SeedID;

	public bool MercyFrame;

	public bool NoSeed;

	public int EmptyTiles;

	public DataHolder Main;

	public bool Unlocked;

	public SpriteRenderer SR;

	public GameObject PinkSeed;

	public SpriteRenderer OOS;
	public float OOSTimer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(OOSTimer > 0)
		{
			OOSTimer-= Time.deltaTime;

			OOS.enabled = true;
			if(OOSTimer <=0 )
			{
				OOS.enabled = false;
			}
		}


		Unlocked = false;

		Unlocked = (SeedID == 0 && Main.UnlockedRed);
		Unlocked = Unlocked || (SeedID == 1);
		Unlocked = Unlocked || (SeedID == 2 && Main.UnlockedYellow);
		Unlocked = Unlocked || (SeedID == 3 && Main.UnlockedGreen);
		Unlocked = Unlocked || (SeedID == 4);
		Unlocked = Unlocked || (SeedID == 5 && Main.UnlockedPurple);
		Unlocked = Unlocked || (SeedID == 6 && Main.UnlockedWhite);
		Unlocked = Unlocked || (SeedID == 7 && Main.UnlockedPink);


		if(Unlocked)
		{
			if(SeedID == 5 && Main.UnlockedPink)
			{
				PinkSeed.SetActive(true);
				gameObject.SetActive(false);
			}


			SR.enabled = true;
		}
		else
		{
			SR.enabled = false;
		}




		Selected = F.SeedID == SeedID;

		if(!MercyFrame && !Selected)
		{
			Glow.enabled = false;
		}
		MercyFrame = false;
		if(!Selected)
		{
			Glow.sprite = Edge;

		}

	}

	void OnMouseOver()
	{

		if(!Selected && F.SeedStock >0 && Unlocked)
		{
			Glow.enabled = true;
			MercyFrame = true;

			if(Input.GetKeyDown(KeyCode.Mouse0))
			{
				F.SelectedSeed = !NoSeed;
				F.SeedID = SeedID;
				Glow.sprite = Highlight;
				F.ChangeSeedVisual(SeedID);
			}
		}
		else if(F.SeedStock <=0)
		{
			if(Input.GetKeyDown(KeyCode.Mouse0))
			{
				OOSTimer = 2;
			}
		}
	}

}
