using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Queen : MonoBehaviour {

	public bool Active;

	public float Speed;

	public GameObject Smashrock;
	public GameObject Ded;
	public GameObject Ohnymybones;

	public float Timer;

	public GameObject Crown;

	public GameObject LevelGoal;


	void Update()
	{

		if(Active)
		{
			Speed-= Time.deltaTime*5;
			transform.parent.eulerAngles += new Vector3(0,0,Speed);
			Timer+= Time.deltaTime;
			if(Speed < -5)
			{

				Instantiate(Ded,transform.position,transform.rotation,transform.parent.parent);
				Instantiate(LevelGoal,transform.parent.position + new Vector3(-3,3,0),LevelGoal.transform.rotation,transform.parent.parent);

				Ohnymybones.SetActive(true);
				Destroy(Crown);
				Destroy(gameObject);
			}


		}



	}


	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "EnemyDeath")
		{
			Active = true;

			Instantiate(Smashrock,other.transform.position,other.transform.rotation,transform.parent.parent);

			Destroy(other.transform.parent.gameObject);

		}
	}
}
