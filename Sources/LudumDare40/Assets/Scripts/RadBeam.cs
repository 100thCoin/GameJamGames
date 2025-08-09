using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadBeam : MonoBehaviour {

	public Vector2 MousePos;



	// Use this for initialization
	void Start () {
		StartCoroutine (kill ());

		MousePos = GameObject.Find("Main Camera").gameObject.GetComponent<Camera>().ScreenToWorldPoint (Input.mousePosition);

		gameObject.transform.eulerAngles = new Vector3 (0, 0, Mathf.Atan2 (gameObject.transform.position.y - MousePos.y, gameObject.transform.position.x - MousePos.x) * Mathf.Rad2Deg);
		gameObject.transform.position = GameObject.Find ("BeamProjector").gameObject.transform.position;
		gameObject.transform.localScale = new Vector3 (Mathf.Pow(Mathf.Pow(MousePos.x - gameObject.transform.position.x,2) + Mathf.Pow(MousePos.y - gameObject.transform.position.y,2),0.5f)*2, 1, 1);

	}

	
	// Update is called once per frame
	void Update () 
	{
		
	
	MousePos = GameObject.Find("Main Camera").gameObject.GetComponent<Camera>().ScreenToWorldPoint (Input.mousePosition);

	gameObject.transform.eulerAngles = new Vector3 (0, 0, Mathf.Atan2 (gameObject.transform.position.y - MousePos.y, gameObject.transform.position.x - MousePos.x) * Mathf.Rad2Deg);
		gameObject.transform.position = GameObject.Find ("BeamProjector").gameObject.transform.position;

	

		gameObject.transform.localScale = new Vector3 (Mathf.Pow(Mathf.Pow(MousePos.x - gameObject.transform.position.x,2) + Mathf.Pow(MousePos.y - gameObject.transform.position.y,2),0.5f)*2, 1, 1);

	}

	IEnumerator kill()
	{
		yield return new WaitForSeconds (0.1f);
		Destroy (gameObject);

	}


}
