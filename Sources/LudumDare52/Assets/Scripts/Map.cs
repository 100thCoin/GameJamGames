using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

	public float AppearTimer;
	public SpriteRenderer MapChar;
	public SpriteRenderer Player;

	public Sprite NewMap;
	public SpriteRenderer MapSR;

	public bool Active;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Main.Data.PromptKillNeighbor) {
			MapSR.sprite = NewMap;
		}

		if (Input.GetKeyDown (KeyCode.M)) {

			Active = !Active;
		}
		AppearTimer += Time.deltaTime * (Active ? 4 : -4);
		AppearTimer = Mathf.Clamp01 (AppearTimer);
		transform.localPosition = new Vector3 (0, DataHolder.SinLerp (-24, 0, AppearTimer, 1), 10);

		MapChar.flipX = Player.flipX;
		MapChar.transform.localPosition = new Vector3 (-8, 5, 0) + Player.transform.position * (1 / 4f);

		if (MapChar.transform.localPosition.x < -11.5f) {
			MapChar.transform.localPosition = new Vector3 (-11.5f, MapChar.transform.localPosition.y, 0);
		}
		if (MapChar.transform.localPosition.x > 11.5f) {
			MapChar.transform.localPosition = new Vector3 (11.5f, MapChar.transform.localPosition.y, 0);
		}
		if (MapChar.transform.localPosition.y < -11.5f) {
			MapChar.transform.localPosition = new Vector3 (MapChar.transform.localPosition.x, -11.5f, 0);
		}
		if (MapChar.transform.localPosition.y > 11.5f) {
			MapChar.transform.localPosition = new Vector3 (MapChar.transform.localPosition.x, 11.5f, 0);
		}

	}
}
