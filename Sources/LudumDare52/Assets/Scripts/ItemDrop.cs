using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour {

	public Vector3 Vel;
	public float Height;
	public float Gravity;
	public Transform SR;
	public Depth D;

	public int ItemId;

	public bool test;

	public GameObject PickupText;
	public bool DoOnce;

	public bool Lock;
	public float LockTimer;

	public GameObject VoiceSteal;
	// Use this for initialization
	void Start () {
		LockTimer = Random.Range (-10f, 10f);
	}
	
	// Update is called once per frame
	void Update () {

		if (Main.Data == null) {
			Destroy (gameObject);
		}

		if (!Lock) {
			if (test) {
				test = false;
				Vel = new Vector3 (Random.Range (-1f, 1f) * 0.05f, Random.Range (-1f, 1f) * 0.05f, Random.Range (1f, 2f) * 0.1f);

			}

			transform.position += new Vector3 (Vel.x, Vel.y, 0);
			Height += Vel.z;
			if (Height < 0) {
				Height = 0;
				Vel = new Vector3 (Vel.x * 0.5f, Vel.y * 0.5f, Vel.z * -0.5f);
			}
			SR.transform.localPosition = new Vector3 (0, Height, 0);
			D.Height = Height + 0.75f;
			Vel -= new Vector3 (0, 0, Gravity * Time.deltaTime);
		} else {
			LockTimer += Time.deltaTime;
			SR.localPosition = new Vector3 (0, Mathf.Sin (LockTimer) * 0.25f, 0);

		}
	}


	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player" &&!DoOnce) {
			DoOnce = true;
			PickupText PT = Instantiate (PickupText, transform.position, transform.rotation).GetComponent<PickupText> ();
			PT.Text = "+1 " + Main.Data.ItemNames [ItemId];
			if (Main.Data.ItemCounts [ItemId] == 0) {
				Main.Data.AddItem (ItemId);


			}
			Main.Data.ItemCounts [ItemId]++;
			if (ItemId == 5) {
				Main.Data.obj_Stealcrop = true;
				Main.Data.TotalCorn++;
				if (!Main.Data.EarlySteal) {
					Main.Data.EarlySteal = true;
					if (Main.Data.CurrentVoiceLine == null) {
						Main.Data.CurrentVoiceLine = Instantiate (VoiceSteal);
					} else {
						Main.Data.QueuedVoiceLine = VoiceSteal;
					}

				}
			
			}
			Destroy (gameObject);
		}

	}

}
