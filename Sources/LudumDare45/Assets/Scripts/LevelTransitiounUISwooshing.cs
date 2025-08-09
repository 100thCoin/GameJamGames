using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitiounUISwooshing : MonoBehaviour {

	public GameObject[] RingEdge;
	public GameObject Ring;
	public GameObject[] Dots;

	public TextMesh LevelTitle;

	public float Timer;
	public float Timer2;

	public bool PreLevelEnter;
	public bool PrePreLevelEnter;
	public bool LevelEnter;
	public bool LevelClear;

	public bool OpenMap;

	public float RingSize;
	public float DotPos;
	public float TitlePos;


	public bool[] Step;

	public DataHolder Main;
	public GameObject Player;

	public bool DeadWhoop;

	public bool LeavingTitle;

	// Use this for initialization
	void Start () {
		
	}
	
	void Update () {

		if(PrePreLevelEnter)
		{
			Timer+= Time.deltaTime;
			Timer2+= Time.deltaTime;

			if(Step[0])
			{
				if(Timer > 0)
				{
					RingEdge[0].GetComponent<SpriteRenderer>().sortingOrder = 6;
					RingEdge[1].GetComponent<SpriteRenderer>().sortingOrder = 6;
					RingEdge[2].GetComponent<SpriteRenderer>().sortingOrder = 6;
					RingEdge[3].GetComponent<SpriteRenderer>().sortingOrder = 6;
					Ring.GetComponent<SpriteRenderer>().sortingOrder = 6;
					Step[0] = false;
					Step[1] = true;
					Timer = 0;
					Timer2 = -1;
					Main.DimMusic = true;
				}
			}

			if(Step[1])
			{
				float duration = 2f;
				float prevscale = 4;
				float targestscale = 0;

				RingSize = (((prevscale - targestscale) * Mathf.Pow(Timer,2))/Mathf.Pow(duration,2) - ((2*prevscale - 2*targestscale) * Timer)/duration + prevscale);
				if(Timer > duration)
				{
					RingSize = targestscale;
					Step[1] = false;
					Timer = 0;
					PrePreLevelEnter = false;
					Main.ReadyLevel();
					Main.Aud.clip = null;
					Main.DimMusic = false;

				}
			}
			RingEdge[0].transform.localPosition = new Vector3(0,8*RingSize + 20.4f,0);
			RingEdge[1].transform.localPosition = new Vector3(0,-8*RingSize - 20.4f,0);
			RingEdge[2].transform.localPosition = new Vector3(8*RingSize + 20.4f,0,0);
			RingEdge[3].transform.localPosition = new Vector3(-8*RingSize - 20.4f,0,0);
			Ring.transform.localScale = new Vector3(RingSize,RingSize,RingSize);
		}




		if(PreLevelEnter)
		{
			Timer+= Time.deltaTime;

			if(Step[0])
			{
				Instantiate(Main.LevelIntro[Main.Level],gameObject.transform.position,gameObject.transform.rotation,gameObject.transform);

				RingEdge[0].GetComponent<SpriteRenderer>().sortingOrder = 0;
				RingEdge[1].GetComponent<SpriteRenderer>().sortingOrder = 0;
				RingEdge[2].GetComponent<SpriteRenderer>().sortingOrder = 0;
				RingEdge[3].GetComponent<SpriteRenderer>().sortingOrder = 0;
				Ring.GetComponent<SpriteRenderer>().sortingOrder = 0;


				Main.LockCamera = true;
				Player.transform.position = new Vector3(-25,0,0);
				Player.GetComponent<PlayerController>().AutomovingLevelIntro = true;
				Step[0] = false;
				Step[1] = true;
			}

			if(Step[1])
			{
				if(Timer > 1)
				{
					Step[1] = false;
					Step[2] = true;
					Timer = 0;
				}
			}

			if(Step[2])
			{
				float durationDOT = 1f;
				float prevscaleDOT = 1f;
				float targestscaleDOT = 0f;
				TitlePos = (((prevscaleDOT - targestscaleDOT) * Mathf.Pow(Timer,2))/Mathf.Pow(durationDOT,2) - ((2*prevscaleDOT - 2*targestscaleDOT) * Timer)/durationDOT + prevscaleDOT);

				if(Timer > durationDOT)
				{
					TitlePos = targestscaleDOT;
					Step[2] = false;
					Step[3] = true;
					Timer = 0f;
				}
			}

			if(Step[3])
			{
				if(Timer > 2)
				{
					Step[3] = false;
					Step[0] = true;
					Timer = 0f;
					PreLevelEnter = false;
					LevelEnter = true;
				}
			}



			DotPos = 3;
			RingSize = 0;
			RingEdge[0].transform.localPosition = new Vector3(0,8*RingSize + 20.4f,0);
			RingEdge[1].transform.localPosition = new Vector3(0,-8*RingSize - 20.4f,0);
			RingEdge[2].transform.localPosition = new Vector3(8*RingSize + 20.4f,0,0);
			RingEdge[3].transform.localPosition = new Vector3(-8*RingSize - 20.4f,0,0);
			Ring.transform.localScale = new Vector3(RingSize,RingSize,RingSize);
			Dots[0].transform.localPosition = new Vector3(0,8*DotPos + 14.4f,0);
			Dots[1].transform.localPosition = new Vector3(0,-8*DotPos - 14.4f,0);
			LevelTitle.transform.localPosition = new Vector3(0,8*TitlePos + 6f,-5);
		}


		if(LevelEnter)
		{
			Timer+= Time.deltaTime;
			Timer2+= Time.deltaTime;

			if(Step[0])
			{
				if(Timer > 0)
				{
					Step[0] = false;
					Step[1] = true;
					Step[2] = true;
					Timer = 0;
					Timer2 = -1;
					Main.DimMusic = false;

				}
			}

			if(Step[1])
			{
				float duration = 0.666f;
				float prevscale = 0;
				float targestscale = 0.666f;

				RingSize = (((prevscale - targestscale) * Mathf.Pow(Timer,2))/Mathf.Pow(duration,2) - ((2*prevscale - 2*targestscale) * Timer)/duration + prevscale);
				if(Timer > duration)
				{
					RingSize = targestscale;
					Step[1] = false;
				}
			}
			if(Step[2])
			{

				//but it's position and not scale
				float durationDOT = 1f;
				float prevscaleDOT = 8;
				float targestscaleDOT = 0.5f;
				DotPos = (((prevscaleDOT - targestscaleDOT) * Mathf.Pow(Timer,2))/Mathf.Pow(durationDOT,2) - ((2*prevscaleDOT - 2*targestscaleDOT) * Timer)/durationDOT + prevscaleDOT);

				if(Timer > 0.7f)
				{
					LevelTitle.color = new Vector4(0,0,0,1);
				}


				if(Timer > durationDOT)
				{
					Step[3] = true;
				}

				if(Timer > durationDOT+0.2f)
				{
					//RingSize = targestscaleDOT;
					Step[2] = false;
					Timer = 0;
				}
			}

			if(Step[3])
			{
				float duration = 0.333f;
				float prevscale = 4;
				float targestscale = 0.666f;

				RingSize = (((prevscale - targestscale) * Mathf.Pow(Timer2,2))/Mathf.Pow(duration,2) - ((2*prevscale - 2*targestscale) * Timer2)/duration + prevscale);
				RingSize = 4.666f-RingSize;
				if(Timer2 > duration)
				{
					RingSize = prevscale;
					Step[3] = false;
					Step[4] = true;
					Timer = 0f;
					Timer2 = 0;
				}
			}

			if(Step[4])
			{
				if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
				{
					Step[4] = false;
					Step[5] = true;
					Timer = 0f;
					Timer2 = 0;
					Main.LockCamera = false;
					Player.GetComponent<PlayerController>().AutomovingLevelIntro = false;
					Main.Aud.clip = Main.LevelMusic[Main.Level];
					Main.Aud.enabled = false; Main.Aud.enabled = true;

				}
			}

			if(Step[5])
			{

				//but it's position and not scale
				float durationDOT = 2f;
				float prevscaleDOT = 3f;
				float targestscaleDOT = 0.84f;
				DotPos = (((prevscaleDOT - targestscaleDOT) * Mathf.Pow(Timer,2))/Mathf.Pow(durationDOT,2) - ((2*prevscaleDOT - 2*targestscaleDOT) * Timer)/durationDOT + prevscaleDOT);
				DotPos = 3.84f - DotPos;
				TitlePos = DotPos-0.84f;
				if(Timer > durationDOT)
				{
					//RingSize = targestscaleDOT;
					Step[5] = false;
					Timer = 0;
					LevelEnter = false;
				}
			}




			RingEdge[0].transform.localPosition = new Vector3(0,8*RingSize + 20.4f,0);
			RingEdge[1].transform.localPosition = new Vector3(0,-8*RingSize - 20.4f,0);
			RingEdge[2].transform.localPosition = new Vector3(8*RingSize + 20.4f,0,0);
			RingEdge[3].transform.localPosition = new Vector3(-8*RingSize - 20.4f,0,0);
			Ring.transform.localScale = new Vector3(RingSize,RingSize,RingSize);
			Dots[0].transform.localPosition = new Vector3(0,8*DotPos + 14.4f,0);
			Dots[1].transform.localPosition = new Vector3(0,-8*DotPos - 14.4f,0);
			LevelTitle.transform.localPosition = new Vector3(0,8*TitlePos + 6f,-5);
			//Dots[3].transform.localPosition = new Vector3(8*DotPos*((Screen.width + 0f)/(Screen.height+0f)) + 14.4f,0,0);
			//Dots[2].transform.localPosition = new Vector3(-8*DotPos*((Screen.width + 0f)/(Screen.height+0f)) - 14.4f,0,0);

		}


		if(LevelClear)
		{

			Timer+= Time.deltaTime;
			Timer2+= Time.deltaTime;

			if(Step[0])
			{

				if(Timer > 2)
				{
					RingEdge[0].GetComponent<SpriteRenderer>().sortingOrder = 6;
					RingEdge[1].GetComponent<SpriteRenderer>().sortingOrder = 6;
					RingEdge[2].GetComponent<SpriteRenderer>().sortingOrder = 6;
					RingEdge[3].GetComponent<SpriteRenderer>().sortingOrder = 6;
					Ring.GetComponent<SpriteRenderer>().sortingOrder = 6;
					Step[0] = false;
					Step[1] = true;
					Step[2] = true;
					Timer = 0;
					Timer2 = -1;

				}
			}

			if(Step[1])
			{
				float duration = 1f;
				float prevscale = 4;
				float targestscale = 0.666f;

				RingSize = (((prevscale - targestscale) * Mathf.Pow(Timer,2))/Mathf.Pow(duration,2) - ((2*prevscale - 2*targestscale) * Timer)/duration + prevscale);
				if(Timer > duration)
				{
					RingSize = targestscale;
					Step[1] = false;
					Step[2] = true;
					Timer = 0;
				}
			}
			if(Step[2])
			{
				if(Timer > 1)
				{
					Step[2] = false;
					Step[3] = true;
					Timer = 0;
					Main.DimMusic = true;

				}
			}

			if(Step[3])
			{
				float duration = 1f;
				float prevscale = 0.666f;
				float targestscale = 0;

				RingSize = (((prevscale - targestscale) * Mathf.Pow(Timer,2))/Mathf.Pow(duration,2) - ((2*prevscale - 2*targestscale) * Timer)/duration + prevscale);
				if(Timer > duration)
				{
					RingSize = targestscale;
					Step[3] = false;
					LevelClear = false;
					OpenMap = true;
					Main.ExitLevel();
					Main.MapScript.ReadyMap(Main.Level);
					Step[0] = true;

				}
			}


			RingEdge[0].transform.localPosition = new Vector3(0,8*RingSize + 20.4f,0);
			RingEdge[1].transform.localPosition = new Vector3(0,-8*RingSize - 20.4f,0);
			RingEdge[2].transform.localPosition = new Vector3(8*RingSize + 20.4f,0,0);
			RingEdge[3].transform.localPosition = new Vector3(-8*RingSize - 20.4f,0,0);
			Ring.transform.localScale = new Vector3(RingSize,RingSize,RingSize);
		}


		if(OpenMap)
		{
			Timer+= Time.deltaTime;
			Timer2+= Time.deltaTime;

			if(Step[0])
			{
				if(Timer > 1)
				{
					RingEdge[0].GetComponent<SpriteRenderer>().sortingOrder = 6;
					RingEdge[1].GetComponent<SpriteRenderer>().sortingOrder = 6;
					RingEdge[2].GetComponent<SpriteRenderer>().sortingOrder = 6;
					RingEdge[3].GetComponent<SpriteRenderer>().sortingOrder = 6;
					Ring.GetComponent<SpriteRenderer>().sortingOrder = 6;
					Step[0] = false;
					Step[1] = true;
					Timer = 0;
					Timer2 = -1;
					Main.DimMusic = false;

					if(Main.Level != 0)
					{
						Main.Aud.clip = Main.Map;
						Main.Aud.enabled = false; Main.Aud.enabled = true;
					}

				}
			}

			if(Step[1])
			{
				float duration = 2f;
				float prevscale = 0;
				float targestscale = 4;

				RingSize = (((prevscale - targestscale) * Mathf.Pow(Timer,2))/Mathf.Pow(duration,2) - ((2*prevscale - 2*targestscale) * Timer)/duration + prevscale);
				if(Timer > duration)
				{
					RingSize = targestscale;
					Step[1] = false;
					Timer = 0;
					OpenMap = false;
				}
			}
			RingEdge[0].transform.localPosition = new Vector3(0,8*RingSize + 20.4f,0);
			RingEdge[1].transform.localPosition = new Vector3(0,-8*RingSize - 20.4f,0);
			RingEdge[2].transform.localPosition = new Vector3(8*RingSize + 20.4f,0,0);
			RingEdge[3].transform.localPosition = new Vector3(-8*RingSize - 20.4f,0,0);
			Ring.transform.localScale = new Vector3(RingSize,RingSize,RingSize);
		}


		if(DeadWhoop)
		{
			Timer+= Time.deltaTime;
			Timer2+= Time.deltaTime;

			if(Step[0])
			{
				if(Timer > 1)
				{
					RingEdge[0].GetComponent<SpriteRenderer>().sortingOrder = 6;
					RingEdge[1].GetComponent<SpriteRenderer>().sortingOrder = 6;
					RingEdge[2].GetComponent<SpriteRenderer>().sortingOrder = 6;
					RingEdge[3].GetComponent<SpriteRenderer>().sortingOrder = 6;
					Ring.GetComponent<SpriteRenderer>().sortingOrder = 6;
					Step[0] = false;
					Step[1] = true;
					Timer = 0;
					Timer2 = -1;

				}
			}

			if(Step[1])
			{
				float duration = 1f;
				float prevscale = 4;
				float targestscale = 0;

				RingSize = (((prevscale - targestscale) * Mathf.Pow(Timer,2))/Mathf.Pow(duration,2) - ((2*prevscale - 2*targestscale) * Timer)/duration + prevscale);
				if(Timer > duration)
				{
					RingSize = targestscale;
					Step[1] = false;
					Step[2] = true;

					Timer = 0;
					Main.HeldPickup = 0;
					Main.HasKey = false;
					Main.UnlockedSword = false;
					Main.ReloadLevel();
					GameObject.Find("Main Camera").transform.parent.GetComponent<CameraControl>().Player = GameObject.Find("Player");
					GameObject.Find("Main Camera").transform.parent.GetComponent<CameraControl>().Target = GameObject.Find("Player");
				}
			}

			if(Step[2])
			{
				float duration = 0.25f;

				if(Timer > duration)
				{
					Step[2] = false;
					Step[3] = true;

					Timer = 0;
				}
			}

			if(Step[3])
			{
				float duration = 1f;
				float prevscale = 0;
				float targestscale = 4;

				RingSize = (((prevscale - targestscale) * Mathf.Pow(Timer,2))/Mathf.Pow(duration,2) - ((2*prevscale - 2*targestscale) * Timer)/duration + prevscale);
				if(Timer > duration)
				{
					RingSize = targestscale;
					Step[3] = false;
					DeadWhoop = false;
					Timer = 0;
				}
			}


			RingEdge[0].transform.localPosition = new Vector3(0,8*RingSize + 20.4f,0);
			RingEdge[1].transform.localPosition = new Vector3(0,-8*RingSize - 20.4f,0);
			RingEdge[2].transform.localPosition = new Vector3(8*RingSize + 20.4f,0,0);
			RingEdge[3].transform.localPosition = new Vector3(-8*RingSize - 20.4f,0,0);
			Ring.transform.localScale = new Vector3(RingSize,RingSize,RingSize);
		}


		if(LeavingTitle)
		{
			Timer+= Time.deltaTime;
			Timer2+= Time.deltaTime;

			if(Step[0])
			{
				if(Timer > 0)
				{
					RingEdge[0].GetComponent<SpriteRenderer>().sortingOrder = 6;
					RingEdge[1].GetComponent<SpriteRenderer>().sortingOrder = 6;
					RingEdge[2].GetComponent<SpriteRenderer>().sortingOrder = 6;
					RingEdge[3].GetComponent<SpriteRenderer>().sortingOrder = 6;
					Ring.GetComponent<SpriteRenderer>().sortingOrder = 6;
					Step[0] = false;
					Step[1] = true;
					Timer = 0;
					Timer2 = -1;

				}
			}

			if(Step[1])
			{

				if(Timer > 1)
				{
					Main.DimMusic = true;
				}

				float duration = 2f;
				float prevscale = 4;
				float targestscale = 0;

				RingSize = (((prevscale - targestscale) * Mathf.Pow(Timer,2))/Mathf.Pow(duration,2) - ((2*prevscale - 2*targestscale) * Timer)/duration + prevscale);
				if(Timer > duration)
				{
					RingSize = targestscale;
					Step[1] = false;
					Timer = 0;
					Main.Lore.SetActive(true);
					LeavingTitle = false;

					Main.DimMusic = false;
					Main.Aud.clip = Main.Map;
					Main.Aud.enabled = false; Main.Aud.enabled = true;

					Destroy(Main.TitleAud);

				}
			}
			RingEdge[0].transform.localPosition = new Vector3(0,8*RingSize + 20.4f,0);
			RingEdge[1].transform.localPosition = new Vector3(0,-8*RingSize - 20.4f,0);
			RingEdge[2].transform.localPosition = new Vector3(8*RingSize + 20.4f,0,0);
			RingEdge[3].transform.localPosition = new Vector3(-8*RingSize - 20.4f,0,0);
			Ring.transform.localScale = new Vector3(RingSize,RingSize,RingSize);
		}



	}
}
