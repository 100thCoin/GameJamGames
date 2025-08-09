using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravProjectile : MonoBehaviour {

	public Transform[] Trail;
	public Vector3[] PrevPoses;

	public bool Locked;

	public Rigidbody2D RB;
	public SpriteRenderer LockedIn;

	public GameObject StardustDestroyer;

	public float DestroyOverTimeTimer = 8;

	public bool Granted;

	// Use this for initialization
	void Start () {
		if (Global.Dataholder.Upgrade_StardustRemover) {
			StardustDestroyer.SetActive (true);
		}
		DestroyOverTimeTimer = 8;
		for (int i = PrevPoses.Length-1; i >= 0; i--) {
			PrevPoses [i] = transform.position + new Vector3(0,0,-10);			
		}
	}

	void Update()
	{
		if (!Locked) {

			DestroyOverTimeTimer -= Time.deltaTime;
			if (DestroyOverTimeTimer <= 0) {
				Destroy (gameObject);
			}

		}
	}

	// Update is called once per frame
	void FixedUpdate () {

		if (!Locked && (transform.position - Global.Dataholder.PMov.gameObject.transform.position).magnitude > 32) {
			Destroy (gameObject);
		}

		for (int i = PrevPoses.Length-1; i >= 0; i--) {
			if (i == 0) {
				PrevPoses [i] = transform.position + new Vector3(0,0,-10);			
			} else {
				PrevPoses [i] = PrevPoses [i - 1];
			}
		}

		for (int i = 0; i < Trail.Length; i++) {
			if (i % 2 == 0) {
				Trail [i].transform.position = PrevPoses [i/2] + new Vector3(0,0,-i*0.1f);;

			} else {
				Trail [i].transform.position = (PrevPoses [i/2] + PrevPoses [(i/2)+1])/2f + new Vector3(0,0,-i*0.1f);

			}
		}

	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (!Locked) {

			bool needGrant = false;
			if (Global.Dataholder.TutorialModeNoMoving) {
				needGrant = true;

				if (other.gameObject.tag == "GrantTUT") {
					Granted = true;
				}

			}


			if (other.tag == "Ground" && (!needGrant || (needGrant && Granted))) {
				
				GravableTile Grav = other.GetComponent<GravableTile> ();
				if (Grav != null) {
					Grav.AddGravProj (this);
					transform.parent = Grav.transform;
					Locked = true;
					LockedIn.enabled = true;
					Destroy (RB);
				}
			} else {
				if (needGrant && !Granted) {
					Instantiate (Global.Dataholder.GravBallPop, transform.position, transform.rotation);
					Destroy (gameObject);
				}
			}
		}
	}

}
