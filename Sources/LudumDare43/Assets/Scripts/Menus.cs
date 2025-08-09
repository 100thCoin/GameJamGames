using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menus : MonoBehaviour {

	public bool StartGame;
	public bool TryAgain;

	public GameObject ThingToDestroy;
	public GameObject TheHolyWhoosh;

	public bool MainCreds;
	public bool MainQuit;
	public bool MainPlayer;
	public bool PlayBegin;
	public bool PlayTut;

	public bool BackToMain;

	public bool LoseToTitle;

	public bool WinToTitle;

	public bool DoOnce;
	// Use this for initialization
	void Start () {
		if(TryAgain)
		{
			ThingToDestroy = GameObject.Find("Main Camera").gameObject;

		}

		if(LoseToTitle)
		{
			ThingToDestroy = GameObject.Find("Main Camera").gameObject;

		}


	}
	
	// Update is called once per frame
	void OnMouseOver () {


		if(Input.GetKeyDown(KeyCode.Mouse0))
		{

			if(TryAgain && !DoOnce)
			{
				DoOnce = true;
				GameObject Holyness = Instantiate(TheHolyWhoosh,new Vector3(GameObject.Find("Main Camera").gameObject.transform.position.x,GameObject.Find("Main Camera").gameObject.transform.position.y,-99 ),gameObject.transform.rotation) as GameObject;
				Holyness.transform.Find("Whoop").GetComponent<Whoop>().Destroyyy = ThingToDestroy;



			}

			if(PlayBegin && !DoOnce)
			{
				DoOnce = true;
				GameObject Holyness = Instantiate(TheHolyWhoosh,new Vector3(GameObject.Find("Main Camera").gameObject.transform.position.x,GameObject.Find("Main Camera").gameObject.transform.position.y,-99 ),gameObject.transform.rotation) as GameObject;
				Holyness.transform.Find("Whoop").GetComponent<Whoop>().Destroyyy = ThingToDestroy;



			}


			if(MainCreds)
			{

				GameObject.Find("Main Camera").gameObject.transform.position = new Vector3(0,-22,-100);


			}



			if(BackToMain)
			{

				GameObject.Find("Main Camera").gameObject.transform.position = new Vector3(0,0,-100);


			}


			if(MainPlayer)
			{

				GameObject.Find("Main Camera").gameObject.transform.position = new Vector3(0,-44,-100);


			}

			if(PlayTut)
			{

				GameObject.Find("Main Camera").gameObject.transform.position = new Vector3(0,-66,-100);


			}
			if(MainQuit)
			{

				Application.Quit();


			}

			if(WinToTitle || LoseToTitle && !DoOnce)
			{
				DoOnce = true;
				print("hello");
				GameObject Holy = Instantiate(TheHolyWhoosh,new Vector3(0,0,0),gameObject.transform.rotation)as GameObject;
				Holy.name = "Menu";

				Destroy(ThingToDestroy);


			}




		}





	}
}
