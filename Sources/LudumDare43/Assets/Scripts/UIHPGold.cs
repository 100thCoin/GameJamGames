using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHPGold : MonoBehaviour {

	public bool HP;
	public bool Meat;


	public MaryController Mary;
	// Update is called once per frame
	void Update () {

		if(HP)
		{
			gameObject.GetComponent<TextMesh>().text = "" + Mary.HP;

		}
		else if(Meat)
		{
			gameObject.GetComponent<TextMesh>().text = "" + Mary.LambMeat;

		}
		else
		{
			gameObject.GetComponent<TextMesh>().text = "" + Mary.Gold;
		}


	}
}
