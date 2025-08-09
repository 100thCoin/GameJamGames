using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowFly : MonoBehaviour {

	public Transform Target;
	public float Timer;
	public float Height;
	public Transform Shadow;
	public float Speed;
	public Depth D;
	// Use this for initialization
	public Transform Player;
	public SpriteRenderer SR;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Timer += Time.deltaTime;



	}

	void FixedUpdate()
	{
		transform.position = new Vector3 ((transform.position.x * Speed + Target.transform.position.x) / (Speed + 1), (transform.position.y * Speed + Target.transform.position.y) / (Speed + 1), 0);
		D.Manual();
		SR.flipX = (transform.position.x < Player.transform.position.x);

	}
}
