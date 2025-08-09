using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

	public float timer;
	public float AnimSpeed;
	public SpriteRenderer SR;
	public SpriteRenderer White;
	public Sprite Anim1;
	public Sprite Anim2;
	public Sprite White1;
	public Sprite White2;

	public bool Alerted;
	public Sprite Point1;
	public Sprite Point2;
	public SpriteRenderer AlertRenderer;

	public bool Neighbor;
	public bool Todd;

	public bool ChaseNeighbor;
	public bool Day5Todd;

	// Use this for initialization
	void OnEnable () {
		if (Main.Data != null) { 
			if (Neighbor && Main.Data.NeighborIsDead) {
				Destroy (gameObject);
			}
			if (Todd && Main.Data.ToddIsDead) {
				Destroy (gameObject);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime * AnimSpeed;
		if (Alerted) {
			SR.sprite = (Mathf.FloorToInt (timer) % 2 == 1) ? Point1 : Point2;
		} else {
			
			SR.sprite = (Mathf.FloorToInt (timer) % 2 == 1) ? Anim1 : Anim2;
		}
		White.sprite = (Mathf.FloorToInt (timer) % 2 == 1) ? White1 : White2;

	}


}
