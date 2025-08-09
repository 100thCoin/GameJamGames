using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillCrop : MonoBehaviour {

	public BillCropWobble Wobble;
	public Sprite[] RandSprite;
	public SpriteRenderer SR;
	public GameObject Drop;
	public GameObject Gibs;
	public int DropItemOfThisID;
	public bool doOnce;
	// Use this for initialization
	void Start () {

		SR.sprite = RandSprite [Random.Range (0, 4)];
		SR.flipX = Random.Range (0, 2) == 1;
		float tint = Random.Range (0.8f, 1f);
		SR.color = Random.Range (0, 2) == 1 ? new Vector4 (1, 1, Random.Range (0.6f, 1f), 1) : new Vector4 (tint, tint, tint, 1);

	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			Wobble.Timer = 0;
			Wobble.Amp = 15;
			Wobble.AngleRads = Mathf.Atan2 (transform.position.y - other.transform.position.y, transform.position.x - other.transform.position.x);
			Wobble.enabled = true;
		}
		if (other.tag == "Shovel" && !doOnce) {
			doOnce = true;
			ItemDrop ID = Instantiate (Drop, transform.position,transform.parent.rotation).GetComponent<ItemDrop>();
			Instantiate (Gibs, transform.position,transform.parent.rotation);
			ID.ItemId = DropItemOfThisID;
			ID.SR.GetComponent<SpriteRenderer> ().sprite = Main.Data.Items [DropItemOfThisID];
			Destroy (gameObject);
		}

	}


}
