using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameObject : MonoBehaviour {

	public SpriteRenderer  SR;
	public string[] Names;
	public string ChosenName;
	public GameObject SplatObj;
	public GameObject PlacedVer;
	public bool Cap;

	// Use this for initialization
	void Start () {
		int RNG = Random.Range(0,Names.Length);
		ChosenName = Names[RNG];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
