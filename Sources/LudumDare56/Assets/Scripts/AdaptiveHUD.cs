using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptiveHUD : MonoBehaviour {

	public int Birds;
	public int Frogs;
	public int Rabbits;

	public GameObject[] BirdButtons;
	public GameObject[] FrogButtons;
	public GameObject[] RabbitButtons;

	public int prevtotal;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		int TotalActive = Birds + Frogs + Rabbits;
		if (prevtotal != TotalActive) {
			prevtotal = TotalActive;
			int j = 0;
			while (j < 3) {
				BirdButtons [j].SetActive (false);
				FrogButtons [j].SetActive (false);
				RabbitButtons [j].SetActive (false);
				j++;
			}

			int i = 0;
			float XPos = 0;
			while (i < TotalActive) {
				// start pos = -2 * total
				XPos = -2 * TotalActive + i*4 + 2; 

				if (i < Birds) {
					BirdButtons [i].SetActive (true);
					BirdButtons [i].transform.localPosition = new Vector3 (XPos, BirdButtons [i].transform.localPosition.y, 0);
				}
				else if (i >= Birds && i < Frogs + Birds) {
					FrogButtons [i-Birds].SetActive (true);
					FrogButtons [i-Birds].transform.localPosition = new Vector3 (XPos, FrogButtons [i-Birds].transform.localPosition.y, 0);

				}
				else{
					RabbitButtons [i-Birds-Frogs].SetActive (true);
					RabbitButtons [i-Birds-Frogs].transform.localPosition = new Vector3 (XPos, RabbitButtons [i-Birds-Frogs].transform.localPosition.y, 0);

				}

				i++;
			}
		}



	}
}
