using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour {

	public Transform Player;

	public MoveCharacter Mov;


	public float Timer;

	public float Distance;
	public Transform Home;

	public float Scale;
	public Camera Cam;

	public GameObject HouseUI;

	public Transform TownNameUI;
	float SignHeight;

	public GameObject Sign;
	public float LowersignDelay;

	public Transform Dragon;
	public bool DragonArenaMode;
	public Dataholder Main;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if(Main.Paused)
		{
			return;
		}
		if(DragonArenaMode && Dragon != null || !DragonArenaMode)
		{
			transform.position = new Vector3(Player.transform.position.x,Player.transform.position.y,-100);
			Distance = (new Vector3(Home.position.x,Home.position.y,0) - new Vector3(transform.position.x,transform.position.y,0)).magnitude;
		}
		if(!DragonArenaMode)
		{
			if(Distance > 31)
			{
				if(Timer > 0)
				{
					Timer -= Time.deltaTime;
				}

					HouseUI.SetActive(true);
			}
			else
			{
				if(Timer < 1)
				{
					Timer += Time.deltaTime;

					LowersignDelay = 5;
					Sign.SetActive(true);

				}
				else
				{
					HouseUI.SetActive(false);
				}
			}
			Scale = -Mathf.Cos(3.141592f*Timer)*0.5f*(8-16)+0.5f*(8+16);
			SignHeight = -Mathf.Cos(3.141592f*Timer)*0.5f*(5.125f-17.75f)+0.5f*(17.75f+5.125f)+20.5f;
			TownNameUI.position = new Vector3(TownNameUI.position.x,SignHeight,TownNameUI.position.z);
			Cam.orthographicSize = Scale;
		}
		else
		{
			Sign.SetActive(false);

		}
		LowersignDelay -= Time.deltaTime;
		if(LowersignDelay < 0)
		{
			Sign.SetActive(false);
		}

		if(Distance > 100)
		{

			Vector2 temp = new Vector2(Home.transform.position.x-transform.position.x,Home.transform.position.y-transform.position.y).normalized * 1.2f;

			Player.GetComponent<MoveCharacter>().VelX = temp.x;
			Player.GetComponent<MoveCharacter>().VelY = temp.y;

		}


		if(DragonArenaMode)
		{

			if(Distance > 31)
			{
				Vector2 temp = new Vector2(Home.transform.position.x-transform.position.x,Home.transform.position.y-transform.position.y).normalized * 1.2f;

				Player.GetComponent<MoveCharacter>().VelX = temp.x;
				Player.GetComponent<MoveCharacter>().VelY = temp.y;
			}

			Cam.orthographicSize = 16;
			if(Dragon != null)
			{
				transform.position = new Vector3((Dragon.position.x + Player.transform.position.x)/2f,(Dragon.position.y + Player.transform.position.y)/2f,-100);
			}

		}

	}
}
