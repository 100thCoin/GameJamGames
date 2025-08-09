using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

	public bool Miggs; //else tilde

	public float WalkDest;

	public float Speed;

	public bool Left;

	public RuntimeAnimatorController Idle;
	public RuntimeAnimatorController Walk;

	public Animator Anim;
	public SpriteRenderer SR;

	public DataHolder Main;

	public SpriteRenderer Bubb;
	public bool MercyFrame;
	public bool MercyFrame2;

	public GameObject Player;
	public bool tleleOnce;

	public RuntimeAnimatorController SeriousStare;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Mathf.Abs(transform.position.x - WalkDest) > 1 && Main.Progress != 3.99f)
		{
			Anim.runtimeAnimatorController = Walk;

			Vector3 Walkin = new Vector3(transform.position.x - WalkDest,0,0).normalized * -Speed;

			if(!Miggs)
			{
				SR.flipX = Walkin.x > 0;
			}
			else
			{
				SR.flipX = Walkin.x < 0;
			}
			if(Main.Progress != 3.99f && Main.Progress != 4f)
			{
				transform.position += Walkin;
			}
			else if(!Miggs)
			{
				Anim.runtimeAnimatorController = SeriousStare;

			}

			if(Main.Progress == 4f && !Main.NoMoving && Mathf.Abs(transform.position.x - 40) > 1)
			{
				Walkin = new Vector3(transform.position.x - 40,0,0).normalized * -Speed;
				SR.flipX = Walkin.x > 0;

				transform.position += Walkin;
			}

			if(Mathf.Abs(transform.position.x - WalkDest) <= 1)
			{
				Anim.runtimeAnimatorController = Idle;

			}

		}


		if(!MercyFrame || Main.WL.InDialogue)
		{
			Bubb.enabled = false;
		}
		if(!MercyFrame2)
		{
		MercyFrame = false;
		}
		MercyFrame2 = false;


		if(Main.Progress == 3.99f)
		{

			Anim.runtimeAnimatorController = SeriousStare;


			SR.flipX = false;
			if(!Miggs)
			{
				float XDist = Mathf.Abs(transform.position.x - Player.transform.position.x);

				if(XDist < 10.5f)
				{
					Main.Progress = 4;
					Main.WL.DEBUGREAD(Main.WL.GetComponent<DialogueHolder>().TildeIsWatching);

				}


			}
		}



	}




	void OnMouseOver()
	{

		if(!Main.WL.InDialogue)
		{
			Bubb.enabled = true;
			MercyFrame = true;
			MercyFrame2 = true;
			if(Input.GetKeyDown(KeyCode.Mouse0))
			{

				if(!Miggs)
				{
					if(Main.Progress == 1f)
					{
						Main.WL.DEBUGREAD(Main.WL.GetComponent<DialogueHolder>().TildeMoreHelp);
					}
					if(Main.Progress == 1.2f)
					{
						Main.WL.DEBUGREAD(Main.WL.GetComponent<DialogueHolder>().TildeGrewFlower);
					}
					if(Main.Progress == 3.14f)
					{
						Main.WL.DEBUGREAD(Main.WL.GetComponent<DialogueHolder>().TildeGreatWork);
					}
					if(Main.Progress >= 3.3f && Main.Progress < 3.3451f)
					{
						Main.WL.DEBUGREAD(Main.WL.GetComponent<DialogueHolder>().TildeOnYourOwn);
					}

					if( Main.Progress == 3.3451f)
					{
						Main.WL.DEBUGREAD(Main.WL.GetComponent<DialogueHolder>().TildeThatWasQuick);
					}

					if(Main.Progress == 3.3452f)
					{
						Main.WL.DEBUGREAD(Main.WL.GetComponent<DialogueHolder>().TildeThatWasQuick);
					}

					if(Main.Progress >= 3.5 && Main.Progress < 3.99f)
					{
						Main.WL.DEBUGREAD(Main.WL.GetComponent<DialogueHolder>().TildeBugGoneKeepworking);
					}

					if(Main.Progress == 4)
					{
						Main.WL.DEBUGREAD(Main.WL.GetComponent<DialogueHolder>().TildeItsOkayYouAreSafe);
					}

					if(Main.Progress == 4.1f)
					{
						Main.WL.DEBUGREAD(Main.WL.GetComponent<DialogueHolder>().TildeMakeWhiteFlower);
					}

					if(Main.Progress == 4.2f)
					{
						Main.WL.DEBUGREAD(Main.WL.GetComponent<DialogueHolder>().TildeMakeWhiteFlower);
					}
					if(Main.Progress == 4.3f)
					{
						Main.WL.DEBUGREAD(Main.WL.GetComponent<DialogueHolder>().TildeMakeWhiteFlower);
					}
					if(Main.Progress == 4.6f)
					{
						Main.WL.DEBUGREAD(Main.WL.GetComponent<DialogueHolder>().TildeHowIsTheFlower);
					}

				}
				else
				{

					if(Main.Progress == 3.1f)
					{
						Main.WL.DEBUGREAD(Main.WL.GetComponent<DialogueHolder>().Miggrepeat);
					}

					if(Main.Progress == 3.14f)
					{
						Main.WL.DEBUGREAD(Main.WL.GetComponent<DialogueHolder>().MiggsSaysIsntTildeCreepy);
					}

					if(Main.Progress == 3.3f)
					{
						Main.WL.DEBUGREAD(Main.WL.GetComponent<DialogueHolder>().MiggsHelps);
					}

					if(Main.Progress == 3.33f)
					{
						Main.WL.DEBUGREAD(Main.WL.GetComponent<DialogueHolder>().MiggsYouTakeItFromHere);
					}

					if(Main.Progress == 3.3451f)
					{
						Main.WL.DEBUGREAD(Main.WL.GetComponent<DialogueHolder>().MiggsHeyyNice);
					}
					if(Main.Progress == 3.3452f)
					{
						Main.WL.DEBUGREAD(Main.WL.GetComponent<DialogueHolder>().MiggsGoOnShowTilde);
					}

					if(Main.Progress == 3.5f)
					{
						Main.WL.DEBUGREAD(Main.WL.GetComponent<DialogueHolder>().MiggsNeedSeeds);
					}

					if(Main.Progress == 3.6f)
					{
						Main.WL.DEBUGREAD(Main.WL.GetComponent<DialogueHolder>().MiggsItsOkayToDoThis);
					}

					if(Main.Progress == 3.7f)
					{
						Main.WL.DEBUGREAD(Main.WL.GetComponent<DialogueHolder>().MiggsPickIt);
					}

					if(Main.Progress == 3.8f)
					{
						Main.WL.DEBUGREAD(Main.WL.GetComponent<DialogueHolder>().MiggsDecorationsAndIveGotASecret);
					}
					if(Main.Progress == 3.9f || Main.Progress == 3.91f)
					{
						Main.WL.DEBUGREAD(Main.WL.GetComponent<DialogueHolder>().MiggsICircledItForYa);
					}

					if(Main.Progress == 4.1f)
					{
						Main.WL.DEBUGREAD(Main.WL.GetComponent<DialogueHolder>().MiggsIsDead);
					}
					if(Main.Progress == 4.2f)
					{
						Main.WL.DEBUGREAD(Main.WL.GetComponent<DialogueHolder>().MiggsIsStillDead);
					}
					if(Main.Progress == 4.3f)
					{
						Main.WL.DEBUGREAD(Main.WL.GetComponent<DialogueHolder>().MiggsIsStillDead);
					}
				}


			}
		}
	}





}
