using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoondustTiles : MonoBehaviour {

	public float timer;
	public Transform[] SpinnyDooDads;
	int randomrad;
	public bool ItsTheUIOne;
	// Use this for initialization
	void Start () {
		timer = Random.Range (-1000f, 0f);
		randomrad = Random.Range (-100, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if ((transform.position - Global.Dataholder.PMov.transform.position).magnitude < 32 || ItsTheUIOne) {
			timer += Time.deltaTime * 0.5f;
			for (int i = 0; i < SpinnyDooDads.Length; i++) {
				SpinnyDooDads [i].transform.localPosition = new Vector3 (Mathf.Sin (timer * Mathf.PI * 2 + 5 * i) + Mathf.Cos (timer * Mathf.PI * 4 + 13 * i), Mathf.Cos (timer * Mathf.PI * 2 + 23 * i) + Mathf.Sin (timer * Mathf.PI * 4 + 37 * i), 0) * Mathf.Cos (randomrad + i) * 0.333f;


			}
		}

	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "GravProjectile") {

			if (Global.Dataholder.Upgrade_StardustRemover) {
				Destroy (gameObject);
			}
		}
	}

}
