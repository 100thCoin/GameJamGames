using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardrobeItem : MonoBehaviour {

	public int ObjectID;
	public SpriteRenderer SR;
	public Sprite[] Items;
	public WardrobeManager WardMan;
	public bool geprankrilfools;
	public bool geprankrilfoolsthesecond;
	public bool geprankrilfoolsthethird;

	// Update is called once per frame
	void Update () {
		if(geprankrilfools || geprankrilfoolsthesecond || geprankrilfoolsthethird)
		{
			return;
		}

		if(ObjectID != -1)
		{
			SR.sprite = Items[ObjectID];
		}
		else
		{
			SR.sprite = null;

		}
	}


	void OnMouseOver()
	{
		if(Input.GetKeyDown(KeyCode.Mouse0))
		{

			if(geprankrilfools)
			{
				// got em.
				// it's not an item at all! It's the "confirm" button.
				// you sure fell for that one you dingus.
				if(WardMan.ConfirmIsNotCancel)
				{
					WardMan.Char.Witch = !WardMan.Char.Witch;
					WardMan.Char.Anim.runtimeAnimatorController = WardMan.Char.Witch ? WardMan.Char.WitchIdle : WardMan.Char.ManagerIdle;
				}
				else
				{
					//return the manager stuff to how it should be
					if(WardMan.Char.Witch)
					{
						WardMan.HasBag = true;
						WardMan.WitchDress = true;
						WardMan.WitchHair = true;
						WardMan.HasHat = true;

					}
					else
					{
						WardMan.HasBag = false;
						WardMan.WitchDress = false;
						WardMan.WitchHair = false;
						WardMan.HasHat = false;
					}

				}

				WardMan.Char.Cam.transform.position = new Vector3(11,214,-50);
				WardMan.Char.InWardrobe = false;
				return;
			}
			else if(geprankrilfoolsthesecond)
			{

				WardMan.Char.AtCheckout = false;

			}
			else if(geprankrilfoolsthethird)
			{

				WardMan.Char.PriceGun.Scan();

			}


			if(ObjectID == -1)
			{
				return;
			}
			else if(ObjectID == 0)
			{
				// hair 1
				if(WardMan.HasHat)
				{
					WardMan.DisplayError("You can't fix\nyour hair\nwhile wearing\na hat!");
				}
				else
				{
					WardMan.WitchHair = false;
					WardMan.ShowError = 0;
					ObjectID = 1;
				}
			}
			else if(ObjectID == 1)
			{
				// hair 2
				WardMan.WitchHair = true;
				WardMan.ShowError = 0;
				ObjectID = 0;

			}
			else if(ObjectID == 2)
			{
				// Zonk
				WardMan.DisplayError("That hair\nstyle isn't\nappropriate\nfor any\noccasion!");
			}
			else if(ObjectID == 3)
			{
				// suit 1
				if(WardMan.HasBag)
				{
					WardMan.DisplayError("You can't put\non your\nuniform\nwithout taking\noff your bag\nfirst!");
				}
				else
				{
					WardMan.WitchDress = false;
					WardMan.ShowError = 0;
					ObjectID = 4;

				}
			}
			else if(ObjectID == 4)
			{
				// suit 2
				WardMan.WitchDress = true;
				WardMan.ShowError = 0;
				ObjectID = 3;

			}
			else if(ObjectID == 5)
			{
				// Zonk
				WardMan.DisplayError("Those Overalls\nare a few\nsizes too\nsmall.");
			}
			else if(ObjectID == 6)
			{
				// No hat
				WardMan.HasHat = false;
				WardMan.ShowError = 0;
				ObjectID = 7;

			}
			else if(ObjectID == 7)
			{
				// hat 1
				if(!WardMan.WitchHair)
				{
					WardMan.DisplayError("This hat only\nlooks good on\nyou when your\nhair is a mess!");
				}
				else
				{
					WardMan.HasHat = true;
					WardMan.ShowError = 0;
					ObjectID = 6;

				}
			}
			else if(ObjectID == 8)
			{
				WardMan.DisplayError("I'll set this\nhat aside for\nlater.");
  			}
			else if(ObjectID == 9)
			{
				WardMan.HasBag = false;
				WardMan.ShowError = 0;
				ObjectID = 10;

			}
			else
			{
				if(!WardMan.WitchDress)
				{
					WardMan.DisplayError("You'll need\nto put on your\ndress before\nputting on the\nbag.");
				}
				else
				{
					WardMan.HasBag = true;
					WardMan.ShowError = 0;
					ObjectID = 9;

				}
			}
		}

	}

}
