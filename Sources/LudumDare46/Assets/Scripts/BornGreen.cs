using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BornGreen : MonoBehaviour {

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
				float LastYpos = 0;
				float TargetYpos = 1;
				if(YTimer >= YDuration)
				{
					YTimer = YDuration;
				}
				float LocalYPos = (((LastYpos - TargetYpos) * Mathf.Pow(YTimer,2))/Mathf.Pow(YDuration,2) - ((2*LastYpos - 2*TargetYpos) * YTimer)/YDuration + LastYpos);
				transform.localScale = new Vector3(LocalYPos,1,1);
			}
		}
	}

}
