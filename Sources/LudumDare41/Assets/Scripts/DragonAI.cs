using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAI : MonoBehaviour {

	public float FireballTimer;
	public float DistanceUntilHover;
	public GameObject Camera;

	public bool Donezo;

	public float Timer;

	public GameObject Fireball;

	// Use this for initialization
	void Start () {
		Camera = GameObject.Find("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {

		DistanceUntilHover = gameObject.transform.position.x - Camera.transform.position.x - 16;

		if(DistanceUntilHover <=0 || Donezo)
		{
			Donezo = true;
			gameObject.transform.parent = Camera.gameObject.transform;
			gameObject.transform.position = new Vector3(Camera.transform.position.x + 16,gameObject.transform.position.y,0);


		}

	}

	void FixedUpdate () {

		if(DistanceUntilHover <=0 || Donezo)
		{

			FireballTimer = FireballTimer + Time.fixedDeltaTime;
			Timer = Timer + Time.fixedDeltaTime;

			gameObject.transform.position = new Vector2(gameObject.transform.position.x,Camera.transform.position.y -3 - Mathf.Sin(Timer*5)*3);

			if(FireballTimer >= 3)
			{
				Instantiate(Fireball,new Vector2(gameObject.transform.position.x,gameObject.transform.position.y),gameObject.transform.rotation,gameObject.transform.parent.transform);


			}
			if(FireballTimer >= 3.5f)
			{

				FireballTimer = 0;
			}

		}

	}


}
