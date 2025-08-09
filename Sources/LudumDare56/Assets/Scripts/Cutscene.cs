using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour {

	public GameObject NextScreen;
	// else, go to victory screen.
	public GameObject CutsceneMan;
	public float Duration;
	public float RollingTransitionTimer;
	public SpriteRenderer RollingTransition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Duration -= Time.deltaTime;

		if (Duration < 0) {

			RollingTransitionTimer += Time.deltaTime;
			RollingTransition.color = new Vector4(0,0,0,DataHolder.SinLerp(0f,0.5f,RollingTransitionTimer,1));
			if (RollingTransitionTimer > 1f) {
				Global.Dataholder.rollingtime2 = true;
				Super.Dataholder.MusicMultiplier = 0;
				NextScreen.SetActive (true);
				gameObject.SetActive (false);
				CutsceneMan.SetActive (false);
			}

		}

	}
}
