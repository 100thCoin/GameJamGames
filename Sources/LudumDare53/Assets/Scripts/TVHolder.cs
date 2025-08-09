using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVHolder : MonoBehaviour {

	public int ID;
	public GameObject TV;

	public void ReplaceTV(int NewID, GameObject NewTV)
	{
		if (NewID == ID) {
			Destroy (NewTV);
		} else {
			if (TV != null) {
				Destroy (TV);
			}
			ID = NewID;
			TV = NewTV;
			TV.transform.parent = transform;
		}

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
