using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour {


	public bool Open;
	public bool CanDo;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown(KeyCode.Escape) && !GameObject.Find("Main").gameObject.GetComponent<DataHolder>().CannotOpenInventory)
		{

			if(Open & CanDo)
			{
				CanDo = false;
				Open = false;
				gameObject.transform.localPosition = new Vector3(0,0,-500);
			}

			if(!Open & CanDo)
			{
				CanDo = false;
				Open = true;
				gameObject.transform.localPosition = new Vector3(0,0,0.4f);

			}
			CanDo = true;





		}


		if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().CannotOpenInventory)
		{
			gameObject.transform.localPosition = new Vector3(0,0,-500);
		}

	}
}
