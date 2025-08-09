using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverOver : MonoBehaviour {

	public GameObject TextBox;
	public DataHolder Main;

	// Use this for initialization
	void OnMouseEnter()
	{
		TextBox.SetActive(true);
	}

	void OnMouseExit()
	{
		TextBox.SetActive(false);
	}
}
