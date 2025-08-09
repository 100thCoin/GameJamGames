using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavTitle : MonoBehaviour {

	public bool Creds;
	public bool Menu;
	public bool Quit;
	public bool Play;

	public bool InGameGoBack;


	public bool Doing;

	public float XTimer;
	public float XDuration;
	public float LastXpos;
	public float TargetXpos;
	public float LastYpos;
	public float TargetYpos;

	public GameObject Cam;
	public float LastO;
	public float TargetO;

	public bool DOOnce;

	// Use this for initialization
	void Start () {
		
	}


	void FixedUpdate()
	{

		if(Doing)
		{

			XTimer += Time.fixedDeltaTime;

			//LocalXPos = ((((LastXpos-2*((TargetXpos + 2 - (0.5f*LastXpos))-2)))/2)*Mathf.Pow((1/XDuration)*XTimer,2)-(LastXpos-2*((TargetXpos + 2 - (0.5f*LastXpos))-2))*(1/XDuration)*XTimer+(LastXpos));

			float LocalXPos = (((LastXpos - TargetXpos) * Mathf.Pow(XTimer,2))/Mathf.Pow(XDuration,2) - ((2*LastXpos - 2*TargetXpos) * XTimer)/XDuration + LastXpos);
			float LocalYPos = (((LastYpos - TargetYpos) * Mathf.Pow(XTimer,2))/Mathf.Pow(XDuration,2) - ((2*LastYpos - 2*TargetYpos) * XTimer)/XDuration + LastYpos);
			float LocalOrtho = (((LastO - TargetO) * Mathf.Pow(XTimer,2))/Mathf.Pow(XDuration,2) - ((2*LastO - 2*TargetO) * XTimer)/XDuration + LastO);

			//	Y = (((a - h) * Mathf.Pow(timer,2))/Mathf.Pow(dur,2) - ((a - h) * timer)/dur + a);


			if(XTimer >= XDuration)
			{
				LocalXPos = TargetXpos;
				LocalYPos = TargetYpos;

				LocalOrtho = TargetO;

				Doing = false;
			}

			Cam.transform.position = new Vector3(LocalXPos,LocalYPos,-10);
			Cam.GetComponent<Camera>().orthographicSize = LocalOrtho;

		}

	}

	
	// Update is called once per frame
	void OnMouseOver () {

		if(Input.GetKeyDown(KeyCode.Mouse0))
		{

			if(Creds || Menu)
			{
				XTimer = 0;
				Doing = true;
				LastXpos = Cam.transform.position.x;
				LastYpos = Cam.transform.position.y;

				LastO = Cam.GetComponent<Camera>().orthographicSize;
			}


			if(Quit)
			{
				Application.Quit();
			}

			if(Play && !DOOnce)
			{
				DOOnce = true;
				gameObject.transform.parent.GetComponent<SpriteRenderer>().enabled = false;

				GameObject Load = Instantiate(Cam,new Vector3(0,0,0),transform.rotation) as GameObject;
				Load.GetComponent<StartGame>().TargetDestroy = gameObject.transform.parent.parent.gameObject;


			}

			if(InGameGoBack && !DOOnce)
			{
				DOOnce = true;

				GameObject Load = Instantiate(Cam,new Vector3(0,0,0),transform.rotation) as GameObject;
				Load.GetComponent<StartGame>().TargetDestroy = GameObject.Find("Main").gameObject;


			}


		}


	}
}
