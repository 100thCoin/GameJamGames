using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeremonyPlayer : MonoBehaviour {

	public GameObject FireParticles;

	public float ConstantTimer;
	public float ZoomTimer;
	public bool[] Checkpoint;
	public bool[] CheckyCheck;

	public GameObject CameraO;

	public float DesiredZoom;
	public float CurrentZoom;
	public float PreviousZoom;
	public float ZoomDuration;
	public GameObject Fire;
	public GameObject Sacrifice;
	public GameObject LetTheGamesBegin;
	public float Speed;
	public GameObject WhooopSkip;
	public GameObject Whooop;

	public bool SKip;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(SKip)
		{
			SKip = false;
			Instantiate(WhooopSkip,new Vector3(CameraO.gameObject.transform.position.x,gameObject.transform.position.y + 3,gameObject.transform.position.z),Fire.gameObject.transform.rotation);

		}


		ConstantTimer+= Time.deltaTime;
		if(ConstantTimer >1 && !Checkpoint[0] && !CheckyCheck[0])
		{
			Checkpoint[0] = true;
			ZoomTimer =0;
			PreviousZoom = 8;
			CurrentZoom =8;
			DesiredZoom = 3;
			ZoomDuration = 1;

		}
		if(Checkpoint[0])
		{
			ZoomTimer += Time.deltaTime;
			CurrentZoom = ((((PreviousZoom-2*((DesiredZoom + 2 - (0.5f*PreviousZoom))-2)))/2)*Mathf.Pow((1/ZoomDuration)*ZoomTimer,2)-(PreviousZoom-2*((DesiredZoom + 2 - (0.5f*PreviousZoom))-2))*(1/ZoomDuration)*ZoomTimer+(PreviousZoom));
			CameraO.gameObject.GetComponent<Camera>().orthographicSize = CurrentZoom;
			if(ZoomTimer >= ZoomDuration)
			{
				Checkpoint[0] = false;
				CheckyCheck[0] = true;
			}
		}
		if(ConstantTimer >2 && !Checkpoint[1] && !CheckyCheck[1])
		{
			Checkpoint[1] = true;
		}
		if(Checkpoint[1])
		{
			gameObject.transform.position = new Vector2(gameObject.transform.position.x + Time.deltaTime * 13f,gameObject.transform.position.y);
			if(gameObject.transform.position.x >= 13)
			{
				Checkpoint[1] = false;
				CheckyCheck[1] = true;

			}
		}
		if(ConstantTimer >3.5f && !Checkpoint[2] && !CheckyCheck[2])
		{
			Checkpoint[2] = true;
			ZoomTimer =0;
			PreviousZoom = 3;
			CurrentZoom =3;
			DesiredZoom = 8;
			ZoomDuration = 1;

		}
		if(Checkpoint[2])
		{
			ZoomTimer += Time.deltaTime;
			CurrentZoom = ((((PreviousZoom-2*((DesiredZoom + 2 - (0.5f*PreviousZoom))-2)))/2)*Mathf.Pow((1/ZoomDuration)*ZoomTimer,2)-(PreviousZoom-2*((DesiredZoom + 2 - (0.5f*PreviousZoom))-2))*(1/ZoomDuration)*ZoomTimer+(PreviousZoom));
			CameraO.gameObject.GetComponent<Camera>().orthographicSize = CurrentZoom;
			if(ZoomTimer >= ZoomDuration)
			{
				Checkpoint[2] = false;
				CheckyCheck[2] = true;
			}
		}
		if(ConstantTimer >4.5f && !Checkpoint[3] && !CheckyCheck[3])
		{
			Checkpoint[3] = true;
			CheckyCheck[3] = true;

			Instantiate(FireParticles,Fire.gameObject.transform.position,Fire.gameObject.transform.rotation,gameObject.transform.parent);
		}



		if(ConstantTimer >5.5f && !Checkpoint[4] && !CheckyCheck[4])
		{
			Checkpoint[4] = true;
			ZoomTimer =0;
			PreviousZoom = 8;
			CurrentZoom =8;
			DesiredZoom = 3;
			ZoomDuration = 1;

		}
		if(Checkpoint[4])
		{
			ZoomTimer += Time.deltaTime;
			CurrentZoom = ((((PreviousZoom-2*((DesiredZoom + 2 - (0.5f*PreviousZoom))-2)))/2)*Mathf.Pow((1/ZoomDuration)*ZoomTimer,2)-(PreviousZoom-2*((DesiredZoom + 2 - (0.5f*PreviousZoom))-2))*(1/ZoomDuration)*ZoomTimer+(PreviousZoom));
			CameraO.gameObject.GetComponent<Camera>().orthographicSize = CurrentZoom;
			if(ZoomTimer >= ZoomDuration)
			{
				Checkpoint[4] = false;
				CheckyCheck[4] = true;
			}
		}
		if(ConstantTimer >6.5f && !Checkpoint[5] && !CheckyCheck[5])
		{
			Checkpoint[5] = true;
		}
		if(Checkpoint[5])
		{
			gameObject.transform.position = new Vector2(gameObject.transform.position.x + Time.deltaTime * 13f,gameObject.transform.position.y);
			if(gameObject.transform.position.x >= 30)
			{
				Checkpoint[5] = false;
				CheckyCheck[5] = true;

			}
		}
		if(ConstantTimer >8.5f && !Checkpoint[6] && !CheckyCheck[6])
		{
			Checkpoint[6] = true;
			ZoomTimer =0;
			PreviousZoom = 3;
			CurrentZoom =3;
			DesiredZoom = 8;
			ZoomDuration = 1;

		}
		if(Checkpoint[6])
		{
			ZoomTimer += Time.deltaTime;
			CurrentZoom = ((((PreviousZoom-2*((DesiredZoom + 2 - (0.5f*PreviousZoom))-2)))/2)*Mathf.Pow((1/ZoomDuration)*ZoomTimer,2)-(PreviousZoom-2*((DesiredZoom + 2 - (0.5f*PreviousZoom))-2))*(1/ZoomDuration)*ZoomTimer+(PreviousZoom));
			CameraO.gameObject.GetComponent<Camera>().orthographicSize = CurrentZoom;
			if(ZoomTimer >= ZoomDuration)
			{
				Checkpoint[6] = false;
				CheckyCheck[6] = true;
			}
		}
		if(ConstantTimer >9.5f && !Checkpoint[7] && !CheckyCheck[7])
		{
			Checkpoint[7] = true;
			CheckyCheck[7] = true;

			Instantiate(FireParticles,Sacrifice.gameObject.transform.position,Fire.gameObject.transform.rotation,gameObject.transform.parent);
		}


		if(ConstantTimer >10.5f && !Checkpoint[9] && !CheckyCheck[9])
		{
			Checkpoint[9] = true;
		}
		if(Checkpoint[9])
		{
			gameObject.transform.position = new Vector2(gameObject.transform.position.x + Time.deltaTime * 13f,gameObject.transform.position.y);
			if(gameObject.transform.position.x >= 43)
			{
				Checkpoint[9] = false;
				CheckyCheck[9] = true;

			}
		}
		if(ConstantTimer >12.5f && !Checkpoint[11] && !CheckyCheck[11])
		{
			Checkpoint[11] = true;
			CheckyCheck[11] = true;

			Instantiate(LetTheGamesBegin,new Vector3(gameObject.transform.position.x,gameObject.transform.position.y + 3,gameObject.transform.position.z),Fire.gameObject.transform.rotation,gameObject.transform.parent);
			CameraO.GetComponent<CameraMover>().Ceremony = false;
		
		}
		if(CheckyCheck[11])
		{
			Speed += Time.deltaTime * 5;
			gameObject.transform.position = new Vector3(gameObject.transform.position.x +Speed,gameObject.gameObject.transform.position.y,gameObject.transform.position.z);

		}
		if(ConstantTimer >15.5f && !Checkpoint[12] && !CheckyCheck[12])
		{
			Checkpoint[12] = true;
			CheckyCheck[12] = true;

			Instantiate(Whooop,new Vector3(CameraO.gameObject.transform.position.x,gameObject.transform.position.y + 3,gameObject.transform.position.z),Fire.gameObject.transform.rotation);

		}
	}
}
