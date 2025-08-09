using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public bool UpsideDown;
	public float GroundElevation;
	public float Speed;
	public bool OnGround;
	public RuntimeAnimatorController Idle;
	public RuntimeAnimatorController Run;
	public GameObject Explosion;
	public bool Win;
	public bool AlreadyWon;
	public GameObject Snap;
	public GameObject Baton;
	public GameObject objective;
	public bool JetPack;
	public float VSpeed;
	public bool Freefall;
	public FreeFallHolder Hold;
	public Sprite Freefall1;
	public Sprite Freefall2;
	public Sprite Freefall3;
	public bool FreefallWinOnce;
	public bool FreefallWin;
	public GameObject FFObjective;
	public bool[] SnapOnce;
	public bool PerfectDive;
	public GameObject Scorecard;
	public float Timer;
	public bool NoMoving;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update()
	{
		Timer = Timer + Time.deltaTime;
		if(Freefall && ! FreefallWin)
		{
			if(Hold.Altitude < 2250 && Hold.Altitude > 1550 && !SnapOnce[0])
			{
				gameObject.GetComponent<SpriteRenderer>().sprite = Freefall1;
				Instantiate(Snap,gameObject.transform.position,gameObject.transform.rotation,gameObject.transform.parent.transform);
				SnapOnce[0] = true;
			}
			if(Hold.Altitude < 1550 && Hold.Altitude > 1000 && !SnapOnce[1])
			{
				gameObject.GetComponent<SpriteRenderer>().sprite = Freefall2;
				Instantiate(Snap,gameObject.transform.position,gameObject.transform.rotation,gameObject.transform.parent.transform);
				SnapOnce[1] = true;
			}
			if(Hold.Altitude < 1000 && Hold.Altitude > 1 && !SnapOnce[2])
			{
				gameObject.GetComponent<SpriteRenderer>().sprite = Freefall3;
				Instantiate(Snap,gameObject.transform.position,gameObject.transform.rotation,gameObject.transform.parent.transform);
				SnapOnce[2] = true;
			}
			if(Hold.Altitude < 1 && !FreefallWinOnce)
			{
				gameObject.GetComponent<Animator>().runtimeAnimatorController = Run;
				FreefallWinOnce = true;
				Destroy(Hold.Target);
				if(gameObject.transform.position.x >= Hold.TargetMin && gameObject.transform.position.x <= Hold.TargetMax)
				{
					gameObject.transform.position = new Vector2(gameObject.transform.position.x,-1);
					FreefallWin = true;
					Instantiate(Snap,gameObject.transform.position,gameObject.transform.rotation);

					objective = Instantiate(FFObjective,new Vector2(Hold.Target.transform.position.x, gameObject.transform.position.y -1),gameObject.transform.parent.rotation,gameObject.transform.parent.transform) as GameObject;
					objective.gameObject.GetComponent<ObjectivePlayer>().P = gameObject.GetComponent<PlayerMovement>();

					Win = true;
					AlreadyWon = false;
					objective.gameObject.GetComponent<ObjectivePlayer>().SnapPos = gameObject.transform.position;
					objective.gameObject.GetComponent<ObjectivePlayer>().PostFFSnap = true;
					objective.gameObject.GetComponent<ObjectivePlayer>().Baton.gameObject.SetActive(true);
					Baton.SetActive(false);
				}
				else
				{
					StartCoroutine(OneFrameTillBoom());
					gameObject.transform.position = new Vector2(gameObject.transform.position.x,-1);
					FreefallWin = false;
					//Instantiate(Snap,gameObject.transform.position,gameObject.transform.rotation);

					objective = Instantiate(FFObjective,new Vector2(Hold.Target.transform.position.x, gameObject.transform.position.y -1),gameObject.transform.parent.rotation,gameObject.transform.parent.transform) as GameObject;
					//objective.gameObject.GetComponent<ObjectivePlayer>().SnapPos = gameObject.transform.position;
					//objective.gameObject.GetComponent<ObjectivePlayer>().PostFFSnap = true;
					//objective.gameObject.GetComponent<ObjectivePlayer>().Baton.gameObject.SetActive(true);
					//Baton.SetActive(false);
				}
	


			}



		}
		if(FreefallWin)
		{
			
			gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

		}

		if(!Win && !JetPack && !Freefall)
		{
			if(Input.GetKeyDown(KeyCode.Space) && OnGround && !UpsideDown)
			{
				gameObject.GetComponent<Rigidbody>().velocity = new Vector3(gameObject.GetComponent<Rigidbody>().velocity.x,10,0);
				UpsideDown = true;
				OnGround = false;
			}
			else if(Input.GetKeyDown(KeyCode.Space) && OnGround && UpsideDown)
			{
				gameObject.GetComponent<Rigidbody>().velocity = new Vector3(gameObject.GetComponent<Rigidbody>().velocity.x,-10,0);
				UpsideDown = false;
				OnGround = false;
			}
		}
	}

	void FixedUpdate () {




		if(Win)
		{
			if(AlreadyWon)
			{
				AlreadyWon = false;
				Instantiate(Snap,gameObject.transform.position,gameObject.transform.rotation,gameObject.transform.parent.transform);
				Baton.gameObject.SetActive(false);
				objective.gameObject.GetComponent<ObjectivePlayer>().SnapPos = gameObject.transform.position;
				objective.gameObject.GetComponent<ObjectivePlayer>().PostSnap = true;
				objective.gameObject.GetComponent<ObjectivePlayer>().Baton.gameObject.SetActive(true);


			}

			gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		}
		else
		{
			if(!NoMoving)
			{
			if(Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
			{
				Speed = Speed + Time.fixedDeltaTime * 15;
					if(Speed <0)
					{
						Speed = Speed + Time.fixedDeltaTime * 15;
					}
				if(!Freefall)
				{
				gameObject.GetComponent<Animator>().runtimeAnimatorController = Run;
				}
			}
			else if(Input.GetKey(KeyCode.A))
			{
				Speed = Speed - Time.fixedDeltaTime * 15;
					if(Speed >0)
					{
						Speed = Speed - Time.fixedDeltaTime * 15;
					}
				if(!Freefall)
				{
				gameObject.GetComponent<Animator>().runtimeAnimatorController = Run;
				}
			}
			else
			{
				if(!Freefall)
				{
				gameObject.GetComponent<Animator>().runtimeAnimatorController = Idle;
				}
			}
			if(JetPack)
			{

				if(Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
				{
					VSpeed = VSpeed + Time.fixedDeltaTime * 15;
					if(VSpeed <0)
					{
						VSpeed = VSpeed + Time.fixedDeltaTime * 15;
					}
					gameObject.GetComponent<Animator>().runtimeAnimatorController = Run;

				}
				else if(Input.GetKey(KeyCode.S))
				{
					VSpeed = VSpeed - Time.fixedDeltaTime * 15;
					if(VSpeed >0)
					{
						VSpeed = VSpeed - Time.fixedDeltaTime * 15;
					}
					gameObject.GetComponent<Animator>().runtimeAnimatorController = Run;

				}


			}

			}


			if(Speed >= 17)
			{
				Speed = 17;
			}
			else 	if(Speed <= -17)
			{
				Speed = -17;
			}


			if(!JetPack)
			{
				gameObject.GetComponent<Rigidbody>().velocity = new Vector3(Speed,gameObject.GetComponent<Rigidbody>().velocity.y,0);
			}
			else
			{
				if(Speed >= 13)
				{
					Speed = 13;
				}
				else 	if(Speed <= -13)
				{
					Speed = -13;
				}
				if(VSpeed >= 13)
				{
					VSpeed = 13;
				}
				else 	if(VSpeed <= -13)
				{
					VSpeed = -13;
				}



				gameObject.GetComponent<Rigidbody>().velocity = new Vector3(Speed,VSpeed,0);
			}
		}


		if(PerfectDive)
		{
			Speed = 0;

			gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;


		}


	}
		



	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag == "Death")
		{
			Instantiate(Explosion,gameObject.transform.position,gameObject.transform.rotation);
			Destroy(gameObject);
			
		}
		else if(other.gameObject.tag == "LeftWall")
		{
			if(Speed <0)
			{
			Speed =0;
			}
		}
		else if(other.gameObject.tag == "RightWall")
		{
			if(Speed >0)
			{
				Speed =0;
			}
		}
		else if(other.gameObject.tag == "Ladder")
		{
			if(Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
			{
				Speed=0;
				gameObject.transform.position = new Vector3(other.gameObject.transform.position.x,gameObject.gameObject.transform.position.y,gameObject.transform.position.z);
				gameObject.GetComponent<Rigidbody>().velocity = new Vector3(Speed,15,0);

			}
			else if(Input.GetKey(KeyCode.S))
			{
				Speed=0;
				gameObject.transform.position = new Vector3(other.gameObject.transform.position.x,gameObject.gameObject.transform.position.y,gameObject.transform.position.z);
				gameObject.GetComponent<Rigidbody>().velocity = new Vector3(Speed,-15,0);

			}	
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Floor" && JetPack)
		{
			Speed = 0;
			VSpeed =0;
		}

		if(other.gameObject.tag == "Win" && !PerfectDive)
		{
			Speed = 0;
			VSpeed =0;
			gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
			Instantiate(Snap,gameObject.transform.position,gameObject.transform.rotation);
			StartCoroutine(EndGame());
			PerfectDive = true;
		}

	}

	IEnumerator OneFrameTillBoom()
	{
		yield return new WaitForSeconds(0.02f);
		gameObject.transform.position = new Vector2(gameObject.transform.position.x,-6);
		yield return new WaitForSeconds(0.02f);
		gameObject.transform.position = new Vector2(gameObject.transform.position.x,-6);
		Instantiate(Explosion,gameObject.transform.position,gameObject.transform.rotation);
		Destroy(gameObject);
	}


	IEnumerator EndGame()
	{
		yield return new WaitForSeconds(2);
		Instantiate( Scorecard,Vector3.zero,gameObject.transform.rotation,gameObject.transform.parent);



	}

}
