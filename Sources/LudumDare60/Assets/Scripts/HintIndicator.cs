using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintIndicator : MonoBehaviour {

	public float TimeSinceKnowledgeCheck;
	public SpriteRenderer SR;
	public Dialogue StartAfterDialogue;
	public Dialogue LeaveAfterDialogue;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (LeaveAfterDialogue.BeenHereBefore) {
			gameObject.SetActive (false);
		}

		if (TimeSinceKnowledgeCheck > 60*3) {
			SR.enabled = true;
		}

		if (StartAfterDialogue.BeenHereBefore) {
			TimeSinceKnowledgeCheck += Time.deltaTime;
		}

	}

	[ContextMenu("set")]
	void Set()
	{
		SR = GetComponent<SpriteRenderer> ();
	}

}
