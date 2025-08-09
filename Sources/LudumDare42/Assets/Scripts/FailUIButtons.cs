using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailUIButtons : MonoBehaviour {

	public bool Quit;
	public bool Retry;

	public bool DoOnce;
	public bool UltraWin;
	public GameObject EntirePlayingGame;

	void OnMouseOver () {
		if(Input.GetKeyDown(KeyCode.Mouse0) &&Retry && !DoOnce)
		{
			DoOnce = true;
			GameObject InGame = Instantiate(EntirePlayingGame,Vector3.zero,gameObject.transform.rotation) as GameObject;
			InGame.name = "IG";
			gameObject.transform.parent.parent.gameObject.GetComponent<Whoop>().Grow = true;
			Destroy(GameObject.Find("InGame").gameObject);
			InGame.name = "InGame";
			gameObject.transform.parent.parent.gameObject.transform.position = new Vector3(0,0,-5);

			Destroy(gameObject.transform.parent.gameObject);
		}
		if(Input.GetKeyDown(KeyCode.Mouse0) &&Quit && !DoOnce)
		{
			DoOnce = true;
			GameObject WholeGame = Instantiate(EntirePlayingGame,Vector3.zero,gameObject.transform.rotation) as GameObject;
			WholeGame.name = "IG";
			gameObject.transform.parent.parent.gameObject.GetComponent<Whoop>().Grow = true;
			Destroy(GameObject.Find("InGame").gameObject);

			Destroy(GameObject.Find("Main").gameObject);
			WholeGame.name = "Main";
			if(UltraWin)
			{
				gameObject.transform.parent.parent.gameObject.transform.position = new Vector3(0,40,-5);

			}
			else
			{
			gameObject.transform.parent.parent.gameObject.transform.position = new Vector3(0,0,-5);
			}
			Destroy(gameObject.transform.parent.gameObject);
		}

	}
}
