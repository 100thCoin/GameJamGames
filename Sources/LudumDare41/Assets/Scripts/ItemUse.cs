using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse : MonoBehaviour {

	public Vector2 MousePos;

	public GameObject laser;

	public float Timer;

	public bool Swing;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		MousePos = GameObject.Find("Main Camera").gameObject.GetComponent<Camera>().ScreenToWorldPoint (Input.mousePosition);

		//gameObject.transform.eulerAngles = new Vector3 (0, 0, Mathf.Atan2 (gameObject.transform.position.y - MousePos.y, gameObject.transform.position.x - MousePos.x) * Mathf.Rad2Deg + 90);

		if (gameObject.transform.eulerAngles.z > 180) {
			gameObject.transform.FindChild("S").transform.FindChild("Sword").gameObject.GetComponent<SpriteRenderer> ().flipX = true;

		} 		
		if (gameObject.transform.eulerAngles.z < 180) {
			gameObject.transform.FindChild("S").transform.FindChild("Sword").gameObject.GetComponent<SpriteRenderer> ().flipX = false;

		}

		if (Input.GetKeyDown (KeyCode.Mouse0)) {

			if(!Swing)
			{
				Swing = true;
			}
		}

		if(Swing)
		{
			gameObject.transform.localPosition = new Vector3(0,0,0);
			Timer = Timer+Time.deltaTime;
			gameObject.transform.FindChild("S").transform.FindChild("Sword").gameObject.GetComponent<SpriteRenderer> ().enabled = true;

			gameObject.transform.FindChild("S").transform.eulerAngles = new Vector3(0,0,Mathf.Atan2 (gameObject.transform.position.y - MousePos.y, gameObject.transform.position.x - MousePos.x) * Mathf.Rad2Deg - 225 - Timer*Mathf.Rad2Deg * 8);

			if(Timer > 0.2f)
			{
				Timer = 0;
				Swing = false;
				gameObject.transform.FindChild("S").transform.FindChild("Sword").gameObject.GetComponent<SpriteRenderer> ().enabled = false;
				gameObject.transform.localPosition = new Vector3(0,0,-500);
			}

		}

	}
}