using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour {

	public float Timer;
	public bool Active;
	public float DestElevation;
	public GameObject Particles;
	public bool TouchingGround;

	public int Dir;
	public float Speed;

	public GameObject Temp;
	public float TempTimer;

	public bool smash;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(Temp != null)
		{
		if(Temp.gameObject.activeSelf)
		{
			TempTimer -= Time.deltaTime;
			if(TempTimer <0)
			{
				Temp.SetActive(false);
			}

		}
		}
		if(Active && !smash)
		{
			Timer+= Time.fixedDeltaTime;

			Speed -= Time.fixedDeltaTime*2;
			transform.position += new Vector3(Speed*Dir * 1.2f,0,0);

			if(Timer > 0.25f)
			{
				Active = false;
				if(!TouchingGround)
				{
					transform.position = new Vector3(transform.position.x,DestElevation,0.1f);
					Instantiate(Particles,transform.position,Particles.transform.rotation,transform.parent);
					smash = true;
					if(Temp != null)
					{
						Temp.SetActive(true);
					}
				}
			}

		}

		TouchingGround = false;
	}

	void OnTriggerStay(Collider other)
	{
		if(other.tag == "Ground")
		{
			TouchingGround = true;
		}
		if(other.tag == "ForceStopBoulder")
		{
			smash = true;
		}
	}

}
