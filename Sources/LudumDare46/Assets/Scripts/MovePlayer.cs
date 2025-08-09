using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour {

	public RuntimeAnimatorController Idle;
	public RuntimeAnimatorController Run;

	public float Accel;
	public float Speed;
	public float Limit;

	public Animator Anim;
	public SpriteRenderer SR;

	public float FootstepTimer;
	public GameObject FootstepPrefab;

	public DataHolder Main;

	public bool didOnce;

	// Use this for initialization
	void Start () {
		Main = GameObject.Find("Main").GetComponent<DataHolder>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {



		if(!Main.NoMoving)
		{

			transform.position += new Vector3(Speed,0,0);



		if(Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
		{
			if(Speed < 0)
			{
				Speed += Accel;
			}
			Speed+= Accel;
			if(Speed > Limit)
			{
				Speed = Limit;
			}
			Anim.runtimeAnimatorController = Run;
			SR.flipX = false;


			FootstepTimer+= Time.fixedDeltaTime;
			if(FootstepTimer > 0.25f)
			{
				FootstepTimer-=0.25f;
				Instantiate(FootstepPrefab,transform.position - new Vector3(0,1.11f,-0.5f),transform.rotation);
			}


		}
		else if(!Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A))
		{
			if(Speed > 0)
			{
				Speed -= Accel;
			}
			Speed -= Accel;

			if(Speed < -Limit)
			{
				Speed = -Limit;
			}
			Anim.runtimeAnimatorController = Run;
			SR.flipX = true;

			FootstepTimer+= Time.fixedDeltaTime;
			if(FootstepTimer > 0.25f)
			{
				FootstepTimer-=0.25f;
				Instantiate(FootstepPrefab,transform.position - new Vector3(0,1.11f,-0.5f),transform.rotation);
			}
		}
		else
		{
			Speed *= 0.9f;
			Anim.runtimeAnimatorController = Idle;
		}
		}
		else
		{
			Speed *= 0.9f;
		}

	}

	public void OnTriggerStay(Collider other)
	{
		if(other.tag == "InvisLeft")
		{
			if(Speed < 0)
			{
				Speed = 0;
				if((Main.Progress == 1 || Main.Progress == 1.1f || Main.Progress == 1.2f) && !didOnce)
				{
					didOnce = true;
					Main.WL.DEBUGREAD(Main.WL.GetComponent<DialogueHolder>().MonoProbsStayHere);


				}

			}	

		}


		if(other.tag == "InvisRight")
		{
			if(Speed > 0)
			{
				Speed = 0;
				if((Main.Progress == 1 || Main.Progress == 1.1f || Main.Progress == 1.2f) && !didOnce)
				{
					didOnce = true;
					Main.WL.DEBUGREAD(Main.WL.GetComponent<DialogueHolder>().MonoProbsStayHere);


				}

			}	

		}

		if(other.tag == "Miggs")
		{
			if(Speed < 0)
			{
				Speed = 0;
			}	
		}

		if(other.tag == "Tilde")
		{
			if(Speed > 0)
			{
				Speed = 0;
			}	
		}




	}



}
