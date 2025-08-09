using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageByVolume : MonoBehaviour {

	public Sprite V0;
	public Sprite V2;
	public Sprite V3;
	public Sprite V5;
	public Sprite V7;
	public Sprite V8;
	public Sprite V10;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().Volume == 0) {gameObject.GetComponent<SpriteRenderer> ().sprite = V0;}
		if (GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().Volume == 0.1f) {gameObject.GetComponent<SpriteRenderer> ().sprite = V2;}
		if (GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().Volume == 0.2f) {gameObject.GetComponent<SpriteRenderer> ().sprite = V2;}
		if (GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().Volume == 0.3f) {gameObject.GetComponent<SpriteRenderer> ().sprite = V3;}
		if (GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().Volume == 0.4f) {gameObject.GetComponent<SpriteRenderer> ().sprite = V3;}
		if (GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().Volume == 0.5f) {gameObject.GetComponent<SpriteRenderer> ().sprite = V5;}
		if (GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().Volume == 0.6f) {gameObject.GetComponent<SpriteRenderer> ().sprite = V7;}
		if (GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().Volume == 0.7f) {gameObject.GetComponent<SpriteRenderer> ().sprite = V7;}
		if (GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().Volume == 0.8f) {gameObject.GetComponent<SpriteRenderer> ().sprite = V8;}
		if (GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().Volume == 0.9f) {gameObject.GetComponent<SpriteRenderer> ().sprite = V8;}
		if (GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().Volume == 1) {gameObject.GetComponent<SpriteRenderer> ().sprite = V10;}





	}
}
