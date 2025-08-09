using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBar : MonoBehaviour {

	public Sprite One;
	public Sprite Two;
	public Sprite three;
	public Sprite Pick;
	public Sprite Ladder;
	public Sprite Lantern;

	public GameObject Vis;

	public DataHolder Main;

	public RedicleFollow Red;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(!Main.UnlockedPick)
		{
			GetComponent<SpriteRenderer>().enabled =false;
			Vis.GetComponent<SpriteRenderer>().enabled =false;

		}
		else
		{
			GetComponent<SpriteRenderer>().enabled =true;
			Vis.GetComponent<SpriteRenderer>().enabled =true;
		}

		if(!Main.TUTHasEverLadderite)
		{
			GetComponent<SpriteRenderer>().sprite = One;
		}
		if(Main.TUTHasEverLadderite)
		{
			GetComponent<SpriteRenderer>().sprite = Two;
		}
		if(Main.TUTHasEverLantern)
		{
			GetComponent<SpriteRenderer>().sprite = three;
		}

		if(Red.Mode == 0)
		{
			Vis.GetComponent<SpriteRenderer>().sprite = Pick;
		}
		else if(Red.Mode == 1)
		{
			Vis.GetComponent<SpriteRenderer>().sprite = Ladder;
		}
		else if(Red.Mode == 2)
		{
			Vis.GetComponent<SpriteRenderer>().sprite = Lantern;
		}

	}
}
