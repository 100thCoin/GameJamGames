using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlaceItems : MonoBehaviour {

	public DataHolder Main;
	public bool Test;
	public GameObject NewItem;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Test)
		{
			Test = false;

		}

	}

	public void AddItemToScreen(ItemStat Stat)
	{
		
		GameObject Newlyadded = Instantiate(NewItem,new Vector3(gameObject.transform.position.x -11,gameObject.transform.position.y +4 - Main.Items.Length,gameObject.transform.position.z + 1),gameObject.transform.rotation,gameObject.transform) as GameObject;
		Newlyadded.GetComponent<MouseOverUI>().Stats = Stat;
		Main.Items[Main.HasItem(Stat)].DHMOText = Newlyadded.GetComponent<MouseOverUI>().Count;

		//woo hoo jam code. i cant believe this is the only fix i could find
		Main.enabled = false;
		Main.enabled = true;

	}

}
