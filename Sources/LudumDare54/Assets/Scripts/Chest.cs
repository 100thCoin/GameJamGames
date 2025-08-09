using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

	public SpriteRenderer SR;
	public SpriteRenderer LockMSG;
	public GameObject Reward;

	public int touchingPlayer;
	public bool Open;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (Open) {
			LockMSG.enabled = false;
			if (Reward != null) {
				Reward.SetActive (true);
			}
			SR.enabled = false;
			return;
		}

		if (touchingPlayer > 0) {

			LockMSG.enabled = true;

		} else {
			LockMSG.enabled = false;
		}
		touchingPlayer--;
	}



	void OnTriggerStay(Collider other)
	{

		switch (other.tag) {
		case "Player":
			{
				touchingPlayer = 6;
			}
			break;
		case "Key":
			{
				if (!Open) {
					Instantiate (Global.Dataholder.SFX_MinigameWin, transform.position, transform.rotation);
				}

				Open = true;
			}
			break;
		}




	}

}
