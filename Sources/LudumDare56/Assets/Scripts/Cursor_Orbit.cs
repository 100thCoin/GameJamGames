using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor_Orbit : MonoBehaviour {

	public float Radius = 0.5f;
	public float speed = 3;
	public float timer;
	public float Pos;
	public SpriteRenderer SR;

	public Sprite[] CreaturesByType;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		float prev = transform.localPosition.x;
		timer += Time.deltaTime;
		transform.localPosition = new Vector3 (Mathf.Cos ((timer + Pos* Mathf.PI*1.06f) * Mathf.PI * speed)*Radius, Mathf.Sin ((timer + Pos* Mathf.PI*1.06f) * Mathf.PI * speed)*Radius, 0);
		SR.flipX = transform.localPosition.x > prev;
		if(Global.Dataholder.CurrentHudSelectionAnimalType != -1)
		{
		SR.sprite = CreaturesByType [Global.Dataholder.CurrentHudSelectionAnimalType];
		}
	}
}
