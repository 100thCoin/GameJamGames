using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFlash : MonoBehaviour {

	public SpriteRenderer SR;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		SR.color = new Vector4 (1, 1, 1, Global.Dataholder.ScreenFlashTimer);
	}
}
