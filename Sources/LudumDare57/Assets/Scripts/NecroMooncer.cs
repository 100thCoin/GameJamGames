using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecroMooncer : MonoBehaviour {

	public Transform[] Trail;
	public Vector3[] PrevPoses;

	public int phase;
	public float spinTimer;
	public float Radius;
	public float IFrames = 0;

	public GameObject Ouch1;
	public GameObject Ouch2;

	public NecromooncerReturnGemCutscene PostKillCut;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate () {
		IFrames -= Time.deltaTime;
		for (int i = PrevPoses.Length-1; i >= 0; i--) {
			if (i == 0) {
				PrevPoses [i] = transform.position;
			} else {
				PrevPoses [i] = PrevPoses [i - 1];
			}
		}



		if (phase == 0) {

		} else if (phase == 1) {
			for (int i = 0; i < Trail.Length; i++) {
				Trail [i].transform.position = (PrevPoses [i * 4]);
			}
			spinTimer += Time.deltaTime * 0.5f;
			transform.localPosition = new Vector3 (Mathf.Sin (spinTimer * Mathf.PI), Mathf.Cos (spinTimer * Mathf.PI), 0)*Radius;
		} else if (phase == 2) {
			for (int i = 0; i < Trail.Length; i++) {
				Trail [i].transform.position = (PrevPoses [i * 4]);
			}
			spinTimer += Time.deltaTime * 0.33f;
			transform.localPosition = new Vector3 (Mathf.Sin (spinTimer * Mathf.PI *2) + Mathf.Cos (spinTimer * Mathf.PI *4), Mathf.Cos (spinTimer * Mathf.PI * 2) + Mathf.Sin (spinTimer * Mathf.PI *4), 0)*Radius*0.5f;
				

		} else {
			for (int i = 0; i < Trail.Length; i++) {
				Trail [i].transform.position = new Vector3 (0, 5000, 0);
				if (PostKillCut != null) {
					PostKillCut.DoIt = true;
				}
			}
		}
			


	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "Ground" && IFrames < 0) {
			IFrames = 1;
			phase++;
			if (phase == 1) {
				Global.Dataholder.VoiceQueue.Add (Ouch1);
			}
			if (phase == 2) {
				Global.Dataholder.VoiceQueue.Add (Ouch2);
			}
		}
	}

}
