using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RibbonColl : MonoBehaviour {

	public bool InTheChaseGame;

	public int Grace;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (Grace <= 0) {

			InTheChaseGame = false;

		}

		Grace--;

	}

	void OnTriggerStay(Collider other)
	{

		if(other.CompareTag("ChaseGame"))
		{

			InTheChaseGame = true;
			Grace = 7;
		}

	}

}
