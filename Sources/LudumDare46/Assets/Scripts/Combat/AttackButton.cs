using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButton : MonoBehaviour {

	public bool Mercy;
	public SpriteRenderer SR;
	public Sprite Glow;
	public Sprite NoGlow;

	public Farm F;


	public float Timer;
	public float Timer2;
	public float Timer3;

	public bool PlayerTurn;

	public bool Happening;

	public SpriteRenderer Opponant;
	public float Leavespeed;


	public bool[] PlayerPlants;
	public CombatFlower[] FlowerList;
	public CombatFlower[] EnemyFlowerList;


	public int PlayerPlantTurn;

	public int EnemyTurn = -1;


	public int PlayerHP;
	public int OpponantHP;


	public bool Win;
	public bool Died;

	public bool TildeFight;

	public SpriteRenderer[] PHP;
	public SpriteRenderer[] OHP;

	public GameObject Farm2;


	public TextMesh Vicory;
	public GameObject NoteWin;

	public int PrizeSeeds;

	public bool WinOnce;

	public DataHolder Main;

	// Use this for initialization
	void Start () {
		Main = GameObject.Find("Main").GetComponent<DataHolder>();
	}

	void OnEnalbe()
	{
		PlayerPlantTurn = 0;
		Win = false;
		Died = false;
		Timer = 0;
		Timer2 = 0;
		Timer3 = 0;
		WinOnce = false;
		PrizeSeeds = 0;
		OpponantHP = 2;
		PlayerHP = 4;
		EnemyTurn = -1;
		PlayerTurn = false;
		Happening = false;


	}

	// Update is called once per frame
	void Update () {

		if(Win)
		{
			if(!WinOnce)
			{
				WinOnce = true;
				if(!TildeFight)
				{
					NoteWin.SetActive(false);

					NoteWin.SetActive(true);

					Vicory.text = "You scared off the bug!\n\nYou won " + PrizeSeeds/3 + " seeds!";
					Main.SeedStock += PrizeSeeds/3;

				}
				else
				{
					
					Opponant.gameObject.SetActive(false);
					NoteWin.SetActive(false);

					NoteWin.SetActive(true);
					Vicory.text = "You killed Tilde!";

				}

			}

			if(!TildeFight)
			{
				Opponant.flipX = true;
				Opponant.transform.position +=new Vector3(Leavespeed*-1.5f,Leavespeed,0);
				Leavespeed += Time.deltaTime*0.3f;
			}

		}


		SR.enabled = !Happening && !Win && !Died;

		int hpl = 0;
		while(hpl < PHP.Length)
		{
			PHP[hpl].enabled = hpl < PlayerHP;
			hpl++;
		}
		hpl = 0;
		while(hpl < OHP.Length)
		{
			OHP[hpl].enabled = (hpl < OpponantHP);
			hpl++;
		}


		if(!Mercy)
		{
			SR.sprite = NoGlow;
		}
		Mercy = false;

		if(Happening)
		{
			//step one
			if(EnemyTurn < EnemyFlowerList.Length) //step 2, enemy attacks
			{
				bool AllDead = true;

				int d = 0;
				while(d < EnemyFlowerList.Length)
				{
					if(EnemyFlowerList[d] != null)
					{
						AllDead = false;
					}

					d++;
				}

				if(!AllDead)
				{

					if(EnemyFlowerList.Length != 0)
					{



						Timer2 += Time.deltaTime;
						if(OpponantHP <= 0)
						{
							Happening = false;
							Win = true;

						}
						if(Timer2 > 0.3f)
						{
							Timer2 -= 0.3f;
							//jump to next available enemy flower
							if(EnemyFlowerList.Length != 0)
							{

								while(EnemyFlowerList[EnemyTurn] == null && EnemyTurn < EnemyFlowerList.Length)
								{
									EnemyTurn++;
									if(EnemyTurn >= EnemyFlowerList.Length)
									{
										break;
									}
								}
								if(EnemyTurn < EnemyFlowerList.Length)
								{
									if(EnemyFlowerList[EnemyTurn] != null)
									{
										EnemyFlowerList[EnemyTurn].Attacking = true;
										Timer3 = -2;
									}
								}
							}
							EnemyTurn++;
						}
					}
				}
				else
				{
					EnemyTurn = 10;
					Timer = 0;
				}
			}
			else if(PlayerPlantTurn < 5)
			{
				Timer += Time.deltaTime;

				bool AllDead = true;

				int d = 0;
				while(d < FlowerList.Length)
				{
					if(FlowerList[d] != null)
					{
						AllDead = false;
					}

					d++;
				}


				if(!AllDead)
				{

					if(Timer > 0.3f)
					{
						Timer -= 0.3f;

						if(PlayerPlantTurn <5)
						{
							// jump to next plant turn

							while(!PlayerPlants[PlayerPlantTurn] && PlayerPlantTurn < 5)
							{
								PlayerPlantTurn++;
								if(PlayerPlantTurn >= 5)
								{
									break;
								}


							}
							if(PlayerPlantTurn < 5)
							{
								if(FlowerList[PlayerPlantTurn] != null)
								{
									FlowerList[PlayerPlantTurn].Attacking = true;
								}
								PlayerPlantTurn++;
							}

						}
					}

					if(OpponantHP <= 0)
					{
						Happening = false;
						Win = true;

					}

				}
				else
				{

					PlayerPlantTurn = 6;

				}
			}
			else if(Timer3 < 2)
			{
				if(PlayerHP <= 0)
				{
					Happening = false;
					Died = true;

				}
				if(OpponantHP <= 0)
				{
					Happening = false;
					Win = true;

				}
				Timer3+= Time.deltaTime;
				if(Timer3 >= 1)
				{
					Happening = false;
					Farm2.SetActive(true);
					PrizeSeeds++;

				}

			}


		}

		bool AllDead2 = true;

		int d2 = 0;
		while(d2 < FlowerList.Length)
		{
			if(FlowerList[d2] != null)
			{
				AllDead2 = false;
			}

			d2++;
		}


		if(AllDead2)
		{
			SR.enabled = false;

		}


	}


	void OnMouseOver()
	{
		Mercy = true;
		SR.sprite = Glow;

		if(Input.GetKeyDown(KeyCode.Mouse0) && SR.enabled && !Happening)
		{
			Timer = -3;
			Timer2 = 0;
			Timer3 = -1;
			EnemyTurn = 0;
			Happening = true;
			PlayerPlantTurn = 0;
			Farm2.SetActive(false);

		}
	}


	public void DamageEnemy(int Type)
	{
		int i = 0;
		bool hassheild = false;

		while(i < EnemyFlowerList.Length)
		{
			if(EnemyFlowerList[i] != null)
			{
				hassheild = true;
				break;
			}
			i++;
		}




		if(Type == 2)
		{
			if(i < EnemyFlowerList.Length)
			{
				if(EnemyFlowerList[i] != null)
				{
				EnemyFlowerList[i].Damage();
				}
			}
			OpponantHP--;

		}
		if(Type == 3)
		{
			i = 0;

			while(i < EnemyFlowerList.Length)
			{
				if(EnemyFlowerList[i] != null)
				{
					EnemyFlowerList[i].Damage();
				}
				i++;
			}
		}
		else if(hassheild && Type != 4)
		{
			EnemyFlowerList[i].Damage();

		}
		else
		{
			OpponantHP--;
		}



	}




	public void DamagePlayer(int Type)
	{
		int i = 0;
		bool hassheild = false;

		while(i < FlowerList.Length)
		{
			if(FlowerList[i] != null)
			{
				hassheild = true;
				break;
			}
			i++;
		}



		/*
		if(Type == 2)
		{
			FlowerList[i].Damage();
			PlayerHP--;

		}*/
		if(Type == 3)
		{
			i = 0;

			while(i < FlowerList.Length)
			{
				if(FlowerList[i] != null)
				{
					FlowerList[i].Damage();
				}
				i++;
			}
		}
		else if(hassheild && Type != 4)
		{
			FlowerList[i].Damage();

		}
		else
		{
			PlayerHP--;
		}



	}



}
