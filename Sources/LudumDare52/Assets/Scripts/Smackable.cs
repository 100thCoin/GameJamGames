using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smackable : MonoBehaviour {

	public float HitTimer;
	public bool Hit;
	public SpriteRenderer White;
	public Vector3 SPos;
	public Vector2 Wiggle;
	public Transform Vis;

	public GameObject Particles;

	public Collider Col;
	public Depth Depthh;

	public bool Neighbor;
	public bool Todd;
	public GameObject ToddDropsThis;
	public GameObject Scream;
	public bool Chicken;
	public bool OtherPerson;

	// Use this for initialization
	void Start () {
		SPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		if (Hit) {


			Depthh.UpdateEveryFrame = false;
			HitTimer += Time.deltaTime;
			if (HitTimer < 0.05f) {
				White.color = new Vector4 (0, 0, 0, 1);
			} else if (HitTimer < 0.13) {
				float S = (HitTimer - 0.05f) * 8;
				White.color = new Vector4 (1, 1, 1, 1 - (HitTimer - 0.05f) * 8);
				S *= 0.25f;
				Wiggle = new Vector2 (Random.Range (-1f, 1f) * S, Random.Range (-1f, 1f) * S);
				Vis.transform.position = new Vector3 (Wiggle.x, Wiggle.y, 0) + SPos;
			} else if (HitTimer < 0.25f) {
				White.color = new Vector4 (1, 1, 1, 0);
			} else if (HitTimer < 0.375f){
				float S = (HitTimer - 0.25f) * 8;
				White.color = new Vector4 (1, 1, 1, S);
				S *= 0.5f;
				Wiggle = new Vector2 (Random.Range (-1f, 1f) * S, Random.Range (-1f, 1f) * S);
				Vis.transform.position = new Vector3 (Wiggle.x, Wiggle.y, 0) + SPos;
			}
			else {

				if (Neighbor) {
					Main.Data.NeighborIsDead = true;
					if (Scream != null) {
						Instantiate (Scream);
					}
					Main.Data.PeopleKilled++;
					Main.Data.PMov.KillFilter = 1;

					if (Main.Data.PromptKillNeighbor) {

						if (Main.Data.CurrentVoiceLine == null) {
							Main.Data.CurrentVoiceLine = Instantiate (Main.Data.VoiceKill);
						} else {
							Main.Data.QueuedVoiceLine = Main.Data.VoiceKill;
						}

					} else {

						if (Main.Data.CurrentVoiceLine == null) {
							Main.Data.CurrentVoiceLine = Instantiate (Main.Data.VoiceEarlyKill);
						} else {
							Main.Data.QueuedVoiceLine = Main.Data.VoiceEarlyKill;
						}

					}


				}
				if (Todd) {
					Main.Data.ToddIsDead = true;
					ItemDrop ID = Instantiate (ToddDropsThis, transform.position, transform.parent.rotation).GetComponent<ItemDrop> ();
					ID.ItemId = 11;
					ID.SR.GetComponent<SpriteRenderer> ().sprite = Main.Data.Items [11];
					if (Scream != null) {
						
						Instantiate (Scream);
					}
					Main.Data.PeopleKilled++;
					Main.Data.PMov.KillFilter = 1;

				}
				if (Chicken) {
					ItemDrop ID = Instantiate (ToddDropsThis, transform.position,transform.parent.rotation).GetComponent<ItemDrop>();
					ID.ItemId = 6;
					ID.SR.GetComponent<SpriteRenderer> ().sprite = Main.Data.Items [6];
					Main.Data.KilledChicken = true;
				}

				if (OtherPerson) {
					Main.Data.PeopleKilled++;
					Main.Data.PMov.KillFilter = 1;
				}

				//explode
				Instantiate(Particles,transform.position,transform.rotation);
				Destroy(gameObject);
			}

		}


	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Shovel") {

			Hit = true;
			Col.enabled = false;
		}


	}



}
