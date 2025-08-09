using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSpriteTitle : MonoBehaviour {

	public Sprite N;
	public Sprite NM;
	public Sprite NU;
	public Sprite NUM;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
		if (!GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().Muted) {

			if (GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().LostTimes >= 14) {

				gameObject.GetComponent<SpriteRenderer> ().sprite = NUM;

			} else {
				gameObject.GetComponent<SpriteRenderer> ().sprite = NM;
					
			}



		} else {



			if (GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().LostTimes >= 14) {
				gameObject.GetComponent<SpriteRenderer> ().sprite = NU;
					

			} else {
				gameObject.GetComponent<SpriteRenderer> ().sprite = N;
			}


		}






	}
}
