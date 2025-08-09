using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_Movement : MonoBehaviour {

	public RuntimeAnimatorController Idle;
	public RuntimeAnimatorController Run;
	public RuntimeAnimatorController ItemAnim;

	public bool CollectingItem;
	public bool CanLeaveItem;

	public Animator Anim;
	public Animator AnimL;
	public Animator AnimR;

	public SpriteRenderer SR;
	public SpriteRenderer SRL;
	public SpriteRenderer SRR;

	public float StripLength;
	public float Speed;
	public float SpeedLimit;
	public float Velocity;

	public SpriteRenderer ItemPick;
	public GameObject ThreeDStar;
	public TextMesh ItemTitle;

	public float ItemTimer;

	public bool Sliced;
	public float SlcieDist;

	public bool Snaking;

	public LoopMain LoopM;
	public Camera_Movement CamMov;
	public bool BeginSnaking;
	public bool SnakingLeft;

	public GameObject PlayerLoopFront;

	public bool JamUntilSnakeLeft;
	public bool JamUntilSnakeRight;

	//public Vector3 Temp;
	public Vector3 Temp2;
	public Vector3 Temp3;

	// Use this for initialization
	void Start () {
		
	}


	//MAGIC NUBMERS
	float m = 0.892f;
	float a = 1.076f;
	float d = 2.5f;
	float b = 4;
	float c = 0.5f;
	//Don't question my math!

	// Update is called once per frame


	void Update()
	{
		SRL.transform.position = new Vector3(transform.position.x - StripLength,transform.position.y,transform.position.z);
		SRR.transform.position = new Vector3(transform.position.x + StripLength,transform.position.y,transform.position.z);

		if(CollectingItem)
		{
			if(ItemTimer >=0)
			{
				ItemTimer += Time.deltaTime;

				float Timer2 = -Mathf.Cos(Mathf.PI*ItemTimer) * 0.5f+0.5f;

				ThreeDStar.transform.localScale = Vector3.one * 16 * (((c*Mathf.Sin(d*Mathf.PI*m*Timer2))/(Mathf.PI + Mathf.Pow(m*Timer2,3))-Mathf.Pow(m*Timer2,2)+a*m*Timer2)*4 + (-Mathf.Pow(-1+2*ItemTimer,2)+1)*0.5f)/1.223f;
				ItemPick.transform.localScale = ThreeDStar.transform.localScale * 0.1f;
				if(ItemTimer>0.82f)
				{
					ThreeDStar.transform.localScale = Vector3.one * 16;
					ItemPick.transform.localScale = ThreeDStar.transform.localScale * 0.1f;
					CanLeaveItem = true;
					ItemTimer = -1;
				}
			}
		}




		if(BeginSnaking)
		{
			BeginSnaking = false;
			StartSnaking();
		}

	}


	// Update is called once per frame
	void FixedUpdate () {
		Temp2 = LoopM.OriginalConnector[LoopM.SliceAt].transform.position;
		Temp3= LoopM.OriginalConnector[LoopM.SliceAt].transform.eulerAngles;
		if(CollectingItem)
		{
			Anim.runtimeAnimatorController = ItemAnim;
			AnimR.runtimeAnimatorController = Anim.runtimeAnimatorController;
			AnimL.runtimeAnimatorController = Anim.runtimeAnimatorController;

			if(CanLeaveItem && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
			{
				Main.ItemName.text = "";
				CollectingItem = false;
				CanLeaveItem = false;
				ThreeDStar.transform.localScale = Vector3.zero;
				ItemPick.transform.localScale = Vector3.zero;
				PlayerLoopFront.SetActive(false);
			}				

		}

		if(!CollectingItem && !Main.InsideCutscene)
		{
			if(Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
			{
				Main.HaltTip1 = true;
				Main.SendTip2 = true;
				//Temp = LoopM.TheLoopConnector[LoopM.TheLoop.Length-1].transform.position;

				Speed += Time.fixedDeltaTime * Velocity;
				Anim.runtimeAnimatorController = Run;
				SR.flipX = false;
				if(SnakingLeft && Snaking)
				{
					StopSnaking();
				}
			}
			else if(Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
			{
				Main.HaltTip1 = true;
				Main.SendTip2 = true;
				Speed -= Time.fixedDeltaTime * Velocity;
				Anim.runtimeAnimatorController = Run;
				SR.flipX = true;
				if(!SnakingLeft && Snaking)
				{
					StopSnaking();
				}
			}
			else
			{
				Anim.runtimeAnimatorController = Idle;
				Speed *= 0.8f;
			}
			if(NoLeft && Speed <0)
			{
				Speed =0;
			}
			if(NoRight && Speed >0)
			{
				Speed =0;
			}

			AnimR.runtimeAnimatorController = Anim.runtimeAnimatorController;
			AnimL.runtimeAnimatorController = Anim.runtimeAnimatorController;
			SRR.flipX = SR.flipX;
			SRL.flipX = SR.flipX;

			if(Snaking)
			{
				if(SnakingLeft)
				{
					if(Speed < 0)
					{
						Speed = 0;
					}
					else if(Speed > 0)
					{
						StopSnaking();
					}						
				}
				else if(!SnakingLeft)
				{
					if(Speed > 0)
					{
						Speed = 0;
					}
					else if(Speed < 0)
					{
						StopSnaking();
					}	
				}
			}

			Speed = Mathf.Abs(Speed) > SpeedLimit ? SpeedLimit * new Vector2(Speed,0).normalized.x : Speed;

			transform.position += new Vector3(Speed,0,0);

			if(transform.position.x > StripLength*0.5f)
			{
				transform.position -= new Vector3(StripLength,0,0);
			}
			else if(transform.position.x <= -StripLength*0.5f)
			{
				transform.position += new Vector3(StripLength,0,0);
			}

			if(Sliced)
			{

				float D1 = (-StripLength*0.5f + 1f + (SlcieDist/(LoopM.TheLoop.Length +0f))*StripLength)%StripLength;

				if(D1 < -StripLength*0.5f +1)
				{
					D1+= StripLength;
				}
				if(D1 > StripLength*0.5f -1)
				{
					D1-= StripLength;
				}

				float E1 = (SlcieDist/LoopM.TheLoop.Length)*StripLength -StripLength*0.5f;

				if(E1 < -StripLength*0.5f +1)
				{
					E1+= StripLength;
				}
				if(E1 > StripLength*0.5f -1)
				{
					E1-= StripLength;
				}
					
				if(transform.position.x < D1 && transform.position.x > E1)
				{
					if(Speed <0)
					{
						Speed = 0;
						SnakingLeft = true;
						StartSnaking();
					}
				}
				else
				{
					float tempdist = SlcieDist;
					if(tempdist == 0)
					{
						tempdist = LoopM.TheLoop.Length-1;
					}


					float D = ((StripLength*0.5f - 1f +(SlcieDist/(LoopM.TheLoop.Length + 0f))*StripLength+StripLength)%StripLength);

					if(D < -StripLength*0.5f + 1)
					{
						D+= StripLength;
					}
					if(D > StripLength*0.5f -1)
					{
						D-= StripLength;
					}
					float E = (SlcieDist/LoopM.TheLoop.Length)*StripLength -StripLength*0.5f;

					if(E > StripLength*0.5f -1)
					{
						E-= StripLength;
					}
					if(E < -StripLength*0.5f +1)
					{
						E+= StripLength;
					}


					//print(transform.position.x + " > " +(D)  + " && "  +  transform.position.x + "  <? " + E);


					if(transform.position.x > (D) && transform.position.x < E)
					{
						if(Speed >0)
						{
							Speed = 0;
							SnakingLeft = false;
							StartSnaking();
						}
					}
				}
			}

		}
	

	}

	public DataHolder Main;

	public bool NoLeft;
	public bool NoRight;

	void OnTriggerEnter(Collider other)
	{
		if(other.name == "DoorOpen")
		{
			Main.DoorsOpen = true;
		}
		if(other.name == "DoorClose")
		{
			Main.DoorsOpen = false;
		}
		if(other.tag == "Orc")
		{
			other.GetComponent<SpriteRenderer>().enabled = true;
		}
		if(other.tag == "NoLeft")
		{
			NoLeft = true;
		}
		if(other.tag == "NoRight")
		{
			NoRight = true;
		}
		if(other.tag == "Chest")
		{
			other.transform.parent.GetComponent<TreasureChest>().InRange = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Orc")
		{
			other.GetComponent<SpriteRenderer>().enabled = false;
			if(Main.OrcDead)
			{
				other.transform.parent.GetComponent<MouseInteractable>().OrcAnim.runtimeAnimatorController = Main.OrcDeadAnim;
				Destroy(other.gameObject);
			}

		}
		if(other.tag == "NoLeft")
		{
			NoLeft = false;
		}
		if(other.tag == "NoRight")
		{
			NoRight = false;
		}
		if(other.tag == "Chest")
		{
			other.transform.parent.GetComponent<TreasureChest>().InRange = false;
		}
	}

	void StartSnaking()
	{
		Main.SendTip3 = true;
		GameObject.Find("Main").GetComponent<DataHolder>().Move = true;
		GameObject.Find("Main").GetComponent<DataHolder>().Forest = false;
		GameObject.Find("Main").GetComponent<DataHolder>().Calm = false;

		Snaking = true;
		CamMov.Snaking = true;
		LoopM.Snaking = true;
		CamMov.PerfectRing = false;
		if(!SnakingLeft)
		{
			JamUntilSnakeRight = false;
			LoopM.FixLoop();
			LoopM.SliceRight(LoopM.SliceAt);
		}
		else
		{
			JamUntilSnakeLeft = false;
		}
		CamMov.SnakeLeft = SnakingLeft;
		CamMov.SnakingFocus = LoopM.TheLoop[0].gameObject;
		LoopM.SnakePivot.transform.parent = LoopM.TheLoop[0].transform;
		LoopM.SnakePivot.transform.localPosition = new Vector3(0,0,20.37183f);
	}

	void StopSnaking()
	{
		Snaking = false;
		CamMov.Snaking = false;
		LoopM.Snaking = false;
		if(!SnakingLeft)
		{
			LoopM.TempPos = Temp2;
			LoopM.TempRot = LoopM.TheLoop[LoopM.SliceAt].transform.parent.eulerAngles.y;
			LoopM.FixLoop();
			LoopM.SliceLeft(LoopM.SliceAt);
			LoopM.TheLoopConnector[0].transform.position = Temp2;
			LoopM.TheLoopConnector[0].transform.eulerAngles = Temp3;

		}
		LoopM.SnakePivot.transform.parent = LoopM.TheLoop[0].transform;
		LoopM.SnakePivot.transform.localPosition = new Vector3(0,0,20.37183f);

	}

}
