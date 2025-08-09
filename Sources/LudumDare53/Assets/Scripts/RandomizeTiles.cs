using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeTiles : MonoBehaviour {

	public Sprite[] Tiles;
	public Sprite[] Alts;
	public bool BG; //no alts
	public SpriteRenderer SR;
	public SpriteRenderer SRAlt;

	public bool ForcePink;
	public bool ForceBlue;

	[ContextMenu("set SR")]
	void SetSR()
	{
		SR = GetComponent<SpriteRenderer> ();
		if (!BG) {
			SRAlt = transform.GetChild (0).GetComponent<SpriteRenderer> ();
		}
	}

	[ContextMenu("Random")]
	void Rand () {

		if (ForceBlue) {
			int R = Mathf.RoundToInt(Random.Range (0, 8));
			SR.sprite = Tiles [R];
			if (!BG) {
				SRAlt.sprite = Alts [R];
			}
		} else if (ForcePink) {
			int R = Mathf.RoundToInt(Random.Range (8, 16));
			SR.sprite = Tiles [R];
			if (!BG) {
				SRAlt.sprite = Alts [R];
			}
		} else {
			int R = Mathf.RoundToInt(Random.Range (0, 16));
			SR.sprite = Tiles [R];
			if (!BG) {
				SRAlt.sprite = Alts [R];
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
