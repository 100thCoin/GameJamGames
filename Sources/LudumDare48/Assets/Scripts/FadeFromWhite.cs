using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeFromWhite : MonoBehaviour {

	public SpriteRenderer SR;
	public float Timer;
	public AudioSource AS;
	public float vol;
	public bool black;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		vol += Time.deltaTime*0.5f;
		Timer -= Time.deltaTime;
		if(!black)
		{
			AS.volume = vol;
		}
		SR.color = black ?  new Vector4(0,0,0,Timer) : new Vector4(1,1,1,Timer);

		if(Timer < 0)
		{
			Destroy(gameObject);
		}

	}
}
