using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonkable : MonoBehaviour {

	public SpriteRenderer SR;
	public float BonkTimer;
	public float StartY;
	public float Offset;
	public float Dur;
	public Sprite BonkSprite;
	public bool OneTimeUse;
	public bool Bonking;
	public bool Locked;
	public GameObject BumpCol;
	public GameObject Contents;
	public bool DoOnce;
	public bool Break;
	public GameObject Gibs;
	// Use this for initialization
	void Start () {
		StartY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {

		if(Break)
		{
			Instantiate(Gibs,transform.position,transform.rotation);
			Destroy(gameObject);
		}



		if(Bonking &&!Locked)
		{
			SR.sprite = BonkSprite;
			if(!DoOnce && Contents != null)
			{
				Instantiate(Contents,transform.position,transform.rotation,transform.parent);
				DoOnce = true;
			}
			BonkTimer += Time.deltaTime;
			Offset = Mathf.Sin((Mathf.PI / Dur) * BonkTimer);
			if(BonkTimer > Dur)
			{
				Offset = 0;
				Bonking= false;
				BonkTimer = 0;

				if(OneTimeUse)
				{
					Locked = true;
					BumpCol.tag = "OneWayCeiling";
				}

			}
			transform.position = new Vector3(transform.position.x,StartY + Offset,0);

		}



	}
}
