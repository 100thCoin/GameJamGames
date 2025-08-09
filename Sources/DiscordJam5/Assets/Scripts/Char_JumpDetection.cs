using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_JumpDetection : MonoBehaviour {

	public GameObject Char;
	public bool CanJump;
	public bool BufferJump;
	public bool BufferJump2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position = Char.transform.position;
		if(BufferJump)
		{
			BufferJump = false;
		}
		else if(BufferJump2)
		{
			BufferJump2 = false;
		}
		else
		{
			CanJump = false;
		}
	}

	void OnTriggerStay(Collider other)
	{
		if(other.tag == "Ground")
		{
			CanJump = true;
			BufferJump = true;
			BufferJump2 = true;
		}

	}

}
