using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScale : MonoBehaviour {

	public GameObject Explosion;
	public bool SpawnAnother;
	public float Timer;
	// Use this for initialization
	void Start () {
		transform.localScale = Vector3.one * Random.Range(1.5f,3.5f);
		transform.eulerAngles = new Vector3(0,0,Random.Range(-360f,360f));
		SpawnAnother = Random.Range(0,3) == 2;
		if(!SpawnAnother)
		{
			Destroy(this);
		}
		else
		{
			Timer = Random.Range(0.25f,0.5f);
		}
	}

	void Update()
	{
		Timer -= Time.deltaTime;
		if(Timer < 0)
		{

			Vector3 Offset = new Vector3(Random.Range(-4f,4f),Random.Range(-4f,4f));

			Instantiate(Explosion,transform.position + Offset,transform.rotation);
			Destroy(this);
		}

	}


}
