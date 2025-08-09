using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadGun : MonoBehaviour {

	public Vector2 MousePos;

	public GameObject laser;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		MousePos = GameObject.Find("Main Camera").gameObject.GetComponent<Camera>().ScreenToWorldPoint (Input.mousePosition);

		gameObject.transform.eulerAngles = new Vector3 (0, 0, Mathf.Atan2 (gameObject.transform.position.y - MousePos.y, gameObject.transform.position.x - MousePos.x) * Mathf.Rad2Deg + 90);

		if (gameObject.transform.eulerAngles.z > 180) {
			gameObject.transform.Find("RadGun").gameObject.GetComponent<SpriteRenderer> ().flipY = false;

		} 		
		if (gameObject.transform.eulerAngles.z < 180) {
			gameObject.transform.Find("RadGun").gameObject.GetComponent<SpriteRenderer> ().flipY = true;

		}

		if (Input.GetKeyDown (KeyCode.Mouse0)) {

			if (!GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().NopMoving) {
				Instantiate (laser, gameObject.transform.position, gameObject.transform.rotation);
			}
		}

	}
}
