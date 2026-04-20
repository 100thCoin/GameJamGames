using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeGameButton : MonoBehaviour {

	public TextMesh TM;
	public SpriteRenderer SR;
	public Sprite Unpressed;
	public Sprite Pressed;
	public BoxCollider Box;

	public TypeGameManager TypeGameMan;


	// Use this for initialization
	void Start () {
		
	}
		
	// Update is called once per frame
	void Update () {
		
	}

	public void CheckEmulatedMouseClick(Vector2 Click)
	{
		if (TypeGameMan.Submit) {
			return;
		}
		if (Click.x < transform.position.x + Box.bounds.extents.x && Click.x > transform.position.x - Box.bounds.extents.x && Click.y < transform.position.y + Box.bounds.extents.y && Click.y > transform.position.y - Box.bounds.extents.y) {

			TypeGameMan.AddChar (TM.text);
			TypeGameMan.HackyKeyPress = true;
			SR.sprite = Pressed;
			TM.transform.localPosition = new Vector3 (0, -0.047f, 0);
		}
	}

}
