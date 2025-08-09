using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDisplay : MonoBehaviour {




	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
		if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Quest != "")
		{
		gameObject.GetComponent<TextMesh>().text = 	GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Quest;

		}
		else
		{
			gameObject.GetComponent<TextMesh>().text = "No active\nQuest";
		}


	}
}
