using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {


	public bool Active;
	public GameObject PrefabSelf;

	public bool Projectile;
	public Vector3 ProjVel;

	public Rigidbody rb;

	public int Id;

	bool ded;
	public float DedTimer;
	// Use this for initialization
	void Start () {

		rb.velocity = ProjVel;

	}
	
	// Update is called once per frame
	void Update () {


			if(transform.position.x < -8.7f)
			{

			transform.position = new Vector3(-8.7f,transform.position.y,transform.position.z);

			}

		if(ded)
		{
			transform.position = new Vector3(0,-1000,-1500);

			DedTimer+= Time.deltaTime;
			if(DedTimer > 1)
			{
			Destroy(gameObject);
			}
		}


		if(Active && rb.isKinematic &&!ded)
		{
			GameObject.Find("Main").GetComponent<DataHolder>().HeldPickup = Id;
			transform.position = new Vector3(0,-1000,-1500);
			ded = true;
		}


	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Ground")
		{
			rb.isKinematic = true;

		}


	}


}
