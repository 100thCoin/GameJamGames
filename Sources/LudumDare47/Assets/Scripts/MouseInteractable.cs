using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInteractable : MonoBehaviour {

	public Char_Movement Char;

	public SpriteRenderer SR;

	public bool Mouseover;

	public bool Scissors;
	public bool Knife;
	public bool Orc;


	public SpriteRenderer highlight;

	public MouseInteractable3D Threed;

	public SpriteRenderer OrcQuote;
	public Animator OrcAnim;
	public Sprite QuoteSprite;
	public GameObject OrcCol;
	public SpriteRenderer OrcHighlight;

	public TreasureChest Chest;
	public bool isChest;

	public bool disabled;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(disabled)
		{
			return;
		}

		if(Scissors)
		{
			highlight.enabled = Mouseover;
		}
		if(Knife)
		{
			highlight.enabled = Mouseover;
		}
		if(Orc)
		{
			OrcHighlight.color = OrcQuote.enabled && Mouseover ? new Vector4(1,1,1,1) : new Vector4(1,1,1,0);
		}
		if(isChest)
		{
			highlight.enabled = Chest.InRange && Mouseover;
		}
		if(Mouseover)
		{
			if(Input.GetKeyDown(KeyCode.Mouse0))
			{
				if(Scissors)
				{
					GameObject.Find("Main").GetComponent<DataHolder>().HaltTip2 = true;

					Char.CollectingItem = true;
					Char.ItemTimer = 0;
					Char.ItemPick.sprite = SR.sprite;
					Char.PlayerLoopFront.SetActive(true);
					GameObject.Find("Main").GetComponent<DataHolder>().HasScissors = true;
					GameObject.Find("Main").GetComponent<DataHolder>().CantSliceSelf.SetActive(true);
					GameObject.Find("Main").GetComponent<DataHolder>().ItemName.text = "Scissors";

					Destroy(Threed.gameObject);
					Destroy(gameObject);
				}

				if(Knife)
				{
					Char.CollectingItem = true;
					Char.ItemTimer = 0;
					Char.ItemPick.sprite = SR.sprite;
					Char.PlayerLoopFront.SetActive(true);
					GameObject.Find("Main").GetComponent<DataHolder>().HasKnife = true;
					GameObject.Find("Main").GetComponent<DataHolder>().ItemName.text = "Knife";

					Destroy(Threed.gameObject);
					Destroy(gameObject);
				}

				if(Orc && OrcQuote.enabled)
				{
					if(GameObject.Find("Main").GetComponent<DataHolder>().HasKnife)
					{
						GameObject.Find("Main").GetComponent<DataHolder>().HasKnife = false;
						GameObject.Find("Main").GetComponent<DataHolder>().OrcDead = true;
						highlight.gameObject.SetActive(false);
						OrcAnim.runtimeAnimatorController = GameObject.Find("Main").GetComponent<DataHolder>().OrcKnife;
						OrcQuote.sprite = QuoteSprite;
						OrcCol.transform.position = new Vector3(0,-500,0);

						disabled = true;
					}
				}

				if( isChest)
				{
					if(Chest.InRange)
					{
						Chest.Opening = true;
						highlight.enabled = false;
						highlight.gameObject.SetActive(false);
						Chest.Anim.runtimeAnimatorController = Chest.OpeningAnim;
						Destroy(Threed);
					}
				}

			}
		}

	}
}
