using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour {

	public bool PauseQuit;
	public bool PauseContinue;

	public bool TitlePlay;
	public bool TitleQuit;
	public bool TitleCreds;
	public bool TitleCredBack;

	public bool DeadQuit;
	public bool DeadContinue;


	public Dataholder Main;

	public GameObject RestartEverything;

	public GameObject ThisScreen;


	void OnMouseDrag()
	{
		
		if(PauseContinue)
		{
			Main.PauseWindow.SetActive(false);
			Main.Paused = false;
		}

		if(PauseQuit)
		{			
			GameObject NewMain =Instantiate(GameObject.Find("MainMain").GetComponent<MainHolder>().Main,Vector3.zero,transform.rotation);
			NewMain.name = "Main";
			Destroy(Main.gameObject);
		}


		if(DeadContinue)
		{
			Main.LoadTheDragon.Unload = true;
			Main.MenuOnScreen = false;
			ThisScreen.SetActive(false);
		}

		if(TitleQuit)
		{
			Application.Quit();

		}

		if(TitleCreds)
		{
			ThisScreen.transform.position = new Vector3(0,16,-8);

		}
		if(TitleCredBack)
		{
			ThisScreen.transform.position = new Vector3(0,0,-8);

		}

		if(TitlePlay)
		{

			RestartEverything.SetActive(true);
			Main.InGame = true;
			Destroy(ThisScreen);

		}


	}


}
