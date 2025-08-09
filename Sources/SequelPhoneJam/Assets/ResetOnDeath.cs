using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetOnDeath : MonoBehaviour {

	public GameObject CurrentMain;
	public GameObject MainPrefab;

	// Use this for initialization
	void OnEnable () {
		CurrentMain = GameObject.Find("Main").gameObject;
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Space))
		{
			GameObject M = Instantiate(MainPrefab,Vector3.zero,transform.rotation);
			Destroy(CurrentMain);
			M.transform.GetChild(0).GetComponent<Environment>().PlayerDiedScreen = gameObject;
			M.name = "Main";
			gameObject.SetActive(false);
		}

	}
}
