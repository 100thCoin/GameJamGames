using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	public bool Open;
	public Animator Anim;
	public RuntimeAnimatorController OpeningDoorAnim;
	public SpriteRenderer SR;
	public Sprite DoorClose;
	public DataHolder Main;

	public Collider Col;

	public GameObject KeySpat;

	public bool SecondDoor;

	void Start()
	{
		Main = GameObject.Find("Main").GetComponent<DataHolder>();

	}

	void OnTriggerStay(Collider other)
	{
		if(Open)
		{
			return;
		}


		if(other.name == "Key")
		{
			Anim.runtimeAnimatorController = OpeningDoorAnim;
			Col.enabled = false;
			Open = true;
			Main.DoorOpen = 1;
			Instantiate(Main.SFX_Door,transform.position,transform.rotation,Main.transform);
			if(Main.Player.Holding)
			{
				if(Main.Player.HeldObject == other.gameObject)
				{
					Main.Player.Holding = false;

				}
			}
			Destroy(other.gameObject);
		}

	}

	void Update()
	{
		if(Main.closingDoor)
		{
			Open = false;
			Anim.runtimeAnimatorController = null;
			SR.sprite = DoorClose;
			Col.enabled = true;
			Main.closingDoorNextFrame = true;
		}
	}

}
