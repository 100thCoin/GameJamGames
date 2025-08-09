using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnPlayerNotStuck : MonoBehaviour {

	public MoveCharacter Char;

	// Use this for initialization
	void Start () {
		Char = GameObject.Find("Main").GetComponent<Dataholder>().Char;
	}
	
	// Update is called once per frame
	void Update () {

		if(!Char.WaitForShotOnDragon)
		{
			Destroy(gameObject);
		}

	}
}
