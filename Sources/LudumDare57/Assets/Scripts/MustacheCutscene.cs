using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MustacheCutscene : MonoBehaviour {

	public SpriteRenderer SR;
	public Sprite M;

	// Use this for initialization
	void Start () {
		if (Global.Dataholder.Upgrade_Mustache) {
			SR.sprite = M;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
