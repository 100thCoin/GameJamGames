using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionSuccessfulCutscene : MonoBehaviour {

	public bool Happening;
	public float Timer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Happening) 
		{
			Timer += Time.deltaTime;
			
			float T1 = Mathf.Clamp01 (Timer - 1);
			float T2 = Mathf.Clamp01 (Timer - 3);
			float T3 = Mathf.Clamp01 (Timer - 4);

			Global.Dataholder.FailMSG[0].transform.localPosition = new Vector3 (Global.Dataholder.FailMSG[0].transform.localPosition.x, DataHolder.ParabolicLerp(-5,1,T1,1), 4);
			Global.Dataholder.FailMSG[1].transform.localPosition = new Vector3 (Global.Dataholder.FailMSG[1].transform.localPosition.x, DataHolder.ParabolicLerp(-5,-1,T2,1), 4);
			Global.Dataholder.FailMSG[2].transform.localPosition = new Vector3 (Global.Dataholder.FailMSG[2].transform.localPosition.x, DataHolder.ParabolicLerp(-5,-2,T3,1), 4);

			if (Super.Dataholder.GetReboundInputDown (KeyCode.Space)) {

				// reset level
				if (!Global.Dataholder.ResettingLevel) {
					Global.Dataholder.ResetLevelTimer = 0;
					Global.Dataholder.ResettingLevel = true;
					Global.Dataholder.FailedALevel = true;
				}

			}

		}
	}
}
