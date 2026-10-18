using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCard : MonoBehaviour {

	public TextMesh TM;
	public bool Destruct;
	public float DestrucTimer;
	public SpriteRenderer Flash;
	public SpriteRenderer SR;
	public GameObject ExplodeParticles;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Destruct) {
			DestrucTimer += Time.deltaTime;
			float RandX = Random.Range (-1f, 1f);
			float RandY = Random.Range (-1f, 1f);
			RandX /= (1 + DestrucTimer * DestrucTimer * 200);
			RandY /= (1 + DestrucTimer * DestrucTimer * 200);
			transform.localPosition = new Vector3 (RandX, RandY, 0);

			Flash.color = new Vector4 (1, 1, 1, 1.5f - DestrucTimer * 7f);
			if (DestrucTimer > 0.24f) {
				SR.enabled = false;
				TM.gameObject.SetActive (false);
				ExplodeParticles.SetActive (true);
			}
			if (DestrucTimer > 2.5f) {
				Destroy (gameObject);
			}

		}

	}
}
