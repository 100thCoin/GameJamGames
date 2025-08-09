using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour {

	public bool Opening;
	public Transform Padlock;
	public float Timer;
	public Vector3 SPos;
	public Vector3 DPos;
	public Transform Target;
	public float TimerSpeed = 0.1f;

	public bool ParticlesOnce;
	public GameObject LockParticles;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Opening) {
			Timer += Time.deltaTime*TimerSpeed;
		}
		Timer = Mathf.Clamp01 (Timer);
		float PadlockScale = Mathf.Clamp01(1 - Timer*3);

		if (Timer*3 < 1) {
			Padlock.localEulerAngles = new Vector3 (0, 0, (Timer*2 * 90) * (Timer*2 * 90));
			Padlock.localScale = new Vector3 (PadlockScale*4, PadlockScale*8, PadlockScale);


		} else {
			Padlock.gameObject.SetActive (false);
			if (!ParticlesOnce) {
				ParticlesOnce = true;
				Instantiate (LockParticles, Padlock.transform.position, LockParticles.transform.rotation,transform.parent.parent);

			}


		}


		Target.transform.localPosition = new Vector3 (
			DataHolder.SinLerp (SPos.x, DPos.x, Timer, 1),
			DataHolder.SinLerp (SPos.y, DPos.y, Timer, 1),
			DataHolder.SinLerp (SPos.z, DPos.z, Timer, 1));


	}



	void OnTriggerStay(Collider other)
	{

		if (other.CompareTag ("Key")) {
			Opening = true;



		}




	}



}
