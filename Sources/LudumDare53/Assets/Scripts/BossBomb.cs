using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBomb : MonoBehaviour {

	public Vector3 Vel;

	public Rigidbody RB;

	public float Speed;

	public GameObject Explosion;

	public bool DoOnce;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Global.DataHolder.GameIsPaused) {
			RB.velocity = Vector3.zero;
			return;
		}


		RB.velocity = Vel.normalized * Speed;
	}

	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Ground" || other.tag == "Ceiling" || other.tag == "LeftWall" || other.tag == "RightWall" || other.tag == "Break") {
			if (!DoOnce) {
				DoOnce = true;

				Instantiate (Explosion, transform.position, transform.rotation, transform.parent.parent);
				Global.DataHolder.QueueBombSFX = true;
				Destroy (gameObject);
			}



		}


	}


}
