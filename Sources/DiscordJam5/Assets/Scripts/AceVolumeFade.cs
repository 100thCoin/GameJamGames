using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AceVolumeFade : MonoBehaviour {

	public AudioSource AS;
	public float Vol;
	public bool Done;
	public DataHolder Main;

	// Update is called once per frame
	void Update () {
		AS.volume = Vol*Main.Volume;

		if(Done)
		{
			Vol -= Time.deltaTime*2;
			if(Vol <=0)
			{
				Destroy(gameObject);
			}
		}

	}
}
