using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButtons : MonoBehaviour {

	public bool PlayButton;
	public bool QuitButton;
	public bool Credits;
	public bool ReturnToMain;

	public GameObject TitleCam;

	public GameObject EntireGame;
	public GameObject Title;

	void OnMouseOver()
	{
		if(Input.GetKeyDown(KeyCode.Mouse0))
		{
			if(Credits)
			{
				TitleCam.transform.localPosition = new Vector3(-32,0,-40);
			}
			if(ReturnToMain)
			{
				TitleCam.transform.localPosition = new Vector3(0,0,-40);
			}
			if(PlayButton)
			{
				//play game
				GameObject Game= Instantiate(EntireGame,new Vector3(0,0,0),transform.rotation);
				Game.name = "Main";
				Destroy(Title.gameObject);
			}
			if(QuitButton)
			{
				Application.Quit();
			}

		}


	}
}
