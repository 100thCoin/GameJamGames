using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {


	public bool HasBoss;
	public Enemy Boss;

	public GameObject LevelEnd;

	public bool ShortLevel;


	// Use this for initialization
	void Start () {



		GameObject Mary = GameObject.Find("Mary");
		LevelEnd.GetComponent<DontEndLevel>().Mary = Mary.GetComponent<MaryController>();

		Mary.GetComponent<MaryController>().CurrentLevel = gameObject;

		if(ShortLevel)
		{
			Mary.GetComponent<MaryController>().SmallLevel = true;


		}
		else
		{
			Mary.GetComponent<MaryController>().SmallLevel = false;

		}

		if(HasBoss)
		{
			GameObject.Find("Main Camera").gameObject.transform.Find("BossBar").GetComponent<HealthBar>().Boss = Boss;
			GameObject.Find("Main Camera").gameObject.transform.Find("BossBar").GetComponent<HealthBar>().BossMax = Boss.HP;
			//print("SET BOSS: " + GameObject.Find("Main Camera").gameObject.transform.Find("BossBar").GetComponent<HealthBar>().Boss.gameObject.name);

		}


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
