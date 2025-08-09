using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCount : MonoBehaviour {

	public DataHolder Main;
	public TextMesh TM;
	public int index;
	public SpriteRenderer SR;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		TM.text = Main.Build.RemainingTiles[index] > 0 ? "" + Main.Build.RemainingTiles[index] : "";
		SR.enabled = Main.Build.RemainingTiles[index] > 0;

	}
}
