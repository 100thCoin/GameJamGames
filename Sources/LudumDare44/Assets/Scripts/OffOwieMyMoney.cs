using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffOwieMyMoney : MonoBehaviour {

	public Vector3 Vel;

	public float Timer;

	// Use this for initialization
	void Start () {
	
		Vel = new Vector3(Random.Range(0.2f,0.5f),new Vector2(Random.Range(-1f,1f),0).normalized.x * Random.Range(0.4f,0.1f),0);

	}

	
	// Update is called once per frame
	void FixedUpdate () {
		Timer += Time.fixedDeltaTime;
		gameObject.transform.position += Vel;
		Vel *= 0.8f;
		GetComponent<SpriteRenderer>().color = new Vector4(1,1,1,1-Timer);
		if(Timer > 1)
		{
			Destroy(gameObject);
		}
	}
}
