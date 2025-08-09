using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGame : MonoBehaviour {


	public Transform Bar;
	public float Percent;

	public ShapeGameTile[] Tiles;

	public Collectible Reward;

	public bool Victory;
	public Sprite RewardVisible;
	public SpriteRenderer RewardSR;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		int c = 0;
		int i = 0;
		while (i < Tiles.Length) {

			if (Tiles [i].Active) {
				c++;
			}

			i++;
		}

		Percent = (c + 0f) / (Tiles.Length-1);

		if (c >= Tiles.Length - 2) {
			//victory! (with leniency)

			if (!Victory) {
				//do once
				Instantiate (Global.Dataholder.SFX_MinigameWin, transform.position, transform.rotation);
				Instantiate (Global.Dataholder.Sparkles, RewardSR.transform.position, transform.rotation);

				i = 0;
				while (i < Tiles.Length) {
					Tiles [i].Victory = true;

					i++;
				}
				Reward.Inactive = false;
				RewardSR.sprite = RewardVisible;
			}

			Victory = true;



		}

		if (Victory) {
			Percent = 1;
		}
		if (Bar != null) {

			Bar.transform.localScale = new Vector3 (Percent, 1, 1);
			Bar.transform.localPosition = new Vector3 (-0.5f + Percent * 0.5f, 0, -0.5f);
		}
	}
}
