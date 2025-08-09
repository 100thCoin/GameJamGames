using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NametagParticle : MonoBehaviour {

	public SpriteRenderer SR;
	public float Timer;
	public Vector3 vel;
	public TextMesh Name;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		Timer += Time.fixedDeltaTime;
		transform.position += vel * Time.fixedDeltaTime;
		transform.eulerAngles = new Vector3(0,0,Timer * 30);
		vel -= new Vector3(0,0.05f,0);
		SR.color = new Vector4(1,1,1,1-Timer*0.5f);
		Name.color = new Vector4(0,0,0,1-Timer*0.5f);
		if(Timer > 2)
		{
			Destroy(gameObject);
		}
	}
}
