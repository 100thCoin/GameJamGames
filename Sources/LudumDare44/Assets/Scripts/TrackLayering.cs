using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackLayering : MonoBehaviour {

	public DataHolder Main;

	public float Vol;
	public AudioSource MainTrack;
	public AudioSource Layer;
	public float L;

	public GameObject P;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		L = P.transform.position.y;

		if(L < 0)
		{
			L = 0;
		}

		if( L > 100)
		{
			L = 100;
		}

		MainTrack.volume = (1 - L*0.01f) / Vol;
		Layer.volume = (L*0.01f) / Vol;



	}
}
