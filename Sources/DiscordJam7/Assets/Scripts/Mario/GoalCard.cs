using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCard : MonoBehaviour {

	public int Timer;
	public float Timer2;
	public Sprite[] Cards;
	public SpriteRenderer SR;
	public bool Happened;
	public SpriteRenderer Clear;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(!Happened)
		{
		Timer++;

			if(Timer <= 5)
			{
				SR.sprite = Cards[0];
			}
			else if(Timer <= 10)
			{
				SR.sprite = Cards[1];
			}
			else
			{
				SR.sprite = Cards[2];
			}
			if(Timer == 15)
			{
				Timer = 0;
			}
		}
		else
		{
			Timer2 += Time.fixedDeltaTime;
			transform.position += new Vector3(0,Time.deltaTime*15,0);
			transform.localScale = new Vector3(Mathf.Sin(Timer2*8),1,1);
			if(transform.position.y > 16 && transform.position.y < 32)
			{
				Clear.enabled = true;
				Main.DataHolder.MarioReturnToMapTimer = 0;

			}
			if(transform.position.y > 32)
			{

				Main.DataHolder.MarioLevelClear = true;
				Main.DataHolder.MarioReturnToMap = true;
				Main.DataHolder.MarioLevel1Clear = true;
				if(Main.DataHolder.MarioLevelID == 1)
				{
					Main.DataHolder.MarioLevel2Clear = true;
				}
			}

		}
	}
}
