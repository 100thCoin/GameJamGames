using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGrid : MonoBehaviour {

	public Sprite GridPowered;
	public Sprite GridOn;
	public Sprite GridDead;

	public float Power;

	public bool Dead;

	// Use this for initialization
	void Start () {
		gameObject.name = "GridOn";

		Power = Random.Range (500, 2000);

	}
	
	// Update is called once per frame
	void Update () {

		if (Dead) {
			gameObject.name = "GridOff";
			gameObject.GetComponent<SpriteRenderer> ().sprite = GridDead;

		}


		if (!Dead) {


			if (Power < 0) {
				Dead = true;
			}

		}
	}


	void OnTriggerStay(Collider other)
	{
		
		if (other.gameObject.name == "Proto_Idle_1" && !Dead) {
			Power = Power - Time.deltaTime * 250;
			gameObject.GetComponent<SpriteRenderer> ().sprite = GridOn;



		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.name == "Proto_Idle_1" && !Dead) {
			gameObject.GetComponent<SpriteRenderer> ().sprite = GridPowered;



		}

	}


}
