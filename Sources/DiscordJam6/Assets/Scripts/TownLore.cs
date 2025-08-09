using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownLore : MonoBehaviour {

	public List<string> Names;
	public List<string> CapNames;

	public string[] StarterFirsts;
	public string[] StarterSeconds;
	public string[] StarterExtras;

	public TownName Name;

	// Use this for initialization
	public void Start () {

		int RNG = Random.Range(0,StarterFirsts.Length);
		int RNG2 = Random.Range(0,StarterSeconds.Length);
		//int RNGE = Random.Range(0,StarterExtras.Length);

		string starter = StarterFirsts[RNG] + /*StarterExtras[RNGE] +*/ StarterSeconds[RNG2];
		Names.Add(starter);
		Name.UpdateText();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
