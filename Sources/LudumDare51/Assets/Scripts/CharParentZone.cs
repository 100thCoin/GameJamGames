using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharParentZone : MonoBehaviour {

	public bool grace;
	public bool Active;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {


		if(!grace)
		{
			if(Active)
			{
				Active = false;
				G.Main.CharMov.transform.parent = G.Main.InGame.transform;
			}
		}
		grace = false;

	}

	void OnTriggerStay(Collider other)
	{
		if(other.tag == "Player")
		{
			Active = true;
			grace = true;
			G.Main.CharMov.transform.parent = transform;
		}


	}

}
