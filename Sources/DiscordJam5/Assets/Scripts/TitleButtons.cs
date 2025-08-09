using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButtons : MonoBehaviour {

	public bool Quit;
	public bool Start;
	public bool Creds;
	public bool Back;
	public bool HowPlay;

	public DataHolder Main;

	public GameObject TitleScreen;
	public AudioSource LevelMusic;
	public SpriteRenderer HowButton;
	public GameObject HowGO;
	void OnMouseOver()
	{
		if(Input.GetKeyDown(KeyCode.Mouse0))
		{
			if(Quit)
			{
				Application.Quit();
			}
			else if(Start)
			{
				Main.InGame = true;
				Main.LevelNum = -1;
				Main.NextLevel = true;
				LevelMusic.pitch = 1;
				Destroy(TitleScreen);
			}
			else if(Creds)
			{
				Main.MainCamera.transform.position = new Vector3(-32,49.82f,-10);
			}
			else if(Back)
			{
				Main.MainCamera.transform.position = new Vector3(-32,32,-10);

			}
			else if(HowPlay)
			{
				HowButton.enabled = false;
				HowGO.SetActive(true);
				Destroy(this);
			}
		}

	}
}
