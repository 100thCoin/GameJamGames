using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleAllItemsDroppermajiggy : MonoBehaviour {

	public DataHolder Main;

	public int DropThisOne;

	void Start () {
		Main = GameObject.Find("Main").GetComponent<DataHolder>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Main.Items.Length >= 1)
		{
			if(Main.Items[DropThisOne].Count > 0)
			{
				Main.Items[DropThisOne].Count--;
				Instantiate(Main.Items[DropThisOne].AssociatedGO,transform.position,transform.rotation,transform.parent);
			}
			else
			{
				Destroy(Main.Items[DropThisOne].DHMOText.transform.parent.gameObject);

				Main.RemoveItem(Main.Items[DropThisOne]);
			}
		}
		else
		{
			Destroy(gameObject);

		}




	}
}
