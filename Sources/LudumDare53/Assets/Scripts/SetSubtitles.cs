using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSubtitles : MonoBehaviour {


	public SubtitleSet Subtitleset;


	// Use this for initialization
	void Start () {

		Global.DataHolder.SubtitleManager.Initialize(Subtitleset);
		transform.parent = Global.DataHolder.VAHolder;
		Global.DataHolder.CurrentVoiceLine = gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
