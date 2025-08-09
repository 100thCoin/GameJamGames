using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffMoney : MonoBehaviour {

	//oof

	public float Timer;


	public Sprite[] Rand;


	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer>().sprite = Rand[Random.Range(0,Rand.Length)];
		GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-3f,3f),Random.Range(2f,4f));
		GetComponent<Rigidbody>().AddTorque(new Vector3(0,0,Random.Range(1f,-1f))*10);
		gameObject.transform.position = new Vector3(gameObject.transform.position.x,transform.position.y,transform.position.z - 0.1f);

	}
	
	// Update is called once per frame
	void Update () {

		Timer -= Time.deltaTime;

		gameObject.transform.localScale = new Vector3(Timer,Timer,1);

		if(Timer < 0)
		{
			Destroy(gameObject);
		}

	}
}
