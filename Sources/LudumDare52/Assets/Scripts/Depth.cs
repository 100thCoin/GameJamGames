using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Depth : MonoBehaviour {

	public bool UpdateEveryFrame;
	public float Height;
	public Transform Vis;
	// Use this for initialization
	public int updatethismanytimesforsomereason;
	void Start () {

		Vis.transform.position = new Vector3 (transform.position.x, transform.position.y, (transform.position.y - Height) * 0.001f);

	}
	
	// Update is called once per frame
	void Update () {

		if (UpdateEveryFrame || updatethismanytimesforsomereason > 0) {
			Vis.transform.position = new Vector3 (transform.position.x, transform.position.y, (transform.position.y - Height) * 0.001f);
			updatethismanytimesforsomereason--;
		}

	}
		
	public void Manual () {

		Vis.transform.position = new Vector3 (transform.position.x, transform.position.y, (transform.position.y - Height) * 0.001f);

	}

}
