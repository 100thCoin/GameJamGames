using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finale : MonoBehaviour {

	public bool Started;
	public int InitFramecount;
	public TASTimeline Timeline;

	public GameObject RewindHand;

	public bool RewindTime;
	public float RewindAnim;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void TimelineUpdate () {

		if (!Started) {
			InitFramecount = Timeline.CurrentFrame;
		}

	}
}
