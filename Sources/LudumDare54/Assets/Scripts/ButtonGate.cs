using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGate : MonoBehaviour {

	public bool Invert;
	public GameObject Target;
	public Button TheButton;
	public Vector3 SPos;
	public Vector3 DPos;
	public float Timer;
	public float TimerSpeed;
	public GameObject AssociatedCollectible;
	public bool OpenWhenCollectibleGone;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		bool AlwaysOn = false;
		if (OpenWhenCollectibleGone) {
			if (AssociatedCollectible == null) {
				AlwaysOn = true;
			}
		}
		if (TheButton.ON == !Invert || AlwaysOn) {
			Timer += Time.deltaTime*TimerSpeed;
		} else {
			Timer -= Time.deltaTime*TimerSpeed;
		}
		Timer = Mathf.Clamp01 (Timer);

		Target.transform.localPosition = new Vector3 (
			DataHolder.SinLerp (SPos.x, DPos.x, Timer, 1),
			DataHolder.SinLerp (SPos.y, DPos.y, Timer, 1),
			DataHolder.SinLerp (SPos.z, DPos.z, Timer, 1));


	}
}
