using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIItems : MonoBehaviour {

	public Sprite ID0; 
	public Sprite ID1;
	public Sprite ID2;
	public Sprite ID3;
	public Sprite ID4;
	public Sprite ID5;
	public Sprite ID6;


	public float Type;

	public float ID;

	public bool Ok;

	// Use this for initialization

	// Update is called once per frame
	void Update () {




		if (Type == 1) {

			ID = GameObject.Find ("Proto_Idle_1").gameObject.GetComponent<ProtoMovement> ().ItemType1;

		}
		if (Type == 2) {

			ID = GameObject.Find ("Proto_Idle_1").gameObject.GetComponent<ProtoMovement> ().ItemType2;

		}
		if (Type == 3) {

			ID = GameObject.Find ("Proto_Idle_1").gameObject.GetComponent<ProtoMovement> ().ItemType3;

		}
		if (Type == 4) {

			ID = GameObject.Find ("Proto_Idle_1").gameObject.GetComponent<ProtoMovement> ().ItemType4;

		}



		if(ID == 0){gameObject.GetComponent<SpriteRenderer> ().sprite = ID0;}
		if(ID == 1){gameObject.GetComponent<SpriteRenderer> ().sprite = ID1;}
		if(ID == 2){gameObject.GetComponent<SpriteRenderer> ().sprite = ID2;}
		if(ID == 3){gameObject.GetComponent<SpriteRenderer> ().sprite = ID3;}
		if(ID == 4){gameObject.GetComponent<SpriteRenderer> ().sprite = ID4;}
		if(ID == 5){gameObject.GetComponent<SpriteRenderer> ().sprite = ID5;}
		if(ID == 6){gameObject.GetComponent<SpriteRenderer> ().sprite = ID6;}


	}




}
