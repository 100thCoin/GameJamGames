using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCheck : MonoBehaviour {

	//Purpose: Detects if the player is standing on the ground, and able to jump. Also used to lerp the player down a step / stair.

	public bool OnGround;
	public GameObject DustCloud;
	public bool FallCheck;
	public GameObject Player;

	//public bool SlopedWalkDownwards;
	public bool OnSlope;
	//public GameObject FeetCollider;

	public bool Lerp;
	public float OldLerpPos;
	public float NewLerpPos;
	public float LerpTime;
	public float LerpDuration;

	public CharMovement PlayerCon;

	GameObject Cam;

	public bool grace;

	public GameObject FeetCollider;
	public bool Fell;

	CamMovement CMov;
	Rigidbody RB;
	void Start()
	{
		Cam = GameObject.Find("Main Camera");
		//PlayerCon = Player.gameObject.GetComponent<PlayerController>();

		CMov = G.Main.CamMov;
		RB = Player.GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		if(!grace)
		{
			if(!Fell)
			{
				FallCheck = false;
				PlayerCon.InAir = true;
				PlayerCon.Falling = true;
				OnGround = false;
				PlayerCon.JumpingTimer = 3;
				//PlayerCon.CanJump = false;
				Fell = true;
			}
		}
		else
		{
			grace = false;
			Fell= false;
		}





	
	}


	void OnTriggerStay(Collider other)
	{

		if(other.tag == "Ground")
		{


			if(!OnGround && PlayerCon.JumpingTimer >= 2 && PlayerCon.transform.position.y > other.transform.position.y)
				{
					if(other.tag != "Slope")
					{
					if(Player.transform.position.y > other.transform.position.y - other.transform.lossyScale.y*0.5f)
						{
							Player.transform.position = new Vector3(Player.transform.position.x,other.transform.position.y + other.transform.lossyScale.y*0.5f,Player.transform.position.z);
						}
					}

					Vector2 Trig = new Vector2(Mathf.Sin(CMov.YRotation * Mathf.Deg2Rad),Mathf.Cos(CMov.YRotation * Mathf.Deg2Rad));

					GameObject Cloud = Instantiate(DustCloud,new Vector3(transform.position.x - 0.5f * Trig.x,transform.position.y+0.5f,transform.position.z - 0.5f * Trig.y),transform.rotation) as GameObject;
					Cloud.transform.eulerAngles = new Vector3(-90,0,0);

					CMov.HeightMoveTimer = 0;
					CMov.LastHeight = CMov.CurrentHeight;

					CMov.Height = Player.transform.position.y + CMov.Bonus;
					CMov.Duration = 0.5f;

					PlayerCon.rb.velocity = new Vector3(0,0,0);
					PlayerCon.CanJump = true;
					PlayerCon.Falling = false;
					PlayerCon.Jumping = false;
					PlayerCon.InAir = false;

					OnGround = true;
					FallCheck = true;
				PlayerCon.UpdateCameraFalling = false;


				}

			grace = true;



		}
	}



	void Update()
	{

		//PlayerCon = Player.GetComponent<PlayerController>();

		if(PlayerCon.InAir)
		{
			Lerp = false;
		}






		if(!FallCheck)
		{
			if((!PlayerCon.Jumping || !PlayerCon.Falling) && !PlayerCon.InAir)
			{
				if(!OnSlope)
				{

					if(Mathf.Abs(PlayerCon.Movamajig.x)< 0.2f && Mathf.Abs(PlayerCon.Movamajig.y) < 0.2f)
					{
						PlayerCon.JumpVelocity = new Vector3(0,0,0);
					}
					if((Mathf.Abs(PlayerCon.Movamajig.x) >= 0.2f || Mathf.Abs(PlayerCon.Movamajig.y) >= 0.2f) &&(Mathf.Abs(PlayerCon.Movamajig.x) < 0.75f && Mathf.Abs(PlayerCon.Movamajig.y) < 0.75f))
					{
							PlayerCon.JumpVelocity = new Vector3(PlayerCon.Movamajig.y * 0.5f,0, PlayerCon.Movamajig.y * 0.5f);
					}
					if(Mathf.Abs(PlayerCon.Movamajig.x) >= 0.75f || Mathf.Abs(PlayerCon.Movamajig.y) >= 0.75f)
					{
							PlayerCon.JumpVelocity = new Vector3(PlayerCon.Movamajig.x,0, PlayerCon.Movamajig.y);
					}



				

						if(PlayerCon.rb.velocity.y < 1)
						{
		
							PlayerCon.JumpingTimer = 0;
							PlayerCon.Falling = true;
							OnGround = false;
							PlayerCon.CanJump = false;
						}
						else
						{
							PlayerCon.rb.velocity = new Vector3(PlayerCon.rb.velocity.x,0,PlayerCon.rb.velocity.z);

						}

				}				


			}
			else
			{
				OnSlope = false;
			}
		}
	
	}
}
