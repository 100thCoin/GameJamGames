using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookButton : MonoBehaviour {


	public bool Mercy;
	public SpriteRenderer SR;
	public Sprite Glow;
	public Sprite NoGlow;

	public GameObject Notes;

	public DataHolder Main;

	public bool Active;
	public Farm F;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		SR.enabled = Main.Progress >= 3;

		if(!Mercy)
		{
			SR.sprite = NoGlow;
		}
		Mercy = false;


		if(F.XDist > 13 || Main.NoMoving)
		{
			Notes.SetActive(false);

		}
	}


	void OnMouseOver()
	{
		Mercy = true;
		SR.sprite = Glow;

		if(Input.GetKeyDown(KeyCode.Mouse0) && Main.Progress >= 3)
		{


			Notes.SetActive(!Notes.activeSelf);

		}
	}
}
