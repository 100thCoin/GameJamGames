using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCardNoise : MonoBehaviour {

	public float MaxShake;
	public float MaxDim;
	public float Speed;
	public float Timer;

	public Transform T;
	public SpriteRenderer SR;
	public TextMesh TM;

	public bool Abridged;
	public bool Many;
	public SpriteRenderer[] SRs;
	public TextMesh[] TMs;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Timer += Time.deltaTime;
		if (Timer > 1f / Speed) {
			Timer -= (1f / Speed);
			float dim = Random.Range (1f - MaxDim, 1f);
			if (!Abridged && !Many) {
				SR.color = new Vector4 (dim, dim, dim, 1);
			}
			float offX = Random.Range (-MaxShake, MaxShake);
			float offY = Random.Range (-MaxShake, MaxShake);
			offX *= offX;
			offY *= offY;
			if (!Many) {
				TM.color = new Vector4 (dim, dim, dim, 1);
			} else {
				for (int i = 0; i < SRs.Length; i++) {
					SRs[i].color = new Vector4 (dim, dim, dim, 1);
				}
				for (int i = 0; i < TMs.Length; i++) {
					TMs[i].color = new Vector4 (dim, dim, dim, 1);
				}
			}
			T.transform.localPosition = new Vector3 (offX, offY, 0);

		}

	}
}
