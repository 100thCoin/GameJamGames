using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleWobble : MonoBehaviour {

	public float Wobbletimer;
	public float amplitude;
	public Transform Vis;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
		Wobbletimer += Time.deltaTime;

		Vis.localPosition = new Vector3 (0, Mathf.Sin (Wobbletimer) * amplitude, 0);


	}
}
