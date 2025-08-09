using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceQueue : MonoBehaviour {


	public Queue<GameObject> QueuedUp;
	public GameObject CurrentLoaded;


	// Use this for initialization
	void Start () {
		QueuedUp = new Queue<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (QueuedUp.Count > 0) {
			if (CurrentLoaded == null) {
				CurrentLoaded = Instantiate (QueuedUp.Dequeue ());
			}
		}

	}
}
