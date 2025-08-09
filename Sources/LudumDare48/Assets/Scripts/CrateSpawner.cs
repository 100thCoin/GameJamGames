using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateSpawner : MonoBehaviour {

	public GameObject Crate;

	public DataHolder Main;

	// Use this for initialization
	void Start () {
		Main = GameObject.Find("Main").GetComponent<DataHolder>();
	}
	
	// Update is called once per frame
	void Update () {



	}


	void OnTriggerStay(Collider other)
	{

		if(other.name == "Key")
		{

			GameObject Create = Instantiate(Crate,transform.position,transform.rotation,transform.parent);
			Object_Move Mov = Create.GetComponent<Object_Move>();
			Create.name = "Crate";
			Mov.PhysState = "Physics";

			if(Main.Player.Holding)
			{
				if(Main.Player.HeldObject.gameObject == other.gameObject)
				{
					Main.Player.HeldObject = Mov;
					Main.PlayerHeld = "Crate";
					Main.PlayerHeldTimer = 0;
					Mov.Held = true;
					Mov.HeldBy = Main.Player;
					Mov.PhysState = "Held";
				}
			}
			Mov.PosLBX = Mathf.RoundToInt(transform.position.x);
			Mov.PosLBY = Mathf.RoundToInt(transform.position.y);
			Mov.SpeedY = 36;
			Destroy(other.gameObject);
			Destroy(gameObject);
		}

	}

}
