using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckHuntButton : MonoBehaviour {

	public DuckHuntGame duckman;
	public BoxCollider Box;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void CheckEmulatedMouseClick(Vector2 Click)
	{
		if (duckman.Fired) {
			duckman.PingNoAmmo ();

			return;
		}
		if (Click.x < transform.position.x + Box.bounds.extents.x && Click.x > transform.position.x - Box.bounds.extents.x && Click.y < transform.position.y + Box.bounds.extents.y && Click.y > transform.position.y - Box.bounds.extents.y) {

			duckman.Ping ();
		}
	}
}
