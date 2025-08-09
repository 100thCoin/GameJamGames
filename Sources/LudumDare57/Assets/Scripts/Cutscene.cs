using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour {

	public SpriteRenderer RollingTransition;
	public float Timer;
	public float Duration;
	public GameObject EnableThisWhenDone;
	public bool InitTutorialWhenComplete;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Timer += Time.deltaTime;

		if (Timer < 0.5f) {

			RollingTransition.color = new Vector4 (0, 0, 0, 0.5f + Timer);

		} else if (Timer < Duration - 0.5f) {
			RollingTransition.color = new Vector4(0,0,0,1);
		} else {
			RollingTransition.color = new Vector4 (0, 0, 0, 1-((Timer+0.5f)-Duration));

		}

		if (Timer > Duration) {
			//we're done here.
			EnableThisWhenDone.SetActive(true);
			if (InitTutorialWhenComplete) {
				Global.Dataholder.InitiateTutorial = true;
			}
			Destroy (gameObject);
		}

	}
}
