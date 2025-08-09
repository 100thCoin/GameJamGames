using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelIcon : MonoBehaviour {

	public Sprite Nineteen;
	public Sprite Eighteen;
	public Sprite Seventeen;
	public Sprite Sixteen;
	public Sprite Fifteen;
	public Sprite Fourteen;
	public Sprite Thirteen;
	public Sprite Twelve;
	public Sprite Eleven;
	public Sprite Ten;
	public Sprite Nine;
	public Sprite Eight;
	public Sprite Seven;
	public Sprite Six;
	public Sprite Five;
	public Sprite Four;
	public Sprite Three;
	public Sprite Two;
	public Sprite One;




	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Level == 19){gameObject.GetComponent<SpriteRenderer> ().sprite = Nineteen;}
		if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Level == 18){gameObject.GetComponent<SpriteRenderer> ().sprite = Eighteen;}
		if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Level == 17){gameObject.GetComponent<SpriteRenderer> ().sprite = Seventeen;}
		if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Level == 16){gameObject.GetComponent<SpriteRenderer> ().sprite = Sixteen;}
		if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Level == 15){gameObject.GetComponent<SpriteRenderer> ().sprite = Fifteen;}
		if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Level == 14){gameObject.GetComponent<SpriteRenderer> ().sprite = Fourteen;}
		if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Level == 13){gameObject.GetComponent<SpriteRenderer> ().sprite = Thirteen;}
		if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Level == 12){gameObject.GetComponent<SpriteRenderer> ().sprite = Twelve;}
		if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Level == 11){gameObject.GetComponent<SpriteRenderer> ().sprite = Eleven;}
		if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Level == 10){gameObject.GetComponent<SpriteRenderer> ().sprite = Ten;}
		if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Level == 9){gameObject.GetComponent<SpriteRenderer> ().sprite = Nine;}
		if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Level == 8){gameObject.GetComponent<SpriteRenderer> ().sprite = Eight;}
		if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Level == 7){gameObject.GetComponent<SpriteRenderer> ().sprite = Seven;}
		if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Level == 6){gameObject.GetComponent<SpriteRenderer> ().sprite = Six;}
		if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Level == 5){gameObject.GetComponent<SpriteRenderer> ().sprite = Five;}
		if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Level == 4){gameObject.GetComponent<SpriteRenderer> ().sprite = Four;}
		if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Level == 3){gameObject.GetComponent<SpriteRenderer> ().sprite = Three;}
		if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Level == 2){gameObject.GetComponent<SpriteRenderer> ().sprite = Two;}
		if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Level == 1){gameObject.GetComponent<SpriteRenderer> ().sprite = One;}




	}
}
