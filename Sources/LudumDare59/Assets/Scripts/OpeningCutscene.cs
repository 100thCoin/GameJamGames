using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningCutscene : MonoBehaviour {

	// Use this for initialization
	void Start () {

		Global.Dataholder.ShowTimeline = false;
		Global.Dataholder.TimelineAppearTimer = 0;
		TASTimeline timeline = Global.Dataholder.TasTimeline.GetComponent<TASTimeline> ();
		timeline.LiveReactionButton.DoingTutorial = true;
		timeline.CompletedTutorial = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
