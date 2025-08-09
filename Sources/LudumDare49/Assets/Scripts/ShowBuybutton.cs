using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBuybutton : MonoBehaviour {

	public DataHolder Main;
	public int Price;
	public Sprite Red;
	public Sprite Blue;
	public Sprite Black;
	public SpriteRenderer White;
	public SpriteRenderer SR;
	public float WhiteTimer;
	public bool delay;
	public int ItemIndex;
	// Use this for initialization
	void Start () {
		
	}

	void Update()
	{
		if(WhiteTimer >0)
		{
			WhiteTimer -= Time.deltaTime*3;
			White.color = new Vector4(1,1,1,WhiteTimer);
		}
		if(delay)
		{
			delay = false;
		}
		else
		{
			SR.sprite = Black;
		}
	}
	
	// Update is called once per frame
	void OnMouseOver () {
		delay = true;
		if(Main.Score >= Price)
		{
			SR.sprite = Blue;
			if(Input.GetKeyDown(KeyCode.Mouse0))
			{
				WhiteTimer = 1;
				Main.Score -= Price;
				Main.Build.RemainingTiles[ItemIndex]++;
			}
		}
		else
		{
			SR.sprite = Red;
		}


	}
}
