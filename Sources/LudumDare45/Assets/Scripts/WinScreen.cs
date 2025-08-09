using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour {

	public GameObject[] Text;

	public float Timer;
	public bool[] Step;

	public DataHolder Main;

	public bool JustLore;

	public GameObject EntireGame;

	// Use this for initialization
	void OnEnable () {

		Step[0] = true;
		if(!JustLore)
		{
			GameObject.Find("Main").GetComponent<DataHolder>().Victory = true;
		}
	}
	
	// Update is called once per frame
	void Update () {

		Timer += Time.deltaTime;
		if(!JustLore)
		{
			if(Step[0])
			{
				if(Timer > 2)
				{
					Text[0].SetActive(true);
					Timer = 0;
					Step[0] = false;
					Step[1] = true;
				}
			}

			if(Step[1])
			{
				if(Timer > 3)
				{
					Text[1].SetActive(true);
					Timer = 0;
					Step[1] = false;
					Step[2] = true;
				}
			}
			if(Step[2])
			{
				if(Timer > 1)
				{
					Text[2].SetActive(true);
					Text[2].GetComponent<TextMesh>().text = "" + Main.Deaths;
					Timer = 0;
					Step[2] = false;
					Step[3] = true;
				}
			}
			if(Step[3])
			{
				if(Timer > 1)
				{
					Text[3].SetActive(true);
					Timer = 0;
					Step[3] = false;
					Step[4] = true;
				}
			}
			if(Step[4])
			{
				if(Timer > 1)
				{
					Text[4].SetActive(true);
					string s = "" + Mathf.Round(Main.Speedruntime*100)*0.01f;
					if(Main.Speedruntime < 10)
					{
						s= "0" + s;
					}
					Text[4].GetComponent<TextMesh>().text = "" + Main.Speedruntime_M + ":" + s;
					Timer = 0;
					Step[4] = false;
					Step[5] = true;
				}
			}
			if(Step[5])
			{
				if(Timer > 1)
				{
					Text[5].SetActive(true);
					Timer = 0;
					Step[5] = false;
					Step[6] = true;
				}
			}
			if(Step[6])
			{
				if(Timer > 1)
				{
					Text[6].SetActive(true);
					Timer = 0;
					Step[6] = false;
					Step[7] = true;
				}
			}
			if(Step[7])
			{
				if(Timer > 2)
				{
					Text[7].SetActive(true);
					Timer = 0;
					Step[7] = false;
					Step[8] = true;
				}
			}
			if(Step[8])
			{
				//press f to go to title
				if(Input.GetKeyDown(KeyCode.F))
				{
					print("hello?");
					GameObject EG = Instantiate(EntireGame,Vector3.zero,gameObject.transform.rotation);
					Main.gameObject.name = "Mehn";
					EG.name = "Main";
					Destroy(GameObject.Find("Mehn"));
				}


			}
		}
		else
		{

			if(Step[0])
			{
				if(Timer > 0)
				{
					Text[0].SetActive(true);
					Timer = 0;
					Step[0] = false;
					Step[1] = true;
				}
			}

			if(Step[1])
			{
				Text[0].GetComponent<TextMesh>().color = new Vector4(1,1,1,Timer);
				if(Timer > 3)
				{
					Text[1].SetActive(true);
					Timer = 0;
					Step[1] = false;
					Step[2] = true;
				}
			}

			if(Step[2])
			{
				Text[1].GetComponent<TextMesh>().color = new Vector4(1,1,1,Timer);
				if(Timer > 3)
				{
					Text[2].SetActive(true);
					Timer = 0;
					Step[2] = false;
					Step[3] = true;
				}
			}

			if(Step[3])
			{
				Text[2].GetComponent<TextMesh>().color = new Vector4(1,1,1,Timer);
				if(Timer > 3)
				{
					Text[3].SetActive(true);
					Timer = 0;
					Step[3] = false;
					Step[4] = true;
				}
			}
			if(Step[4])
			{
				Text[3].GetComponent<TextMesh>().color = new Vector4(1,1,1,Timer);
				if(Timer > 3)
				{
					Text[4].SetActive(true);
					Timer = 0;
					Step[4] = false;
					Step[5] = true;
				}
			}

			if(Step[5])
			{

				//pres f
				if(Input.GetKeyDown(KeyCode.F))
				{
					
					Main.ExitLevel();
					Main.MapScript.ReadyMap(Main.Level);
					Main.SwooshControl.OpenMap = true;
					Main.SwooshControl.Step[0] = true;

					Destroy(Main.TitleScreen);
					Destroy(gameObject);
				}
			}
		}

	}
}
