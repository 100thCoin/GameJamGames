using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScreenFlash : MonoBehaviour {

	public float timer;
	public SpriteRenderer SR;

	// Use this for initialization
	void Start () {
		Destroy(Global.Dataholder.LeadIntoBooWomp.gameObject);
		Destroy(Global.Dataholder.MainMusic.gameObject);

	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime * 10;
		transform.localScale = new Vector3 (1, 1 / (timer*timer + 0.001f), 1);
		SR.color = new Vector4 (1, 1, 1, 20 - timer*0.5f);
		if (timer > 50) {
			Global.Dataholder.Pseudogame.SetActive (true);
			Global.Dataholder.Game.SetActive (false);
		}
	}
}
