using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {
	public Sprite Dim;
	public Sprite Glow;
	public Sprite Bright;
	public Sprite SuperBright;

	public Sprite Broke;

	public GameObject Item;


	public float Health;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Health == 1){gameObject.GetComponent<SpriteRenderer> ().sprite = Dim;}
		if(Health == 2){gameObject.GetComponent<SpriteRenderer> ().sprite = Glow;}
		if(Health == 3){gameObject.GetComponent<SpriteRenderer> ().sprite = Bright;}
		if(Health == 4){gameObject.GetComponent<SpriteRenderer> ().sprite = SuperBright;}
		if(Health == 5){gameObject.GetComponent<SpriteRenderer> ().sprite = Broke;
			Instantiate(Item,new Vector3(gameObject.transform.position.x,gameObject.transform.position.y + 0.5f,gameObject.transform.position.z + 1),gameObject.transform.rotation);
			Health = 6;
		}




	}

	void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.tag == "PlayerAttack") {

			Health = Health + 1;



		}


	}



}
