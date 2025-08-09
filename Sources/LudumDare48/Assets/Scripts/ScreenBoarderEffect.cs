using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBoarderEffect : MonoBehaviour {

	public Transform Measure;
	public Transform Target;

	public float Timer;
	public float TimerSpeed;
	public float MeasureSpeed;
	public float Amp;

	public float Rand;
	public float Rand2;
	public float RotAmp;
	public float RotTimerSpeed;
	public float RotMeasureSpeed;
	// Use this for initialization
	void Start () {
		Rand = Random.Range(-10000,0);
		Rand2 = Random.Range(0.6f,1.2f);
		Amp *= Rand2;
		Rand2 = Random.Range(0.6f,1.2f);
		RotAmp *= Rand2;
		Rand2 = Random.Range(0.6f,1.2f);
		TimerSpeed *= Rand2;
		Rand2 = Random.Range(0.6f,1.2f);
		RotTimerSpeed *= Rand2;
		Rand2 = Random.Range(0.6f,1.2f);
		MeasureSpeed *= Rand2;
		Rand2 = Random.Range(0.6f,1.2f);
		RotMeasureSpeed *= Rand2;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		Timer += Time.fixedDeltaTime;
		Target.transform.localPosition = new Vector3(Mathf.Sin(Timer*TimerSpeed + Measure.transform.position.x*MeasureSpeed + Rand) + 1,0,0)*Amp;
		Target.transform.localEulerAngles = new Vector3(0,0,Mathf.Sin(Timer*RotTimerSpeed + Measure.transform.position.x*RotMeasureSpeed + Rand)*RotAmp);

	}

}
