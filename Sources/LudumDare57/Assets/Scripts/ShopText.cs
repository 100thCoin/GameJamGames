using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopText : MonoBehaviour {

	[TextArea(5,5)]
	public string DefaultMSG;
	public TextMesh TM;
	public bool ItsThePercentageTracker;
	public GameObject StartdustTile;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
		if (ItsThePercentageTracker) {
			TM.text = Mathf.Floor (Global.Dataholder.ReconstructedPercent * 100) + "%";
			return;
		}

		if (Global.Dataholder.SelectedShopButton != null) {
			if (Global.Dataholder.SelectedShopButton.Grace >= 0) {
				TM.text = Global.Dataholder.SelectedShopButton.Message;
				if (Global.Dataholder.SelectedShopButton.SelectionIndex == 2) {
					StartdustTile.SetActive (true);
				} else {
					StartdustTile.SetActive (false);
				}
			} else {
				TM.text = DefaultMSG;
				StartdustTile.SetActive (false);
			}
		} else
		{
			TM.text = DefaultMSG;
			StartdustTile.SetActive (false);

		}

	}
}
