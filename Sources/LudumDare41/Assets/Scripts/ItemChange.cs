using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChange : MonoBehaviour {

	public Sprite Sword;
	public Sprite Pick;
	public Sprite Axe;
	public Sprite Shovel;

	public int Weapon;



	// Use this for initialization
	void Start () {




	}
	
	// Update is called once per frame
	void Update () {


		if(Input.GetKeyDown(KeyCode.Alpha1) && GameObject.Find("Main").gameObject.GetComponent<DataHolder>().OtherInventory[15] != -1){Weapon = 0;}
		if(Input.GetKeyDown(KeyCode.Alpha2)&& GameObject.Find("Main").gameObject.GetComponent<DataHolder>().OtherInventory[13] != -1){Weapon = 1;}
		if(Input.GetKeyDown(KeyCode.Alpha3) && GameObject.Find("Main").gameObject.GetComponent<DataHolder>().OtherInventory[12] != -1){Weapon = 2;}
		if(Input.GetKeyDown(KeyCode.Alpha4) && GameObject.Find("Main").gameObject.GetComponent<DataHolder>().OtherInventory[14] != -1){Weapon = 3;}

		if(Weapon == 0){gameObject.GetComponent<SpriteRenderer>().sprite = Sword;gameObject.tag = "Sword";}
		if(Weapon == 1){gameObject.GetComponent<SpriteRenderer>().sprite = Pick;gameObject.tag = "Pick";}
		if(Weapon == 2){gameObject.GetComponent<SpriteRenderer>().sprite = Axe; gameObject.tag = "Axe";}
		if(Weapon == 3){gameObject.GetComponent<SpriteRenderer>().sprite = Shovel;gameObject.tag = "Shovel";}




	}
}
