using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatFlower : MonoBehaviour {

	public int ID;
	public bool Attacking;
	public bool AttackOnce;
	public bool Enemy;

	public GameObject Target;

	public GameObject[] Projectile;
	public float Timer;

	public int HP;

	public AttackButton Main;

	public bool Damaged;

	public GameObject Splash;

	public Sprite[] Bloom;

	public SpriteRenderer SR;

	public float DamageTimer;

	public Farm F;
	public DataHolder DH;

	public GameObject UprootText;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(Damaged)
		{
			DamageTimer += Time.fixedDeltaTime;


			if(DamageTimer > 1)
			{
				SR.enabled = false;
			}
			else
			{
				SR.enabled = true;

			}

			if(DamageTimer > 1.2f)
			{
				DamageTimer-= 1.2f;
			}

		}


		if(Attacking)
		{
			if(!AttackOnce)
			{
				GameObject P = Instantiate(Projectile[ID],transform.position,transform.rotation,transform);
				P.GetComponent<Projectiles>().Enemy = Enemy;

				if(Enemy)
				{
					bool WillHitFlower = false;

					int d = 0;
					while(d < F.AttackB.EnemyFlowerList.Length)
					{
						if(F.AttackB.EnemyFlowerList[d] != null)
						{
							WillHitFlower = true;
						}
						d++;
					}

					P.GetComponent<Projectiles>().WillHitFlower = WillHitFlower;
				}
				else
				{
					bool WillHitFlower = false;

					int d = 0;
					while(d < F.AttackB.FlowerList.Length)
					{
						if(F.AttackB.FlowerList[d] != null)
						{
							WillHitFlower = true;
						}
						d++;
					}

					P.GetComponent<Projectiles>().WillHitFlower = WillHitFlower;
				}

				AttackOnce = true;
				
				SR.sprite = Bloom[ID];
			}


			if(Timer < 2)
			{
				Timer+= Time.fixedDeltaTime;




				if(Timer >2)
				{

					if(!Enemy)
					{

						if(ID == 0 || ID == 6)
						{
							Main.PlayerHP++;
						}
						else if(ID == 10)
						{
							Main.PlayerHP++;
							Main.PlayerHP++;
						}
						else{

							bool idOverten = ID > 10;
							if(idOverten)
							{
								Main.DamageEnemy(ID-10);
							}
							else
							{
								Main.DamageEnemy(ID);
							}
						}

					}
					else
					{
						if(ID == 0)
						{
							Main.OpponantHP++;
						}
						else if(ID == 10)
						{
							Main.OpponantHP++;
							Main.OpponantHP++;
						}
						else{

							bool idOverten = ID > 10;
							if(idOverten)
							{
								Main.DamagePlayer(ID-10);
								Main.DamagePlayer(ID-10);
							}
							else
							{
								Main.DamagePlayer(ID);
							}
						}
					}

					Attacking = false;
					AttackOnce = false;
					Timer = 0;
				}
			}

		}





	}


	public void Damage()
	{
		if(Damaged)
		{
			Instantiate(Splash,transform.position,transform.rotation,transform.parent);

			if(transform.parent.GetComponent<BulbPlot>())
			{

				transform.parent.GetComponent<BulbPlot>().FilledOnce = false;
				transform.parent.GetComponent<BulbPlot>().Filled = false;

			}

			GameObject Uproot = Instantiate(UprootText,transform.position + new Vector3(0,1,0),transform.rotation,transform.parent);

			int bonus = 3;

			if(ID == 0){bonus = 15;}
			if(ID == 2){bonus = 4;}
			if(ID == 3){bonus = 12;}
			if(ID == 6){bonus = 2;}

			if(ID > 10){bonus += 5;}


			Uproot.GetComponent<SeedBonus>().howmany = bonus;

			DH.SeedStock += bonus;
			F.SeedStock += bonus;

			if(ID == 3){DH.UnlockedGreen = true;}

			Destroy(gameObject);
		}
		else
		{

			Damaged = true;
		}

	}


}
