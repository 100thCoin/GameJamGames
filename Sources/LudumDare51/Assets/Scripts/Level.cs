using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

	public bool Debug;

	public GameObject[] LevelParts;

	// Use this for initialization
	void Start () {
		if(Debug)
		{
			G.Main.StateBlocks = LevelParts;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
