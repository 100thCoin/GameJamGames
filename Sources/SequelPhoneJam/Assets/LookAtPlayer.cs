using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour {

	public Transform P;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.eulerAngles = new Vector3(0,0,Mathf.Atan2(transform.position.y - P.position.y,transform.position.x - P.position.x)*Mathf.Rad2Deg-90);
	}
}
