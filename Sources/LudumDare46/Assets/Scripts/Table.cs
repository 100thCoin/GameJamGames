using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour {

	public DataHolder Main;

	public SpriteRenderer SR;
	public GameObject children;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Main.Progress >= 3.8f)
		{
			children.SetActive(true);
			SR.enabled = true;

		}
	}
}
