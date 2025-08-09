using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubCutsceneManager : MonoBehaviour {

	public int ID;
	public CubicBezierCurve[] Curves;
	public GameObject[] Actors;
	public float[] ActorDelays;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {

		if (ID == 0) {

			int i = 0;
			while (i < Actors.Length) {
				ActorDelays [i] += Time.deltaTime;
				float Atimer = Mathf.Clamp01 (ActorDelays [i]);
				Actors [i].transform.position = Curves [i].GetPoint (Atimer);


				i++;
			}



		} else if (ID == 1) {

			Global.Dataholder.WellWall.SetActive (true);

		}
		else if (ID == 2) {
			if (Global.Dataholder.LoadedLevel.GetComponent<HideForCutscene> () == null) {
				gameObject.SetActive (false);

				return;
			}
			Global.Dataholder.LoadedLevel.GetComponent<HideForCutscene> ().ForceUnhide();
			gameObject.SetActive (false);
		}
		else if (ID == 3) {

			int i = 0;
			while (i < Actors.Length) {
				ActorDelays [i] += Time.deltaTime * 0.5f;
				float Atimer = Mathf.Clamp01 (ActorDelays [i]);
				Actors [i].transform.position = Curves [i].GetPoint (Atimer);


				i++;
			}



		}
		else if (ID == 5) {

			int i = 0;
			while (i < Actors.Length) {
				ActorDelays [i] += Time.deltaTime;
				float Atimer = Mathf.Clamp01 (ActorDelays [i]);
				Actors [i].transform.position = Curves [i].GetPoint (Atimer);


				i++;
			}



		}
		else if (ID == 7) {

			Global.Dataholder.ChangeToBossMusic();
			Destroy (this);


		}

	}
}
