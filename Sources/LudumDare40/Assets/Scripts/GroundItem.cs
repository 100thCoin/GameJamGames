using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundItem : MonoBehaviour {

	public GameObject InstantiateItem;
	public GameObject InstantiatedItem;
	public bool NeverAgain;

	public int RandomInt;

	public Sprite R1;
	public Sprite R2;
	public Sprite R3;
	public Sprite R4;
	public Sprite R5;
	public Sprite R6;
	public Sprite R7;
	public Sprite R8;
	public Sprite R9;
	public Sprite R10;
	public Sprite R11;



	// Use this for initialization
	void Start () {

		RandomInt = Random.Range (1, 12);

		if (RandomInt == 1) {gameObject.GetComponent<SpriteRenderer> ().sprite = R1;}
		if (RandomInt == 2) {gameObject.GetComponent<SpriteRenderer> ().sprite = R2;}
		if (RandomInt == 3) {gameObject.GetComponent<SpriteRenderer> ().sprite = R3;}
		if (RandomInt == 4) {gameObject.GetComponent<SpriteRenderer> ().sprite = R4;}
		if (RandomInt == 5) {gameObject.GetComponent<SpriteRenderer> ().sprite = R5;}
		if (RandomInt == 6) {gameObject.GetComponent<SpriteRenderer> ().sprite = R6;}
		if (RandomInt == 7) {gameObject.GetComponent<SpriteRenderer> ().sprite = R7;}
		if (RandomInt == 8) {gameObject.GetComponent<SpriteRenderer> ().sprite = R8;}
		if (RandomInt == 9) {gameObject.GetComponent<SpriteRenderer> ().sprite = R9;}
		if (RandomInt == 10) {gameObject.GetComponent<SpriteRenderer> ().sprite = R10;}
		if (RandomInt == 11) {gameObject.GetComponent<SpriteRenderer> ().sprite = R11;}




		
	}
	
	// Update is called once per frame
	void Update () {

	}


	void OnTriggerEnter(Collider other)
	{


		if (other.gameObject.tag == "Player" && !NeverAgain) {
			NeverAgain = true;

			InstantiatedItem = Instantiate (InstantiateItem, gameObject.transform.position, gameObject.transform.rotation, GameObject.Find ("ItemHolder").gameObject.transform);
			InstantiatedItem.gameObject.GetComponent<SpriteRenderer> ().sprite = gameObject.GetComponent<SpriteRenderer> ().sprite;



			Destroy (gameObject);

		}



	}






}
