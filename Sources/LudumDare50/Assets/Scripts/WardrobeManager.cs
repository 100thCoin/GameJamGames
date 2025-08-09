using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardrobeManager : MonoBehaviour {

	public WardrobeItem[] Items;

	public SpriteRenderer CharHead;
	public SpriteRenderer CharBody;

	public Sprite HairNice;
	public Sprite HairMess;
	public Sprite HairHat;
	public Sprite BodyNice;
	public Sprite BodyWitch;
	public Sprite BodyBag;

	public bool WitchHair;
	public bool HasHat;
	public bool WitchDress;
	public bool HasBag;

	public MoveChar Char;

	public TextMesh CountText;
	public float ShowError;

	public bool DEBUGGenWardrobe;
	public TextMesh Confirm;
	public bool ConfirmIsNotCancel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		ShowError-=Time.deltaTime;

		if(DEBUGGenWardrobe)
		{
			DEBUGGenWardrobe = false;
			GenerateRandomWardrobe();

		}

		// figure out the combo.

		if(WitchHair)
		{
			if(HasHat)
			{
				CharHead.sprite = HairHat;
			}
			else
			{
				CharHead.sprite = HairMess;
			}
		}
		else
		{
			HasHat = false;
			CharHead.sprite = HairNice;
		}

		if(WitchDress)
		{
			if(HasBag)
			{
				CharBody.sprite = BodyBag;

			}
			else
			{
				CharBody.sprite = BodyWitch;

			}

		}
		else
		{
			HasBag = false;
			CharBody.sprite = BodyNice;

		}


		//calculate total of each wardrobe

		int ManagerSuit =0;
		int WitchSuit =0;

		if(WitchHair){WitchSuit++;}
		else{ManagerSuit++;}
		if(HasHat){WitchSuit++;}
		if(WitchDress){WitchSuit++;}
		else{ManagerSuit++;}
		if(HasBag){WitchSuit++;}

		if(ShowError <0)
		{
			CountText.text = "Checklist:\n\nWitch Clothes:\n"+WitchSuit+"/4\n\nManager Suit:\n"+ManagerSuit+"/2";
		}

		if(Char.Witch)
		{
			Confirm.text =( ManagerSuit ==2) ? "Confirm\noutfit" : "Cancel";
			ConfirmIsNotCancel = (ManagerSuit ==2);
		}
		else
		{
			Confirm.text = ( WitchSuit ==4) ? "Confirm\noutfit" : "Cancel";
			ConfirmIsNotCancel = (WitchSuit ==4) ;

		}


	}

	public void GenerateRandomWardrobe()
	{
		// 0 - Nice Hair
		// 1 - Naughty Hair
		// 2 - Zonk Hair
		// 3 - Nice Suit
		// 4 - Naughty Suit
		// 5 - Zonk Suit
		// 6 - No Hat
		// 7 - Naughty Hat
		// 8 - Zonk Hat
		// 9 - No Bag
		// 10 - Bag

		int[] queue = new int[1];


		if(Char.Level == 0)
		{
			// 0/1 3/4 6/7 9/10
			// 4 items

			if(Char.Witch)
			{// already a witch. show normal clothes
				queue = new int[]{0,3,6,9};
			}
			else
			{
				queue = new int[]{1,4,7,10};
			}


		}
		else if(Char.Level == 1)
		{
			// level 2.
			// normal items plus zonk overalls or hat


			if(Char.Witch)
			{// already a witch. show normal clothes
				queue = new int[]{0,3,6,9,8};
			}
			else
			{
				queue = new int[]{1,4,7,10,5};
			}

		}
		else
		{
			// level 3.
			// all zonks


			if(Char.Witch)
			{// already a witch. show normal clothes
				queue = new int[]{0,3,6,9,8,5,2};
			}
			else
			{
				queue = new int[]{1,4,7,10,8,5,2};
			}

		}



		int i = 0;
		while(i < Items.Length)
		{
			Items[i].ObjectID = -1;
			i++;
		}
		i = 0;
		int r = 0;
		while(i < queue.Length)
		{
			r = Random.Range(0,Items.Length);
			while(Items[r].ObjectID != -1)
			{
				r++;
				if(r == Items.Length)
				{
					r=0;
				}
			}

			Items[r].ObjectID = queue[i];

			i++;
		}




	}


	public void DisplayError(string Message)
	{
		CountText.text = Message;
		ShowError = 2;

	}


}
