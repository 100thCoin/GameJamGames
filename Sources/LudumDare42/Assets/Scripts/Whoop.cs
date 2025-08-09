using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whoop : MonoBehaviour {

	public GameObject One;
	public GameObject Two;
	public GameObject Three;
	public GameObject Four;
	public GameObject Center;
	public float STimer;
	public float GTimer;

	public bool Shrink;
	public bool Grow;

	public GameObject NextEvent;
	public GameObject CurrentEvent;

	public GameObject Cam;
	public bool HoldIt;
	public GameObject HoldObject;
	public bool HoldItPre;

	// Use this for initialization
	void Start () {

		Cam = GameObject.Find("Main Camera").gameObject;

	}
	
	// Update is called once per frame
	void Update () {
	
		if(Grow)
		{
			GTimer+= Time.deltaTime;
			Center.gameObject.transform.localScale = Vector3.one * GTimer * 8;
			One.gameObject.transform.localPosition = new Vector3(-16 - GTimer * 64,0);
			Two.gameObject.transform.localPosition = new Vector3(16 + GTimer * 64,0);
			Three.gameObject.transform.localPosition = new Vector3(0,-16 - GTimer * 64,0);
			Four.gameObject.transform.localPosition = new Vector3(0,16 + GTimer * 64);

			if(GTimer >= 0.5f)
			{
				GTimer =0.5f;
				Center.gameObject.transform.localScale = Vector3.one * GTimer * 8;
				One.gameObject.transform.localPosition = new Vector3(-16 - GTimer * 64,0);
				Two.gameObject.transform.localPosition = new Vector3(16 + GTimer * 64,0);
				Three.gameObject.transform.localPosition = new Vector3(0,-16 - GTimer * 64,0);
				Four.gameObject.transform.localPosition = new Vector3(0,16 + GTimer * 64);
				Grow = false;
				GTimer =0;


				Destroy(gameObject);





			}

		}
		if(Shrink)
		{
			STimer-= Time.deltaTime;
			Center.gameObject.transform.localScale = Vector3.one * STimer * 8;
			One.gameObject.transform.localPosition = new Vector3(-16 - STimer * 64,0);
			Two.gameObject.transform.localPosition = new Vector3(16 + STimer * 64,0);
			Three.gameObject.transform.localPosition = new Vector3(0,-16 - STimer * 64,0);
			Four.gameObject.transform.localPosition = new Vector3(0,16 + STimer * 64);
			if(STimer <= 0)
			{
				STimer =0;
				Center.gameObject.transform.localScale = Vector3.one * STimer * 8;
				One.gameObject.transform.localPosition = new Vector3(-16 - STimer * 64,0);
				Two.gameObject.transform.localPosition = new Vector3(16 + STimer * 64,0);
				Three.gameObject.transform.localPosition = new Vector3(0,-16 - STimer * 64,0);
				Four.gameObject.transform.localPosition = new Vector3(0,16 + STimer * 64);
				Shrink = false;
				STimer = 0.5f;

				//NextEvent.SetActive(true);
				Destroy(CurrentEvent);


					
				if(HoldIt)
				{
					Instantiate(HoldObject,gameObject.transform.position,gameObject.transform.rotation,gameObject.transform);
					Cam.gameObject.GetComponent<Camera>().orthographicSize = 12;
					Cam.GetComponent<CameraMover>().Player = GameObject.Find("InGame");
				}
				else if (HoldItPre)
				{
					
					Instantiate(HoldObject,new Vector3(0,0.34f,-8),gameObject.transform.rotation,gameObject.transform);
					Cam.gameObject.GetComponent<Camera>().orthographicSize = 12;
					Cam.GetComponent<CameraMover>().Player = GameObject.Find("InGame");

				}
				else
				{
				if(	Cam.gameObject.GetComponent<CameraMover>().EventNum == 7)
				{
					Cam.gameObject.GetComponent<CameraMover>().EventNum=-1;

				}
				Cam.gameObject.GetComponent<CameraMover>().EventNum++;
				gameObject.transform.position = new Vector3(0,0,-8);
					GameObject.Find("InGame").gameObject.transform.FindChild("Music").gameObject.SetActive(true);


				Grow = true;
				}

			}

		}








	}
}
