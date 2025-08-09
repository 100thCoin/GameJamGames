using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenLightVariance : MonoBehaviour {
	public float Timer;
	public float Speed;

	// Use this for initialization
	void Start () {
		Timer = Random.Range(-10000.0f,10000.0f);
		Speed = Random.Range(0.8f,1.4f);
	}

	// Update is called once per frame
	void Update () {

		Timer += Time.deltaTime;
		transform.localScale= new Vector3(Mathf.Sin(Timer * Speed) *0.15f + Mathf.Sin(Timer * Speed * 2.77f) *0.08f + Mathf.Sin(Timer * Speed * 4.21f) *0.03f + 9,1,1);

}
}