using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudButtonForORbChangeSprite : MonoBehaviour {

	public SpriteRenderer SR;
	public Sprite Off;
	public Sprite On;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		SR.sprite = Global.Dataholder.PlayerIsCarryingGoal ? On : Off;
	}
}
