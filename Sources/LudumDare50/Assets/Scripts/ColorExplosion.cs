using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorExplosion : MonoBehaviour {

	public Color Colorr;
	public ParticleSystem[] PSs;

	// Use this for initialization
	void Start () {

		int i = 0;
		while(i < PSs.Length)
		{
			PSs[i].startColor = Colorr;
			i++;
		}
			


	}
	

}
