using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeFallHolder : MonoBehaviour {

	public float Altitude;

	public GameObject Alert1;
	public GameObject Alert2;
	public GameObject Alert3;

	public bool[] DoItCheck;
	public GameObject Target;
	public float TargetMin;
	public float TargetMax;

	// Use this for initialization
	void Start () {

		Target.gameObject.transform.position = new Vector3(Random.Range(-8,8),-6,0);
		TargetMin = Target.gameObject.transform.position.x - 2;
		TargetMax = Target.gameObject.transform.position.x + 2;

	}
	
	// Update is called once per frame
	void Update () {
		Altitude -= Time.deltaTime * 100;

		if(Altitude <0)
		{
			Altitude =0;
		}

		Vector3 AlertSpawn = new Vector3(Random.Range(-11f,11f),-6,0);
		Vector3 Alert2Spawn = new Vector3(Random.Range(-10f,10f),-6,0);
		Vector3 Alert3Spawn = new Vector3(Random.Range(-9f,9f),-6,0);


		if(Altitude < 2950 && !DoItCheck[1]){DoItCheck[1] = true; Instantiate(Alert1,AlertSpawn,gameObject.transform.rotation,gameObject.transform);}
		if(Altitude < 2900 && !DoItCheck[2]){DoItCheck[2] = true; Instantiate(Alert1,AlertSpawn,gameObject.transform.rotation,gameObject.transform);}
		if(Altitude < 2350 && !DoItCheck[3]){DoItCheck[3] = true; Instantiate(Alert1,AlertSpawn,gameObject.transform.rotation,gameObject.transform);}
		if(Altitude < 2325 && !DoItCheck[4]){DoItCheck[4] = true; Instantiate(Alert1,AlertSpawn,gameObject.transform.rotation,gameObject.transform);}
		if(Altitude < 2300 && !DoItCheck[5]){DoItCheck[5] = true; Instantiate(Alert1,AlertSpawn,gameObject.transform.rotation,gameObject.transform);}
		if(Altitude < 2050 && !DoItCheck[6]){DoItCheck[6] = true; Instantiate(Alert2,Alert2Spawn,gameObject.transform.rotation,gameObject.transform);}
		if(Altitude < 1950 && !DoItCheck[7]){DoItCheck[7] = true; Instantiate(Alert2,Alert2Spawn,gameObject.transform.rotation,gameObject.transform);}
		if(Altitude < 1800 && !DoItCheck[8]){DoItCheck[8] = true; Instantiate(Alert1,AlertSpawn,gameObject.transform.rotation,gameObject.transform);}
		if(Altitude < 1500 && !DoItCheck[9]){DoItCheck[9] = true; Instantiate(Alert3,Alert3Spawn,gameObject.transform.rotation,gameObject.transform);}
		if(Altitude < 1000 && !DoItCheck[10]){DoItCheck[10] = true; Instantiate(Alert3,Alert3Spawn,gameObject.transform.rotation,gameObject.transform);}
		//if(Altitude < 1100 && !DoItCheck[11]){DoItCheck[11] = true; Instantiate(Alert2,Alert2Spawn,gameObject.transform.rotation,gameObject.transform);}
		if(Altitude < 900 && !DoItCheck[12]){DoItCheck[12] = true; Instantiate(Alert2,AlertSpawn,gameObject.transform.rotation,gameObject.transform);}
		//if(Altitude < 900 && !DoItCheck[13]){DoItCheck[13] = true; Instantiate(Alert1,AlertSpawn,gameObject.transform.rotation,gameObject.transform);}
		//if(Altitude < 900 && !DoItCheck[14]){DoItCheck[14] = true; Instantiate(Alert1,AlertSpawn,gameObject.transform.rotation,gameObject.transform);}
		if(Altitude < 800 && !DoItCheck[15]){DoItCheck[15] = true; Instantiate(Alert1,AlertSpawn,gameObject.transform.rotation,gameObject.transform);}





	}
}
