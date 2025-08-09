using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenuFunctions : MonoBehaviour {

	public bool Credits;
	public bool LeaveCreds;
	public bool Quit;
	public bool Play;

	public GameObject Cam;

	public GameObject EntireTitle;

	public DataHolder Main;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseOver()
	{
		if(Input.GetKeyDown(KeyCode.Mouse0) && !Main.StartedGame)
		{
			if(Credits)
			{
				Cam.transform.localPosition = new Vector3(0,-28,-30);
			}
			else if(LeaveCreds)
			{
				Cam.transform.localPosition = new Vector3(0,0,-30);
			}
			else if(Quit)
			{
				Application.Quit();
			}
			else if(Play)
			{
				Main.StartedGame = true;
				Main.SwooshControl.Step[0] = true;
				Main.SwooshControl.LeavingTitle = true;
			}
		}
	}

}
