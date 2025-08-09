using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpButton : MonoBehaviour {


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

		if(!Mercy)
		{
			SR.sprite = NoGlow;
		}
		Mercy = false;

	}


	void OnMouseOver()
	{
		Mercy = true;
		SR.sprite = Glow;

		if(Input.GetKeyDown(KeyCode.Mouse0) && F.InFarm)
		{


			Main.ExitFarm = true;
			F.InFarm = false;
		}
	}
}
