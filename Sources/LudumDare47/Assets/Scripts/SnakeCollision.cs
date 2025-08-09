using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeCollision : MonoBehaviour {

	public LoopMain Loop;
	public Camera_Movement CamMov;
	public FadetoWhite FadeWhite;
	public DataHolder Main;

	public Char_Movement Char;

	public GameObject CCentral;
	public GameObject CLeft;
	public GameObject CDown;
	public GameObject CDownLeft;
	public GameObject CUp;
	public GameObject CUpLeft;
	public GameObject CUpRight;
	public GameObject Cright;
	public GameObject Cdownright;

	void FixedUpdate()
	{
		if(CamMov.SnakingFocus != null)
		{
			transform.position = CamMov.SnakingFocus.transform.position;
		}
		/*
		CCentral.transform.position = new Vector3(Mathf.Round((gameObject.transform.position.x-40)/320f)*320f,0,Mathf.Round((gameObject.transform.position.z-40)/320f)*320f);
		CLeft.transform.position = new Vector3(Mathf.Round((gameObject.transform.position.x-40-320)/320f)*320f,0,Mathf.Round((gameObject.transform.position.z-40)/320f)*320f);
		Cright.transform.position = new Vector3(Mathf.Round((gameObject.transform.position.x-40+320)/320f)*320f,0,Mathf.Round((gameObject.transform.position.z-40)/320f)*320f);
		Cdownright.transform.position = new Vector3(Mathf.Round((gameObject.transform.position.x-40+320)/320f)*320f,0,Mathf.Round((gameObject.transform.position.z-40-320)/320f)*320f);
		CUpRight.transform.position = new Vector3(Mathf.Round((gameObject.transform.position.x-40+320)/320f)*320f,0,Mathf.Round((gameObject.transform.position.z-40+320)/320f)*320f);
		CDownLeft.transform.position = new Vector3(Mathf.Round((gameObject.transform.position.x-40-320)/320f)*320f,0,Mathf.Round((gameObject.transform.position.z-40-320)/320f)*320f);
		CUpLeft.transform.position = new Vector3(Mathf.Round((gameObject.transform.position.x-40-320)/320f)*320f,0,Mathf.Round((gameObject.transform.position.z-40+320)/320f)*320f);
		CDown.transform.position = new Vector3(Mathf.Round((gameObject.transform.position.x-40)/320f)*320f,0,Mathf.Round((gameObject.transform.position.z-40-320)/320f)*320f);
		CUp.transform.position = new Vector3(Mathf.Round((gameObject.transform.position.x-40)/320f)*320f,0,Mathf.Round((gameObject.transform.position.z-40+320)/320f)*320f);
*/

	}

	void OnTriggerEnter(Collider other)
	{
		if(other.name == "WALL")
		{
			if(Char.SnakingLeft)
			{
				Char.JamUntilSnakeRight = true;
			}
			else
			{
				Char.JamUntilSnakeLeft = true;

			}



		}
		if(other.name == "StartLvl2")
		{
			FadeWhite.Active = true;
		}
		if(other.tag == "Scale")
		{
			Main.OnScale = true;
		}
		if(other.tag == "Win")
		{

			Char.JamUntilSnakeRight = true;
			Char.JamUntilSnakeLeft = true;

			Main.Move = false;
			Main.Win = true;

			Destroy(GameObject.Find("AudioHolder").gameObject);

		}
	}
	void OnTriggerExit(Collider other)
	{

		if(other.tag == "Scale")
		{
			Main.OnScale = false;
		}
	}

}
