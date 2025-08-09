using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SactuaryText : MonoBehaviour {

	public float Timer;
	public TextMesh TM;

	public bool Title;
	public SpriteRenderer SR;

	public SpriteRenderer Credz;
	public GameObject Startgame;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
		Timer += Time.deltaTime;
		if(Title)
		{

			if(Timer < 4)
			{
				SR.color = new Vector4(1,1,1,Timer*0.25f);

			}
			else
			{

				Startgame.gameObject.SetActive(true);

			}

		}
		else
		{
			if(Timer < 1)
			{
				TM.color = new Vector4(1,1,1,Timer);

			}
			if(Timer > 2)
			{
				TM.color = new Vector4(1,1,1,3-Timer);
			}

			if(Timer > 3)
			{
				gameObject.SetActive(false);
			}
		}

	}
}
