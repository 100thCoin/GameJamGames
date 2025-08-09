using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudPlacementNoNos : MonoBehaviour {

	public int Nono;
	public int AnimalGroup;
	public bool Harmony;
	public bool Rabbit;
	public bool RabbitHarmony;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

			if (AnimalGroup == Global.Dataholder.CurrentHudSelectionAnimalType && Harmony == Global.Dataholder.CurrentHudSelectionHarmony) {
				Nono--;
			} else {
				Nono= -1;
			}


	}

	void OnTriggerStay(Collider other)
	{
		if (!Rabbit) {
			if (other.CompareTag ("NoNoZone")) {

				if (AnimalGroup == Global.Dataholder.CurrentHudSelectionAnimalType && Harmony == Global.Dataholder.CurrentHudSelectionHarmony) {
					Nono = 3;

				} else {
					Nono = -1;
				}

			}
		} else {

				if (other.CompareTag ("RabbitNono")) {
					if (AnimalGroup == Global.Dataholder.CurrentHudSelectionAnimalType && Harmony == Global.Dataholder.CurrentHudSelectionHarmony) {
						Nono = 3;

					} else {
						Nono = -1;
					}
				}
			
		}
	}

}
