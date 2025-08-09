using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRays : MonoBehaviour {

	public float ConstTimer;

	public SpriteRenderer sr;

	// Use this for initialization
	void Start () {

		ConstTimer += Random.Range(0,100f);

	}
	
	// Update is called once per frame
	void Update () {
		ConstTimer += Time.deltaTime;
		Color A = new Vector4(1,1,1,0.7f + Mathf.Sin(ConstTimer)*0.15f + Mathf.Sin(ConstTimer * 0.37f)*0.15f);

		sr.color = A;

	}
}
