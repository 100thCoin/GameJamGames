using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Orc : MonoBehaviour {

	public GameObject Gore;
	public RuntimeAnimatorController Idle;
	public RuntimeAnimatorController Walk;
	public bool Walking;

	public float Speed;

	public bool Left;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(Walking)
		{
			if(!Left)
			{
				transform.position += new Vector3(Speed * Time.fixedDeltaTime,0,0);
			}
			else
			{
				transform.position += new Vector3(-Speed * Time.fixedDeltaTime,0,0);
			}

		}


	}


	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Killzone")
		{
			Instantiate(Gore,new Vector3(transform.position.x,transform.position.y-1,0.1f),Gore.transform.rotation,transform.parent);

			Destroy(gameObject);
		}



		if(other.tag == "CantGoRight")
		{
			Left = true;
			GetComponent<SpriteRenderer>().flipX = true;
		}
		if(other.tag == "CantGoLeft")
		{
			Left = false;
			GetComponent<SpriteRenderer>().flipX = false;

		}
	}



}
