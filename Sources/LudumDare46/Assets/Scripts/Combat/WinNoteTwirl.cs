using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinNoteTwirl : MonoBehaviour {

	public float Timer;




	// Use this for initialization
	void OnEnable () {

		Timer = 0;

	}
	
	// Update is called once per frame
	void Update () {
		float YDuration = 0.5f;
		float LastYpos = 0;
		float TargetYpos = 1;

		float LastR = 360;
		float TargetR = 0;


		if(Timer < YDuration)
		{
			Timer+= Time.deltaTime;

			if(Timer > YDuration)
			{
				Timer = YDuration;
			}

			float LocalYPos = (((LastYpos - TargetYpos) * Mathf.Pow(Timer,2))/Mathf.Pow(YDuration,2) - ((2*LastYpos - 2*TargetYpos) * Timer)/YDuration + LastYpos);
			float LocalR = (((LastR - TargetR) * Mathf.Pow(Timer,2))/Mathf.Pow(YDuration,2) - ((2*LastR - 2*TargetR) * Timer)/YDuration + TargetR);

			transform.localScale = new Vector3(1,1,1)*LocalYPos;
			transform.eulerAngles = new Vector3(0,0,LocalR);

		}
	}
}
