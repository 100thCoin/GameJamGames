using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedRibbonPiecce : MonoBehaviour {
	// nice typo


	public GameObject TVScreen;
	public GameObject Key;

	public Sprite[] Ribbons;
	public SpriteRenderer BG;
	public float[] Lengths;

	public bool left;
	public bool middle;
	public bool lone;

	public void Updoodle(int ID)
	{
		if (ID == 0) {
			TVScreen.SetActive (true);

		} else {
			TVScreen.SetActive (false);

		}
		if (ID == 2) {
			Key.SetActive (true);

		} else {
			Key.SetActive (false);

		}

		BG.sprite = Ribbons [ID];

		if (lone) {
			return;
		}

		if (middle) {

			// figure out where to place the middle piece
			transform.localPosition = new Vector3 (Global.Dataholder.CustomRibbonMan.TopLayer.transform.localPosition.x*4, 0, 0);



		} else {

			bool infront = false;
			if (left) {
				infront = Global.Dataholder.CustomRibbonMan.MainLayer.sortingOrder == 2;
				float pos = (Global.Dataholder.CustomRibbonMan.MainLayer.transform.localPosition.x * 4);
				transform.localPosition = new Vector3 (pos, 0, infront ? -5 : -10);

			} else {
				infront = Global.Dataholder.CustomRibbonMan.MainLayer2.sortingOrder == 2;
				float pos = (Global.Dataholder.CustomRibbonMan.MainLayer2.transform.localPosition.x * 4);

				transform.localPosition = new Vector3 (pos, 0, infront ? -5 : -10);

			}




		}




	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
