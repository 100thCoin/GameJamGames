using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {

	public GameObject TargetDestroy;

	public GameObject EntireGame;

	// Use this for initialization
	void Start () {
	

		Destroy(TargetDestroy);
		StartCoroutine(Loadit());


	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator Loadit()
	{
		yield return new WaitForEndOfFrame();
		GameObject Game = Instantiate(EntireGame,new Vector3(0,0,0),gameObject.transform.rotation) as GameObject;
		Game.name = "Main";
		yield return new WaitForSeconds(0.3f);
		Game.SetActive(true);

		GetComponent<SpriteRenderer>().enabled = false;
		Destroy(gameObject);
	}

}
