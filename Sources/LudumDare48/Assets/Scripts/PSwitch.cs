using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSwitch : MonoBehaviour {

	public DataHolder Main;

	public GameObject Col;

	public SpriteRenderer SR;
	public Sprite Push;
	public Sprite NotPush;

	public bool KeySwitch;

	// Use this for initialization
	void Start () {
		Main = GameObject.Find("Main").GetComponent<DataHolder>();
	}
	
	// Update is called once per frame
	void Update () {
		Col.SetActive(Main.ButtonPressed == 0);
		SR.sprite = Main.ButtonPressed == 1 ? Push : NotPush;


	}


	void OnTriggerStay(Collider other)
	{
		if(!KeySwitch)
		{
			return;
		}


		if(other.name == "Key")
		{
			
			Main.ButtonPressed = 1;
			Main.ButtonTimer = 0;
			Destroy(other.gameObject);
		}

	}

}
