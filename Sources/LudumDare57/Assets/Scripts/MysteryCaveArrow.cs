using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryCaveArrow : MonoBehaviour {

	public SpriteRenderer SR;
	public float timer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Global.Dataholder.CountGems () == 2 && !Global.Dataholder.NecromooncerDefeated && !Global.Dataholder.CamMov.Boss1Camera) {
			timer += Time.deltaTime;
			if (timer > 0.5f) {
				SR.enabled = true;
			} else {
				SR.enabled = false;
			}
			if (timer > 1) {
				timer -= 1;
			}
		} else {
			SR.enabled = false;

		}
		
	}
}
