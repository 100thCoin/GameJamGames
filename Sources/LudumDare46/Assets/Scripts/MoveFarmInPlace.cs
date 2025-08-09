using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFarmInPlace : MonoBehaviour {

	float YTimer;
	public bool DoIt;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		if(DoIt)
		{
			YTimer+= Time.deltaTime;
			float YDuration = 1;

			if(YTimer < YDuration)
			{
				YTimer += Time.fixedDeltaTime;
				float LastYpos = -10;
				float TargetYpos = -3.5f;
				if(YTimer >= YDuration)
				{
					YTimer = YDuration;
				}
				float LocalYPos = (((LastYpos - TargetYpos) * Mathf.Pow(YTimer,2))/Mathf.Pow(YDuration,2) - ((2*LastYpos - 2*TargetYpos) * YTimer)/YDuration + LastYpos);
				transform.localPosition = new Vector3(32,LocalYPos,-1);
			}
		}
	}
}