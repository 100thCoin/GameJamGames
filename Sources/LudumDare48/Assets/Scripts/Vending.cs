using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vending : MonoBehaviour {

	public GameObject Key;
	public int KeyXLB;
	public int KeyYLB;

	public SpriteRenderer SR;
	public Sprite Vend_NoKey;

	public float VendDoTimer;

	public bool VendDo;
	public bool VendDid;

	public DataHolder Main;

	GameObject InstKey;

	public Animator Anim;
	public RuntimeAnimatorController VendSquish;

	// Use this for initialization
	void Start () {
		Main = GameObject.Find("Main").GetComponent<DataHolder>();
		Main.Vend = this;
	}
	
	// Update is called once per frame
	void Update () {

		if(VendDo)
		{
			if(!VendDid)
			{
				Instantiate(Main.SFX_Vend,transform.position,transform.rotation,Main.transform);
			}
			VendDid = true;
			VendDoTimer += Time.deltaTime;

			if(VendDoTimer > 0.3f)
			{
				VendDo = false;
				SR.sprite = Vend_NoKey;
				InstKey = Instantiate(Key,new Vector3(KeyXLB,KeyYLB,0),transform.rotation,transform.parent);
				InstKey.name = "Key";
				InstKey.GetComponent<Object_Move>().SpeedX = 10;
				InstKey.GetComponent<Object_Move>().SpeedY = 10;
				InstKey.GetComponent<Object_Move>().PosLBX = KeyXLB;
				InstKey.GetComponent<Object_Move>().PosLBY = KeyYLB;

			}
		}

	}
}
