using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBar : MonoBehaviour {

	public float Timer;
	public TextMesh TM;
	public int Index = -1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		transform.localPosition = new Vector3(-3 + Timer,0,0);
		transform.localScale = new Vector3(Timer*2,2,1);
	}
}
