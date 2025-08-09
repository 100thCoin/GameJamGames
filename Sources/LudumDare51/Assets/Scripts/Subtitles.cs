using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subtitles : MonoBehaviour {

	public TextMesh[] OL;
	public string Text;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		int i = 0;
		while( i < OL.Length)
		{
			OL[i].text = Text;
			i++;
		}


	}
}
