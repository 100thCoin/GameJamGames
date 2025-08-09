using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButtons : MonoBehaviour {

	public bool Play;
	public bool Credits;
	public bool Back;
	public bool Quit;


	public GameObject FullTitle;
	public GameObject FullGame;
	public GameObject Camera;


	void OnMouseOver()
	{
		if(Input.GetKeyDown(KeyCode.Mouse0))
		{

			if(Play)
			{
				FullGame.SetActive(true);
				FullTitle.SetActive(false);
				Camera.transform.position = new Vector3(-16,300,-50);

			}

			if(Credits)
			{

				Camera.transform.position = new Vector3(0,525,-50);

			}

			if(Back)
			{

				Camera.transform.position = new Vector3(0,500,-50);

			}



			if(Quit)
			{

				Application.Quit();
			}

		}


	}



}
