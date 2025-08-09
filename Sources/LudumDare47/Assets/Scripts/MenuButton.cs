using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour {

	public bool MuteButton;
	public bool Play;
	public bool Quit;
	public bool Credits;
	public bool Back;
	public bool Hover;

	public GameObject Game;
	public GameObject Title;

	public GameObject Cam;

	public DataHolder Muted;

	public SpriteRenderer SR;
	public Sprite Muteeeee;
	public Sprite notMuteeeee;

	void OnMouseEnter () {
		Hover = true;
	}

	// Update is called once per frame
	void OnMouseExit () {
		Hover = false;
	}



	void Update()
	{
		if(Hover && Input.GetKeyDown(KeyCode.Mouse0))
		{
			if(Quit)
			{
				Application.Quit();

			}

			if(Play)
			{
				Game.SetActive(true);

				Destroy(Title.gameObject);
			}


			if(Credits)
			{
				Cam.transform.localPosition = new Vector3(-30,0,-50);

			}

			if(Back)
			{
				Cam.transform.localPosition = new Vector3(0,0,-50);

			}

			if(MuteButton)
			{
				Muted.Mute = !Muted.Mute;
				SR.sprite = Muted.Mute ? Muteeeee : notMuteeeee;
			
			}

		}


	}
}
