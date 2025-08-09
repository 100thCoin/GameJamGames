using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwistEndingController : MonoBehaviour {

	public TextMesh TM;
	public float GrandTimer;
	public bool GO;
	public GameObject AndNow;
	public GameObject TheTwistEnding;
	public GameObject GotEmLol;
	public GameObject TheEnd;
	public GameObject VictoryScreen;
	public GameObject PreVictory;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (GO) {

			GrandTimer += Time.deltaTime;

			if (GrandTimer > 0.5f && GrandTimer < 1.6f) {

				AndNow.SetActive (true);

			}
			if (GrandTimer > 1.6f && GrandTimer < 3.5f) {

				TheTwistEnding.SetActive (true);
				AndNow.SetActive (false);

			}

			if (GrandTimer > 3.5f && GrandTimer < 7) {

				GotEmLol.SetActive (true);
				TheTwistEnding.SetActive (false);

			}

			if (GrandTimer > 7 && GrandTimer < 8.5f) {

				TheEnd.SetActive (true);
				GotEmLol.SetActive (false);

			}

			if (GrandTimer > 8.5f) {
				TheEnd.SetActive (false);
			}

			if (GrandTimer > 9f) {
				VictoryScreen.SetActive (true);
				PreVictory.SetActive (false);

			}

		}


	}
}
