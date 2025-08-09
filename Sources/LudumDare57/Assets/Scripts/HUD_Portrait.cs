using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD_Portrait : MonoBehaviour {

	public bool Active;
	public float ActiveTimer;
	public Animator Anim;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Active) {

			ActiveTimer += Time.deltaTime * 5;

		} else {
			ActiveTimer -= Time.deltaTime * 5;
		}

		ActiveTimer = Mathf.Clamp01 (ActiveTimer);

		transform.localScale = new Vector3 (1, (-1 / (8 * ActiveTimer + 1) + 1) / 0.8888888f, 1);

	}
}
