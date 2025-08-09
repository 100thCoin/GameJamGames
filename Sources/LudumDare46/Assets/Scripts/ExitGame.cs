using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour {

	public bool Mercy;
	public SpriteRenderer SR;
	public Sprite Glow;
	public Sprite NoGlow;

	public Farm F;

	public bool Exit;
	public bool StartGame;

	public bool Mute;

	public TextMesh TM;

	public WriteLine WL;

	public GameObject Sanctext;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {


		if(!Mercy)
		{
			TM.color = Color.white;
		}
		Mercy = false;
	}


	void OnMouseOver()
	{
		Mercy = true;
		TM.color = Color.yellow;

		if(Input.GetKeyDown(KeyCode.Mouse0))
		{
			if(Exit)
			{
				Application.Quit();
			}
			else
				if(StartGame)
				{
					WL.POSTPONE = false;
					Sanctext.SetActive(true);
					Destroy(transform.parent.gameObject);
				}

		}
	}
}