using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningCinematic : MonoBehaviour {

	public bool oneFrameDelay;
	public float MessageTimer;
	public float SlerpTimer;
	public GameObject FunnyMusic;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (oneFrameDelay) {
			MessageTimer += Time.deltaTime;
			if (MessageTimer > 8) { 

				SlerpTimer += Time.deltaTime * 0.25f;
				SlerpTimer = Mathf.Clamp01 (SlerpTimer);
				Global.DataHolder.CamMov.transform.position = new Vector3(0, DataHolder.TwoCurveLerp (46, 0, SlerpTimer, 1),-10);
				if (SlerpTimer >= 1) {
					Global.DataHolder.InGame = true;
					Global.DataHolder.CamMov.InCinematic = false;
					FunnyMusic.SetActive (true);
					Destroy (this);
				}
			}

		}
		oneFrameDelay = true;
	}
}
