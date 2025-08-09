using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTileTex : MonoBehaviour {

	public Sprite[] Rand;
	public SpriteRenderer ThisRederer;
	// Use this for initialization
	void OnEnable () {

		int rand = Random.Range(0,Rand.Length);
		ThisRederer.sprite = Rand[rand];


	}

}
