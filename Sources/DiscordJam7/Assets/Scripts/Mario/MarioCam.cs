using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioCam : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Main.DataHolder.MarioOnMap)
		{
			transform.position = new Vector3(0,0,-50);
		}
		else
		{
			if(Main.DataHolder.Mario.transform.position.x > transform.position.x + 2)
			{
				transform.position = new Vector3(Main.DataHolder.Mario.transform.position.x-2,transform.position.y,-50);

			}
			else if(Main.DataHolder.Mario.transform.position.x < transform.position.x - 4)
			{
				transform.position = new Vector3(Main.DataHolder.Mario.transform.position.x+4,transform.position.y,-50);

			}

			if(transform.position.x < 0)
			{
				transform.position = new Vector3(0,transform.position.y,-50);
			}
			if(transform.position.x >275)
			{
				transform.position = new Vector3(275,transform.position.y,-50);
			}
			if(transform.position.x >216 && Main.DataHolder.MarioLevelID == 2)
			{
				transform.position = new Vector3(216,transform.position.y,-50);
			}
		}
	}
}
