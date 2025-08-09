using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeCollapseSequence : MonoBehaviour {

	public bool Collapse;
	public int collapseCounter;
	public int Delay;
	public int Counter;

	public SpriteRenderer SR;

	public GameObject[] BridgeSegments;

	public SpriteRenderer Dreadd;
	public Animator DreaddAnim;
	public Sprite DreaddGasp;

	public float DreaddAccel;

	public GameObject BridgeSFX;
	public GameObject FallSFX;

	public bool Finale;

	public GameObject MusicHolder;

	// Use this for initialization
	void Start () {
		Global.DataHolder.AxeObject = gameObject;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (Collapse) {
			DreaddAnim.runtimeAnimatorController = null;
			Dreadd.sprite = DreaddGasp;

			if (collapseCounter < BridgeSegments.Length) {

				BridgeSegments [collapseCounter].SetActive (false);

				if (Counter == 0) {

					collapseCounter++;
					Counter = Delay;
					Instantiate (BridgeSFX);

				} else {
					Counter--;
				}
				if (collapseCounter >= BridgeSegments.Length) {
					Instantiate (FallSFX);
				}

			} else {
				DreaddAccel += Time.fixedDeltaTime*0.33f;
				Dreadd.transform.position -= new Vector3 (0, DreaddAccel, 0);

				if (DreaddAccel >= 0.55f) {
					if (!Finale) {
						Global.DataHolder.NextCinematic ();
						Global.DataHolder.UnloadLevel ();
						enabled = false;
						Destroy (gameObject);
						Global.DataHolder.Game.SetActive (false);
					}
					else {
						Global.DataHolder.WinGame ();
						enabled = false;

					}
				} 

			}
		}
	}





}
