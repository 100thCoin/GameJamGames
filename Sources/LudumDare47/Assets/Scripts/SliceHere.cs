using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceHere : MonoBehaviour {

	public bool MouseOver;

	public Renderer R;

	public Collider C;

	DataHolder Main;

	public bool ScisCheck;

	public GameObject SliceEffect;



	// Use this for initialization
	void Start () {
		Main = 	GameObject.Find("Main").GetComponent<DataHolder>();

		name = transform.parent.name;
		transform.parent = Main.SliceHold.transform;

	}
	
	// Update is called once per frame
	void Update () {

		R.enabled = MouseOver;

		if(MouseOver)
		{

			if(!Main.HasScissors)
			{
				MouseOver = false;
				C.enabled = false;
				R.enabled = false;
			}
			else
			{
				if(Input.GetKeyDown(KeyCode.Mouse0))
				{
					Main.HasScissors = false;

					int P = int.Parse(name);

					Main.Loop.SliceAt = P;
					Main.Loop.SliceItLeft = true;
					Main.CantSliceSelf.SetActive(false);
					Main.Char.Sliced = true;
					Main.Char.SlcieDist = P;
					//Main.Loop.Level2_InsideSeam.transform.localPosition = new Vector3((-((P+0f)/Main.Loop.TheLoop.Length)*Main.Char.StripLength - Main.Char.StripLength*0.5f),0,0);
					Main.Lvl2Quad.Offset = (((P+0f)/Main.Loop.TheLoop.Length));
					Main.Lvl2OtherQuad.Offset = (((P+0f)/Main.Loop.TheLoop.Length)+0.5f);

				}

			}


		}

		if(ScisCheck)
		{
			return;
		}
		if(Main.HasScissors)
		{
			C.enabled = true;
			ScisCheck = true;
		}

	}

	void OnMouseEnter () {
		MouseOver = true;
	}

	// Update is called once per frame
	void OnMouseExit () {
		MouseOver = false;
	}
}
