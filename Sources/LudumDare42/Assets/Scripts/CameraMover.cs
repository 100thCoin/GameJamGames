using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour {

	public bool Ceremony;
	public bool Event100M;
	public bool Event25M;
	public bool EventHurdles;
	public bool EventJetpack;
	public bool EventFreefall;
	public bool EventLongJump;
	public bool EventDiving;

	public float FreefallTimer;

	public GameObject Player;
	public GameObject[] PlayerList;

	public int EventNum;
	public int LastEventNum;
	public GameObject[] EventList;
	public GameObject EventTitle;

	public GameObject FailureWhoop;
	public bool FailOnce;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {

		if(Player == null)
		{
			Ceremony = false;
			Event100M = false;
			Event25M = false;
			EventHurdles = false;
			EventJetpack = false;
			EventFreefall = false;
			EventLongJump = false;
			EventDiving = false;

			if(!FailOnce)
			{

				FailOnce = true;
				StartCoroutine(Failure());
			}

		}





		if(LastEventNum != EventNum)
		{
			Destroy(EventList[LastEventNum].gameObject);
			LastEventNum = EventNum;
			Player = PlayerList[EventNum];
			EventList[EventNum].gameObject.SetActive(true);
			Ceremony = false;
			Event100M = false;
			Event25M = false;
			EventHurdles = false;
			EventJetpack = false;
			EventFreefall = false;
			EventLongJump = false;
			EventDiving = false;

			if(EventNum ==0){EventHurdles = true;EventTitle.gameObject.GetComponent<TextMesh>().text = "100m Hurdles";}
			if(EventNum ==2){Event100M = true;EventTitle.gameObject.GetComponent<TextMesh>().text = "100m Dash";}
			if(EventNum ==1){Event25M = true;EventTitle.gameObject.GetComponent<TextMesh>().text = "25m Climb";}
			if(EventNum ==3){EventLongJump = true;EventTitle.gameObject.GetComponent<TextMesh>().text = "Long Jump";}
			if(EventNum ==4){EventJetpack = true;EventTitle.gameObject.GetComponent<TextMesh>().text = "Jetpacking";}
			if(EventNum ==5){EventFreefall = true;EventTitle.gameObject.GetComponent<TextMesh>().text = "Freefall Sprint";}
			if(EventNum ==6){EventDiving = true;EventTitle.gameObject.GetComponent<TextMesh>().text = "Diving";}




		}



		if(Ceremony)
		{
			gameObject.transform.position = new Vector3(Player.gameObject.transform.position.x,Player.gameObject.transform.position.y,-10);

		}
		else if(Event100M)
		{ 
			Player = PlayerList[2];
			gameObject.GetComponent<Camera>().orthographicSize = 8;

			gameObject.transform.position = new Vector3(Player.gameObject.transform.position.x + 6,0,-10);
			if(gameObject.transform.position.x <0)
			{
				gameObject.transform.position = new Vector3(0,0,-10);
			}
			else if(gameObject.transform.position.x >93)
			{
				gameObject.transform.position = new Vector3(93,0,-10);
			}
		}
		else if(EventHurdles)
		{
			Player = PlayerList[0];

			gameObject.GetComponent<Camera>().orthographicSize = 8;

			gameObject.transform.position = new Vector3(Player.gameObject.transform.position.x + 6,0,-10);
			if(gameObject.transform.position.x <0)
			{
				gameObject.transform.position = new Vector3(0,0,-10);
			}
			else if(gameObject.transform.position.x >93)
			{
				gameObject.transform.position = new Vector3(93,0,-10);
			}
		}
		else if(EventLongJump)
		{
			Player = PlayerList[3];

			gameObject.GetComponent<Camera>().orthographicSize = 8;

			gameObject.transform.position = new Vector3(Player.gameObject.transform.position.x + 6,0,-10);
			if(gameObject.transform.position.x <0)
			{
				gameObject.transform.position = new Vector3(0,0,-10);
			}
			else if(gameObject.transform.position.x >143)
			{
				gameObject.transform.position = new Vector3(143,0,-10);
			}
		}
		else if(EventJetpack)
		{
			Player = PlayerList[4];

			gameObject.GetComponent<Camera>().orthographicSize = 10.5f;

			gameObject.transform.position = new Vector3(Player.gameObject.transform.position.x + 6,Player.gameObject.transform.position.y,-10);
			if(gameObject.transform.position.x <0)
			{
				gameObject.transform.position = new Vector3(0,Player.gameObject.transform.position.y,-10);
			}
			else if(gameObject.transform.position.x >148)
			{
				gameObject.transform.position = new Vector3(148,Player.gameObject.transform.position.y,-10);
			}
		}
		else if(Event25M)
		{
			Player = PlayerList[1];

			gameObject.GetComponent<Camera>().orthographicSize = 10.5f;
			gameObject.transform.position = new Vector3(0,Player.gameObject.transform.position.y,-10);
			if(gameObject.transform.position.y <5)
			{
				gameObject.transform.position = new Vector3(0,5,-10);
			}
			else if(gameObject.transform.position.y >54)
			{
				gameObject.transform.position = new Vector3(0,54,-10);
			}

		}
		else if(EventFreefall)
		{
			Player = PlayerList[5];

			FreefallTimer+= Time.deltaTime;

			float FreefallRotation =-90;
			float Duration = 2;

			if(FreefallTimer >0)
			{
			FreefallRotation = ((((-90-2*((0 + 2 - (0.5f*-90))-2)))/2)*Mathf.Pow((1/Duration)*FreefallTimer,2)-(-90-2*((0 + 2 - (0.5f*-90))-2))*(1/Duration)*FreefallTimer+(-90));
			}


			gameObject.transform.eulerAngles = new Vector3(0,0,FreefallRotation);
			if(FreefallTimer >= Duration)
			{
				FreefallTimer = Duration;
				Player.gameObject.GetComponent<PlayerMovement>().NoMoving = false;
			}

			gameObject.GetComponent<Camera>().orthographicSize = 8;
			gameObject.transform.position = new Vector3(0,0,-10);
		}
		else if(EventDiving)
		{
			Player = PlayerList[6];


			gameObject.GetComponent<Camera>().orthographicSize = 8;
			gameObject.transform.position = new Vector3(0,0,-10);
		}
	}


	IEnumerator Failure()
	{
		yield return new WaitForSeconds(1);
		Instantiate(FailureWhoop,new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,gameObject.transform.position.z+5),gameObject.transform.rotation);


	}

}
