using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bones : MonoBehaviour {

	public Rigidbody RB;
	public SpriteRenderer SR;
	public Sprite[] Bone;

	void Start () {

		int r = Random.Range(0,Bone.Length);
		SR.sprite = Bone[r]; // ha

		RB.velocity = new Vector3(Random.Range(3f,-3f),Random.Range(2f,5f),0)*3;
		RB.angularVelocity = new Vector3(0,0,Random.Range(4f,-4f));
	}
	
	void FixedUpdate () {
		RB.AddForce(new Vector3(0,-5,0));
	}
}
