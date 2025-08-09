using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour {

	public Transform Target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 diff = (Global.Dataholder.Pmov.transform.position - transform.position).normalized;
		Target.transform.eulerAngles = new Vector3 (0, -Mathf.Rad2Deg*Mathf.Atan2 (diff.z, diff.x) +90, 0);
	}
}
