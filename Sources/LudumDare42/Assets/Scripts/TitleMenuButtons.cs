using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenuButtons : MonoBehaviour {


	public GameObject Cam;
	public bool ReturnToMain;
	public bool GoToCreds;
	public bool StartGame;
	public bool QuitGame;
	public GameObject InGame;

	void OnMouseOver () {
		if(Input.GetKeyDown(KeyCode.Mouse0))
		{

			if(ReturnToMain)
			{
				Cam.gameObject.transform.position = new Vector3(0,0,-10);

			}
			if(GoToCreds)
			{
				Cam.gameObject.transform.position = new Vector3(0,-40,-10);

			}
			if(QuitGame)
			{
				Application.Quit();

			}
			if(StartGame)
			{
				InGame.SetActive(true);
				Destroy(gameObject.transform.parent.gameObject);
			}
		}


	}
}
