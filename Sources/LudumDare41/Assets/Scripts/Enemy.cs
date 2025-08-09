using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float HP;
	public GameObject Item1;
	public bool Pelt;

	public bool Ghost;

	public bool Bone;

	public bool Dragon;
	public GameObject BoneAttack;

	// Use this for initialization
	void Start () {
		
	}

	void FixedUpdate()
	{
		if(Ghost)
		{

			gameObject.GetComponent<Rigidbody>().AddExplosionForce(-3f,GameObject.Find("Player").gameObject.transform.position,50,0,ForceMode.Impulse);

			float SpeedLim = 3f;

			if(gameObject.GetComponent<Rigidbody>().velocity.x > SpeedLim)
			{
				gameObject.GetComponent<Rigidbody>().velocity = new Vector2(SpeedLim,gameObject.GetComponent<Rigidbody>().velocity.y);
			}
			if(gameObject.GetComponent<Rigidbody>().velocity.x < -SpeedLim)
			{
				gameObject.GetComponent<Rigidbody>().velocity = new Vector2(-SpeedLim,gameObject.GetComponent<Rigidbody>().velocity.y);
			}
			if(gameObject.GetComponent<Rigidbody>().velocity.y > SpeedLim)
			{
				gameObject.GetComponent<Rigidbody>().velocity = new Vector2(gameObject.GetComponent<Rigidbody>().velocity.x,SpeedLim);
			}
			if(gameObject.GetComponent<Rigidbody>().velocity.y < -SpeedLim)
			{
				gameObject.GetComponent<Rigidbody>().velocity = new Vector2(gameObject.GetComponent<Rigidbody>().velocity.x,-SpeedLim);
			}


		}




	}

	
	// Update is called once per frame
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Ground" && Pelt)
		{
			gameObject.GetComponent<Rigidbody>().velocity = new Vector2(Random.Range(-4,6),Random.Range(4,8));

		}

		if(other.gameObject.tag == "Ground" && Bone)
		{
			gameObject.GetComponent<Rigidbody>().velocity = new Vector2(Random.Range(-2,8),Random.Range(10,20));
			Instantiate(BoneAttack,gameObject.transform.position,gameObject.transform.rotation,gameObject.transform.parent.transform);
		}

		if(other.gameObject.tag == "Sword")
		{	
			HP = HP - GameObject.Find("Main").gameObject.GetComponent<DataHolder>().SwordDamage * (1 + 1/GameObject.Find("Main").gameObject.GetComponent<DataHolder>().level);

			if(HP < 0)
			{
					GameObject Item = Instantiate(Item1,gameObject.transform.position,gameObject.transform.rotation,gameObject.transform.parent.transform) as GameObject;

				if(Pelt)
				{
				Item.gameObject.GetComponent<Items>().Pelt = true;
				}

					int i = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().level;
					while(i > 0)
					{
						Item = Instantiate(Item1,gameObject.transform.position,gameObject.transform.rotation,gameObject.transform.parent.transform) as GameObject;
					Item.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-2f,2f),Random.Range(2f,4f));
					if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().level >= 2 && HP < -10)
						{
							if(Random.Range(0,2) == 1)
							{

							if(Random.Range(0,2) == 1 && GameObject.Find("Main").gameObject.GetComponent<DataHolder>().level > 5)
								{
								Item.gameObject.GetComponent<Items>().Souls = true;
								}
								else
								{
								Item.gameObject.GetComponent<Items>().Bones = true;
								}
							}
							else
							{
							Item.gameObject.GetComponent<Items>().Pelt = true;

							}
						}
						else							
						{
						Item.gameObject.GetComponent<Items>().Pelt = true;
						}
						i = i-1;
					}


				if(Bone)
				{
					Item.gameObject.GetComponent<Items>().Bones = true;
				}




				if(Ghost)
				{
					Item.gameObject.GetComponent<Items>().Bones = true;
				}


				if(!Dragon)
				{
				i = Random.Range(2,10);
				while(i > 0)
				{
					Item = Instantiate(Item1,gameObject.transform.position,gameObject.transform.rotation,gameObject.transform.parent.transform) as GameObject;
					Item.gameObject.GetComponent<Items>().Gold = true;
					Item.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-4f,4f),Random.Range(2f,6f));
					i = i-1;
				}


				if(Pelt)
				{
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ExpCur = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ExpCur +34;


				}
					if(Bone)
					{
						GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ExpCur = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ExpCur +60;


					}
				if(Ghost)
				{
					GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ExpCur = GameObject.Find("Main").gameObject.GetComponent<DataHolder>().ExpCur +150;


				}
				}
				GameObject.Find("Main").gameObject.GetComponent<DataHolder>().QuestKill += 1;
				


				if(Dragon)
				{


					Item = Instantiate(Item1,gameObject.transform.position,gameObject.transform.rotation,gameObject.transform.parent.transform) as GameObject;
					Item.GetComponent<Rigidbody>().velocity = new Vector3(4,5);

				}





				Destroy(gameObject);
				}

			
		
		}




	}

}
