using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMover : MonoBehaviour {

	public float Speed;

	public float JumpTimer;

	public RuntimeAnimatorController[] Run;
	public RuntimeAnimatorController[] Idle;

	public int ArmorClass;

	bool StartAnim;
	bool StartAnimIdle;

	public float HurtTimer;

	bool StartOnce;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		HurtTimer -= Time.fixedDeltaTime;

		if(Input.GetKey(KeyCode.D))
		{
			Speed = Speed +Time.fixedDeltaTime * 70;
			if(!StartOnce)
			{
				StartOnce = true;
				StartAnim = true;
			}
			gameObject.GetComponent<SpriteRenderer>().flipX = false;
		}
		if(Input.GetKey(KeyCode.A))
		{
			Speed = Speed -Time.fixedDeltaTime * 70;
			if(!StartOnce)
			{
				StartOnce = true;
				StartAnim = true;
			}
			gameObject.GetComponent<SpriteRenderer>().flipX = true;

		}
		if((!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)) || ((Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A))))
		{
			Speed = Speed * 0.85f;
			if(StartOnce)
			{
				StartOnce = false;
				StartAnimIdle = true;
			}
		}
		if(Speed > 15)
		{
			Speed = 15;
		}
		if (Speed < -15)
		{
			Speed = -15;
		}

		if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().CannotOpenInventory)
		{
			Speed = 0;
			}

		gameObject.GetComponent<Rigidbody>().velocity = new Vector2(Speed,gameObject.GetComponent<Rigidbody>().velocity.y);

		if(Input.GetKey(KeyCode.Space) && JumpTimer < 0.125f)
		{
			gameObject.GetComponent<Rigidbody>().velocity = new Vector2(gameObject.GetComponent<Rigidbody>().velocity.x,14);
			JumpTimer = JumpTimer + Time.fixedDeltaTime;

		}
		if(Input.GetKeyUp(KeyCode.Space) && JumpTimer < 0.125f)
		{
			gameObject.GetComponent<Rigidbody>().velocity = new Vector2(gameObject.GetComponent<Rigidbody>().velocity.x,8);
			JumpTimer = 1;
		}

		if(StartAnim)
		{
			gameObject.GetComponent<Animator>().runtimeAnimatorController = Run[ArmorClass];
			StartAnim = false;
		}

		if(StartAnimIdle)
		{
			gameObject.GetComponent<Animator>().runtimeAnimatorController = Idle[ArmorClass];
			StartAnimIdle = false;

		}


	}


	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag == "Ground")
		{
			JumpTimer = 0;
		}




	}





}
