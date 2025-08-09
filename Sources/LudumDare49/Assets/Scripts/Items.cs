using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour {

	public Sprite[] Options;
	public SpriteRenderer SR;



	// Use this for initialization
	void Start () {
		SR.sprite = Options[Mathf.RoundToInt(Random.Range(0,Options.Length))];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
