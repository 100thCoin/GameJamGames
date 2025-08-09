using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour {

	public GravableTile GravTile;
	public SpriteRenderer GemSlot;
	public SpriteRenderer SR;
	public bool Collected;
	public Sprite GemCollected;
	public Sprite GemSlotCollected;

	public GameObject VoiceClipWhenCollected;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (Collected) {
			transform.position = (transform.position * 5 + new Vector3 (GemSlot.transform.position.x, GemSlot.transform.position.y, -1)) / 6f;

		}

	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (Collected) {
			return;
		}

		if (other.tag == "GemSlot") {
			Global.Dataholder.VoiceQueue.Add (VoiceClipWhenCollected);
			Collected = true;
			SR.sprite = GemCollected;
			GemSlot.sprite = GemSlotCollected;
			Global.Dataholder.ShockwaveObject.transform.position = new Vector3 ((transform.position.x ), (transform.position.y), -15);
			Global.Dataholder.ShockwaveObject.timer = 0;
		}
	}

}
