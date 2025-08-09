using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {

	public float Timer;
	public GameObject Fireball;
	public Vector3 Vel;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (!Global.DataHolder.GameIsPaused) {
			Timer += Time.deltaTime;
		}

		if (Timer > 2) {
			Timer -= 2;
			GameObject T = Instantiate (Fireball,transform.position,transform.rotation,transform);
			T.GetComponent<MoveOverTime> ().Vel = Vel;
		}

	}
}
