using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiChoiceButton : MonoBehaviour {

	public TextMesh TM;
	public SpriteRenderer SR;
	public Sprite Unpressed;
	public Sprite Pressed;
	public BoxCollider Box;

	public MultiChoiceManager MultiMan;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void CheckEmulatedMouseClick(Vector2 Click)
	{
		if (MultiMan.Submit || Global.Dataholder == null) {
			return;
		}
		Vector2 Offset = Vector2.zero;
		if (!Global.Dataholder.ShowTimeline) {
			Offset = new Vector2 (8f, 0);
		}
		if (Click.x + Offset.x < transform.position.x + Box.bounds.extents.x && Click.x + Offset.x > transform.position.x - Box.bounds.extents.x && Click.y < transform.position.y + Box.bounds.extents.y && Click.y > transform.position.y - Box.bounds.extents.y) {
			MultiMan.HackyKeyPress = true;
			SR.sprite = Pressed;
			//TM.transform.localPosition = new Vector3 (0, -0.047f, 0);
		}
	}
}
