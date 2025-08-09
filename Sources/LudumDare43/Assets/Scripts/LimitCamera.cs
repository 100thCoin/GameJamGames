using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitCamera : MonoBehaviour {

	public MaryController Mary;

	public float ShakeTimer;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {



		if(ShakeTimer >0)
		{

			gameObject.transform.localPosition = new Vector3(Random.Range(-ShakeTimer,ShakeTimer),Random.Range(ShakeTimer,-ShakeTimer),-103);
			ShakeTimer -= Time.fixedDeltaTime;

		}

		if(gameObject.transform.position.x <= 11)
		{
			gameObject.transform.position = new Vector3(11,gameObject.transform.position.y,-100);
		}
		else if(gameObject.transform.position.x >= 172 && !Mary.SmallLevel)
		{
			gameObject.transform.position = new Vector3(172,gameObject.transform.position.y,-100);
		}
		else if(gameObject.transform.position.x >= 76 && Mary.SmallLevel)
		{
			gameObject.transform.position = new Vector3(76,gameObject.transform.position.y,-100);
		}
		else
		{
			gameObject.transform.localPosition = new Vector3(0,0,-100);
			if(ShakeTimer >0)
			{

				gameObject.transform.localPosition = new Vector3(Random.Range(-ShakeTimer,ShakeTimer),Random.Range(ShakeTimer,-ShakeTimer),-103);


			}
		}
	}
	void Update () {
		if(ShakeTimer >0)
		{

			gameObject.transform.localPosition = new Vector3(Random.Range(-ShakeTimer,ShakeTimer),Random.Range(ShakeTimer,-ShakeTimer),-103);


		}

		if(gameObject.transform.position.x <= 11)
		{
			gameObject.transform.position = new Vector3(11,gameObject.transform.position.y,-100);
		}
		else if(gameObject.transform.position.x >= 172 && !Mary.SmallLevel)
		{
			gameObject.transform.position = new Vector3(172,gameObject.transform.position.y,-100);
		}
		else if(gameObject.transform.position.x >= 76 && Mary.SmallLevel)
		{
			gameObject.transform.position = new Vector3(76,gameObject.transform.position.y,-100);
		}
		else
		{
			gameObject.transform.localPosition = new Vector3(0,0,-100);
			if(ShakeTimer >0)
			{

				gameObject.transform.localPosition = new Vector3(Random.Range(-ShakeTimer,ShakeTimer),Random.Range(ShakeTimer,-ShakeTimer),-103);


			}
		}
	}
	void LateUpdate () {
		if(ShakeTimer >0)
		{

			gameObject.transform.localPosition = new Vector3(Random.Range(-ShakeTimer,ShakeTimer),Random.Range(ShakeTimer,-ShakeTimer),-103);


		}

		if(gameObject.transform.position.x <= 11)
		{
			gameObject.transform.position = new Vector3(11,gameObject.transform.position.y,-100);
		}
		else if(gameObject.transform.position.x >= 172 && !Mary.SmallLevel)
		{
			gameObject.transform.position = new Vector3(172,gameObject.transform.position.y,-100);
		}
		else if(gameObject.transform.position.x >= 76 && Mary.SmallLevel)
		{
			gameObject.transform.position = new Vector3(76,gameObject.transform.position.y,-100);
		}
		else
		{
			gameObject.transform.localPosition = new Vector3(0,0,-100);
			if(ShakeTimer >0)
			{

				gameObject.transform.localPosition = new Vector3(Random.Range(-ShakeTimer,ShakeTimer),Random.Range(ShakeTimer,-ShakeTimer),-103);


			}
		}
	}




}
