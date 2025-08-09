using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetObjectPlayerWants : MonoBehaviour {

	// the player is walking towards something, let's figure out what.

	public GameObject[] PotentialObjects;
	public HeroSmackObject[] PotentialObjectSmacks;

	public bool TEST;
	public GameObject TheCurrentTarget;
	public GameObject PreviousObject;

	public GameObject HideObjectsForCutscene;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (TEST) {
			//TEST = false;
			TheCurrentTarget = GetTheTargetObject ();
		}


	}


	public GameObject GetTheTargetObject()
	{
		int highestHP = 0;
		if (Global.Dataholder.Level == 2) {
			if (PotentialObjectSmacks.Length == 0) {

				PotentialObjectSmacks = new HeroSmackObject[PotentialObjects.Length];
				int k = 0;
				while (k < PotentialObjectSmacks.Length) {

					PotentialObjectSmacks [k] = PotentialObjects [k].transform.parent.Find("PlayerCol").GetComponent<HeroSmackObject> ();
					k++;

				}

			}

			int i = 0;
			while (i < PotentialObjectSmacks.Length) {

				if (PotentialObjectSmacks [i].Health > highestHP) {
					highestHP = PotentialObjectSmacks [i].Health;
				}
				i++;
			}

		}

		// the player isn't currently walking directly towards any targets?
		//are they moving at all?
		if (new Vector2 (Global.Dataholder.Pmov.CurrentVector.x, Global.Dataholder.Pmov.CurrentVector.z).magnitude > 0.1f) {
			// we are moving

			RaycastHit[] Hit = Physics.RaycastAll (Global.Dataholder.Pmov.transform.position + new Vector3(0,1,0), Global.Dataholder.Pmov.CurrentVector, 100000, 1<<10);

			if (Hit.Length > 0 && (Global.Dataholder.Level != 2 || (PreviousObject != Hit[0].collider.gameObject && Global.Dataholder.Level == 2))) {
				PreviousObject = Hit [0].collider.gameObject;
				return Hit[0].collider.gameObject;


			} else {


				//pick closest target.

				float best = 10000;
				GameObject B = null;
				int j = 0;
				while (j < PotentialObjects.Length) {
					if (PotentialObjects [j] != null) {
						float d = (PotentialObjects [j].transform.position - Global.Dataholder.Pmov.transform.position).magnitude;
						if (Global.Dataholder.Level == 2) {
							if (d < best && PotentialObjectSmacks[j].Health >= highestHP && PotentialObjects[j] != PreviousObject) {
								best = d;
								B = PotentialObjects [j];
							}
						} else {
							if (d < best) {
								best = d;
								B = PotentialObjects [j];
							}
						}
					}
					j++;
				}
				PreviousObject = B;
				return B;

			} 


		}
		else {
			//stall?
		}
		PreviousObject = null;
		return null;

	}



}
