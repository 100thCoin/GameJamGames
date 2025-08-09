using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderiteUI : MonoBehaviour {


	public GameObject LadderiteUIObject;
	public TextMesh T;
	public GameObject LanternUIO;
	public TextMesh TL2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(GetComponent<DataHolder>().RefinedLadderite >0)
		{

			LadderiteUIObject.SetActive(true);

			T.text = "Refined\nLadderite: " + GetComponent<DataHolder>().RefinedLadderite;

		}
		else
		{
			LadderiteUIObject.SetActive(false);

		}

		if(GetComponent<DataHolder>().Lanterns > 0)
		{

			LanternUIO.SetActive(true);

			TL2.text = "Lanterns: " + GetComponent<DataHolder>().Lanterns;

		}
		else
		{
			LanternUIO.SetActive(false);

		}

	}
}
