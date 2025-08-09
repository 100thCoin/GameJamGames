using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {

	public bool Sin;
	public Vector3 Vel;
	float sin;
	float sintimer;
	public float sinSpeed;
	// Use this for initialization
	Dataholder Main;
	void Start () {
		Main = GameObject.Find("Main").GetComponent<Dataholder>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Main.Paused)
		{
			return;
		}
		if(Sin)
		{
			sintimer += Time.fixedDeltaTime * sinSpeed;
			sin = Mathf.Sin(sintimer)+1;
			transform.localPosition += Vel * Time.fixedDeltaTime*sin;

		}
		else
		{
			transform.localPosition += Vel * Time.fixedDeltaTime;

		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Town")
		{

			Destroy(gameObject);

		}

	}

}
