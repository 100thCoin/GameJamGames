using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

	public float Type;

	public float ID;
	public float IDCheck;

	public Sprite T1I1;
	public Sprite T1I2;
	public Sprite T1I3;
	public Sprite T1I4;
	public Sprite T1I5;

	public Sprite T2I1;
	public Sprite T2I2;
	public Sprite T2I3;

	public Sprite T3I1;
	public Sprite T3I2;
	public Sprite T3I3;
	public Sprite T3I4;
	public Sprite T3I5;
	public Sprite T3I6;

	public Sprite T4I1;
	public Sprite T4I2;
	public Sprite T4I3;

	public Sprite T5I1;
	public Sprite T5I2;

	public bool Ok;

	// Use this for initialization
	void Start () {
		StartCoroutine (Okify ());

		IDCheck = ID;
			
		Type = Random.Range (1, 6);

		if (Type == 1) {ID = Random.Range (1, 6);}
		if (Type == 2) {ID = Random.Range (1, 4);}
		if (Type == 3) {ID = Random.Range (1, 7);}
		if (Type == 4) {ID = Random.Range (1, 4);}
		if (Type == 5) {ID = Random.Range (1, 3);}



	}
	
	// Update is called once per frame
	void Update () {


		if (ID == 0) {
			Destroy (gameObject);
		}



		if (Type == 1) {
			if(ID == 1){gameObject.GetComponent<SpriteRenderer> ().sprite = T1I1;}
			if(ID == 2){gameObject.GetComponent<SpriteRenderer> ().sprite = T1I2;}
			if(ID == 3){gameObject.GetComponent<SpriteRenderer> ().sprite = T1I3;}
			if(ID == 4){gameObject.GetComponent<SpriteRenderer> ().sprite = T1I4;}
			if(ID == 5){gameObject.GetComponent<SpriteRenderer> ().sprite = T1I5;}
		}

		if (Type == 2) {
			if(ID == 1){gameObject.GetComponent<SpriteRenderer> ().sprite = T2I1;}
			if(ID == 2){gameObject.GetComponent<SpriteRenderer> ().sprite = T2I2;}
			if(ID == 3){gameObject.GetComponent<SpriteRenderer> ().sprite = T2I3;}
		}

		if (Type == 3) {
			if(ID == 1){gameObject.GetComponent<SpriteRenderer> ().sprite = T3I1;}
			if(ID == 2){gameObject.GetComponent<SpriteRenderer> ().sprite = T3I2;}
			if(ID == 3){gameObject.GetComponent<SpriteRenderer> ().sprite = T3I3;}
			if(ID == 4){gameObject.GetComponent<SpriteRenderer> ().sprite = T3I4;}
			if(ID == 5){gameObject.GetComponent<SpriteRenderer> ().sprite = T3I5;}
			if(ID == 6){gameObject.GetComponent<SpriteRenderer> ().sprite = T3I6;}
		}
		if (Type == 4) {
			if(ID == 1){gameObject.GetComponent<SpriteRenderer> ().sprite = T4I1;}
			if(ID == 2){gameObject.GetComponent<SpriteRenderer> ().sprite = T4I2;}
			if(ID == 3){gameObject.GetComponent<SpriteRenderer> ().sprite = T4I3;}
		}
		if (Type == 5) {
			if(ID == 1){gameObject.GetComponent<SpriteRenderer> ().sprite = T5I1;}
			if(ID == 2){gameObject.GetComponent<SpriteRenderer> ().sprite = T5I2;}
		}

		if (IDCheck != ID) {


			IDCheck = ID;

			Ok = false;
			StartCoroutine (Okify ());




		}

	}


	void OnTriggerEnter(Collider other)
	{
		if (Ok) {

			if (other.gameObject.tag == "PlayerAttack") {
				if (Type != 5) {

					gameObject.name = "ActiveObject";

					GameObject.Find ("Proto_Idle_1").gameObject.GetComponent<ProtoMovement> ().NewItemID = ID;
					GameObject.Find ("Proto_Idle_1").gameObject.GetComponent<ProtoMovement> ().NewItemType = Type;
					GameObject.Find ("Proto_Idle_1").gameObject.GetComponent<ProtoMovement> ().StartCoroutine (GameObject.Find ("Proto_Idle_1").gameObject.GetComponent<ProtoMovement> ().PickUp ());

				}

				if (Type == 5) {

					if (ID == 1) {
						GameObject.Find ("Proto_Idle_1").gameObject.GetComponent<ProtoMovement> ().Health = GameObject.Find ("Proto_Idle_1").gameObject.GetComponent<ProtoMovement> ().Health + 1000;
					}

					if (ID == 2) {
						GameObject.Find ("Proto_Idle_1").gameObject.GetComponent<ProtoMovement> ().Health = GameObject.Find ("Proto_Idle_1").gameObject.GetComponent<ProtoMovement> ().Health + 2500;
					}
					Destroy (gameObject);
				}


			}
		}
	}


	IEnumerator Okify()
	{
		yield return new WaitForSeconds (0.2f);
		Ok = true;

	}


}
