using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryText : MonoBehaviour {

	public bool Gold;
	public bool Keys;
	public bool Acorns;
	public bool Sticks;
	public bool Logs;
	public bool Berries;
	public bool Onions;
	public bool Pumpkins;
	public bool Rocks;
	public bool Iron;
	public bool Gems;
	public bool Pelt;
	public bool Bones;
	public bool Souls;




	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Gold){gameObject.GetComponent<TextMesh>().text = "" + GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Gold;}
		else if(Keys){gameObject.GetComponent<TextMesh>().text = "" + GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Keys;}
		else if(Acorns){gameObject.GetComponent<TextMesh>().text = "" + GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Acorns;}
		else if(Sticks){gameObject.GetComponent<TextMesh>().text = "" + GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Sticks;}
		else if(Logs){gameObject.GetComponent<TextMesh>().text = "" + GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Logs;}
		else if(Berries){gameObject.GetComponent<TextMesh>().text = "" + GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Berries;}
		else if(Onions){gameObject.GetComponent<TextMesh>().text = "" + GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Onions;}
		else if(Pumpkins){gameObject.GetComponent<TextMesh>().text = "" + GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Pumpkins;}
		else if(Rocks){gameObject.GetComponent<TextMesh>().text = "" + GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Rocks;}
		else if(Iron){gameObject.GetComponent<TextMesh>().text = "" + GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Iron;}
		else if(Gems){gameObject.GetComponent<TextMesh>().text = "" + GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Gems;}
		else if(Pelt){gameObject.GetComponent<TextMesh>().text = "" + GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Pelt;}
		else if(Bones){gameObject.GetComponent<TextMesh>().text = "" + GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Bones;}
		else if(Souls){gameObject.GetComponent<TextMesh>().text = "" + GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Souls;}








	}
}
