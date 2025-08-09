using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2AlwaysFacePlayer : MonoBehaviour {
	public GameObject PlayerCol;
	public bool NotParent;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!NotParent)
		{
		transform.parent.eulerAngles = new Vector3(0,-Mathf.Atan2(transform.parent.position.z-PlayerCol.transform.position.z,transform.parent.position.x-PlayerCol.transform.position.x)*Mathf.Rad2Deg,0);
		}
		else
		{
			transform.eulerAngles = new Vector3(0,-Mathf.Atan2(transform.position.z-PlayerCol.transform.position.z,transform.position.x-PlayerCol.transform.position.x)*Mathf.Rad2Deg + 90,0);

		}
	}
}
