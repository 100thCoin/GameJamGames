using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	public float Health;

	public Sprite Dim;
	public Sprite Glow;
	public Sprite Bright;
	public Sprite SuperBright;

	public GameObject Main;

	public bool Right;
	public bool Spawn;

	public float RandomNumber;
	public Vector2 spawnVector;
	// Update is called once per frame
	void Update () {
		if(Health == 1){gameObject.GetComponent<SpriteRenderer> ().sprite = Dim;}
		if(Health == 2){gameObject.GetComponent<SpriteRenderer> ().sprite = Glow;}
		if(Health == 3){gameObject.GetComponent<SpriteRenderer> ().sprite = Bright;}
		if(Health == 4){gameObject.GetComponent<SpriteRenderer> ().sprite = SuperBright;}

		if (Health == 5) {
			Main = GameObject.Find ("Main").gameObject;

			if(Spawn)
			{
			if (!Right) {
				RandomNumber = Random.Range (1, 8); //Goal 30
				spawnVector = new Vector2 (gameObject.transform.position.x, gameObject.transform.position.y + 7);

					if (RandomNumber == 1) {Instantiate (Main.GetComponent<DataHolder> ().URoom1, spawnVector, gameObject.transform.rotation,GameObject.Find("InGame(Clone)").transform);}
					if (RandomNumber == 2) {Instantiate (Main.GetComponent<DataHolder> ().URoom2, spawnVector, gameObject.transform.rotation,GameObject.Find("InGame(Clone)").transform);}
					if (RandomNumber == 3) {Instantiate (Main.GetComponent<DataHolder> ().URoom3, spawnVector, gameObject.transform.rotation,GameObject.Find("InGame(Clone)").transform);}
					if (RandomNumber == 4) {Instantiate (Main.GetComponent<DataHolder> ().URoom4, spawnVector, gameObject.transform.rotation,GameObject.Find("InGame(Clone)").transform);}
					if (RandomNumber == 5) {Instantiate (Main.GetComponent<DataHolder> ().URoom5, spawnVector, gameObject.transform.rotation,GameObject.Find("InGame(Clone)").transform);}
					if (RandomNumber == 6) {Instantiate (Main.GetComponent<DataHolder> ().URoom6, spawnVector, gameObject.transform.rotation,GameObject.Find("InGame(Clone)").transform);}
					if (RandomNumber == 7) {Instantiate (Main.GetComponent<DataHolder> ().URoom7, spawnVector, gameObject.transform.rotation,GameObject.Find("InGame(Clone)").transform);}

			}


			if (Right) {
				RandomNumber = Random.Range (1, 5); //Goal 30
				spawnVector = new Vector2 (gameObject.transform.position.x + 6, gameObject.transform.position.y -1);

					if (RandomNumber == 1) {Instantiate (Main.GetComponent<DataHolder> ().RRoom1, spawnVector, gameObject.transform.rotation,GameObject.Find("InGame(Clone)").transform);}
					if (RandomNumber == 2) {Instantiate (Main.GetComponent<DataHolder> ().RRoom2, spawnVector, gameObject.transform.rotation,GameObject.Find("InGame(Clone)").transform);}
					if (RandomNumber == 3) {Instantiate (Main.GetComponent<DataHolder> ().RRoom3, spawnVector, gameObject.transform.rotation,GameObject.Find("InGame(Clone)").transform);}
					if (RandomNumber == 4) {Instantiate (Main.GetComponent<DataHolder> ().RRoom4, spawnVector, gameObject.transform.rotation,GameObject.Find("InGame(Clone)").transform);}
					if (RandomNumber == 5) {Instantiate (Main.GetComponent<DataHolder> ().URoom5, spawnVector, gameObject.transform.rotation,GameObject.Find("InGame(Clone)").transform);}
					if (RandomNumber == 6) {Instantiate (Main.GetComponent<DataHolder> ().URoom6, spawnVector, gameObject.transform.rotation,GameObject.Find("InGame(Clone)").transform);}
					if (RandomNumber == 7) {Instantiate (Main.GetComponent<DataHolder> ().URoom7, spawnVector, gameObject.transform.rotation,GameObject.Find("InGame(Clone)").transform);}

			}
			}


			Main.gameObject.GetComponent<DataHolder> ().Room = Main.gameObject.GetComponent<DataHolder> ().Room + 1;
			Destroy (gameObject);
		}

	}

	void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.tag == "PlayerAttack") {

			Health = Health + 1;



		}


	}
}
