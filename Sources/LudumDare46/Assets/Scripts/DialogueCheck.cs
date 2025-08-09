using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCheck : MonoBehaviour {

	public float Timer;
	public float Speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Timer += Time.deltaTime;
		transform.localEulerAngles = new Vector3(0,0,Mathf.Sin(Timer*Speed)*27);


	}
}
