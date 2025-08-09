using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickCoins : MonoBehaviour {

	public bool Brick2Coin;
	public DataHolder Main;
	public GameObject Coin;
	public GameObject Brick;

	// Use this for initialization
	void Start () {
		Main = GameObject.Find("Main").GetComponent<DataHolder>();

	}
	
	// Update is called once per frame
	void Update () {

		Coin.SetActive(Brick2Coin == (Main.ButtonPressed ==1));
		Brick.SetActive(Brick2Coin != (Main.ButtonPressed ==1));

	}
}
