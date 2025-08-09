using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakingDamage : MonoBehaviour {

	public float HurtTimer;
	public GameObject Ouch;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		HurtTimer = HurtTimer - Time.fixedDeltaTime;
	}

	void OnTriggerStay(Collider other)
	{


		if(other.gameObject.tag == "Damage" && HurtTimer < 0)
		{
			GameObject.Find("Main").gameObject.GetComponent<DataHolder>().HP -= 1;

			HurtTimer = 1;
			Instantiate(Ouch,new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,gameObject.transform.position.z-0.1f),gameObject.transform.rotation);
		}

		if(other.gameObject.tag == "InstantDeath")
		{
			GameObject.Find("Main").gameObject.GetComponent<DataHolder>().HP =0;

		}


	}





}
