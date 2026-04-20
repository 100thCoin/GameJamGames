using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamblinatorButton : MonoBehaviour {

	public GamblinatorGame Gambliman;
	public BoxCollider Box;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CheckEmulatedMouseClick(Vector2 Click)
	{
		if (Gambliman.CurrentCard > Gambliman.Cards.Length) {
			return;
		}
		if (Click.x < transform.position.x + Box.bounds.extents.x && Click.x > transform.position.x - Box.bounds.extents.x && Click.y < transform.position.y + Box.bounds.extents.y && Click.y > transform.position.y - Box.bounds.extents.y) {

			Gambliman.Ping ();
		}
	}

}
