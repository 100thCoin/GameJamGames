using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public Vector2 Vel;

	public GameObject Splat;

	public Dataholder Main;

	public bool donepaused;

	void Start()
	{
		Main = GameObject.Find("Main").GetComponent<Dataholder>();
	}

	void FixedUpdate()
	{
		if(Main.Paused && !donepaused)
		{
			donepaused = true;
			Vel = GetComponent<Rigidbody>().velocity;
			GetComponent<Rigidbody>().velocity = Vector3.zero;
		}
		else if(!Main.Paused && donepaused)
		{
			donepaused = false;;
			GetComponent<Rigidbody>().velocity = Vel;
		}
	}


	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Town")
		{
			if(GameObject.Find("Main").GetComponent<Dataholder>().Char.WaitForShotOnDragon)
			{
				return;
			}

			Instantiate(Splat,gameObject.transform.position,gameObject.transform.rotation);
			Destroy(gameObject);

		}

	}
}
