using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Ploogy : MonoBehaviour {


	public GameObject Follow;

	public GameObject DeadPloogy;
	public GameObject DeadPloogyBoom;
	public bool DieOnce;

	public GameObject Shost;

	public int RandomDoom;
	// Use this for initialization
	void Start () {

		gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (Random.Range(3.0f,-3.0f), Random.Range(3.0f,-3.0f), 0);


	}
	
	// Update is called once per frame
	void FixedUpdate () {
		gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (gameObject.GetComponent<Rigidbody> ().velocity.x, gameObject.GetComponent<Rigidbody> ().velocity.y- 0.1f, 0);

		if (GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().WInning) {
			Follow = GameObject.Find ("PloogyFollow");

		}

	}


	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Ground") {
			RandomDoom = Random.Range (1, 201);
			gameObject.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y + 0.02f, 0);

			if (RandomDoom != 50) {

				if (Follow.transform.position.x - gameObject.transform.position.x > 3) {
					gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (7, 4, 0);
				}

				if (Follow.transform.position.x - gameObject.transform.position.x < -3) {
					gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (-7, 4, 0);
				}

				if (Follow.transform.position.x - gameObject.transform.position.x > -3 && Follow.transform.position.x - gameObject.transform.position.x < 3) {
					gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (Random.Range (3.0f, -3.0f), 4, 0);
				}
			} else {

				gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (Random.Range (12.0f, -12.0f), Random.Range(5,8), 0);


			}

		}


		if (other.gameObject.tag == "PloogyBounce") {

			gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (gameObject.GetComponent<Rigidbody> ().velocity.x, gameObject.GetComponent<Rigidbody> ().velocity.y + 0.2f, 0);
			gameObject.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y + 0.02f, 0);
			if (Follow.transform.position.x - gameObject.transform.position.x > 3) {
				gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (6, 4, 0);
			}

			if (Follow.transform.position.x - gameObject.transform.position.x < -3) {
				gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (-6, 4, 0);
			}

			if (Follow.transform.position.x - gameObject.transform.position.x > -3 && Follow.transform.position.x - gameObject.transform.position.x < 3) {
				gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (Random.Range (3.0f, -3.0f), 4, 0);
			}

		}		
		if (other.gameObject.tag == "Safe" && !DieOnce && !GameObject.Find("Main").gameObject.GetComponent<DataHolder> ().NopMoving) {
			DieOnce = true;

			other.gameObject.transform.parent.gameObject.GetComponent<PloogyBox> ().PloogiesSaved += 1;

			Destroy (gameObject);


		}

		if (other.gameObject.tag == "Laser" && !DieOnce) {
			DieOnce = true;
			Follow.gameObject.GetComponent<Player> ().PloogyCount -= 1;
			Instantiate (DeadPloogy, new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,-3), DeadPloogy.gameObject.transform.rotation, gameObject.transform.gameObject.transform.parent);
			Instantiate (Shost, new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,-6), Shost.gameObject.transform.rotation, gameObject.transform.gameObject.transform.parent);
			GameObject.Find("Main").gameObject.GetComponent<DataHolder> ().DeadPloogies += 1;
			GameObject.Find ("Main").GetComponent<DataHolder> ().CurrentPloogies -= 1;

			Destroy (gameObject);

		}
		if (other.gameObject.tag == "Lava" && !DieOnce) {
			DieOnce = true;

			Follow.gameObject.GetComponent<Player> ().PloogyCount -= 1;
			Instantiate (DeadPloogyBoom, new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,-3), DeadPloogyBoom.gameObject.transform.rotation, gameObject.transform.gameObject.transform.parent);
			Instantiate (Shost, new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,-6), Shost.gameObject.transform.rotation, gameObject.transform.gameObject.transform.parent);
			GameObject.Find("Main").gameObject.GetComponent<DataHolder> ().DeadPloogies += 1;
			GameObject.Find ("Main").GetComponent<DataHolder> ().CurrentPloogies -= 1;

			Destroy (gameObject);

		}
		if (other.gameObject.tag == "SawBlade" && !DieOnce) {
			DieOnce = true;
			GameObject.Find("Main").gameObject.GetComponent<DataHolder> ().DeadPloogies += 1;

			GameObject.Find ("Main").GetComponent<DataHolder> ().CurrentPloogies -= 1;

			Follow.gameObject.GetComponent<Player> ().PloogyCount -= 1;
			Instantiate (DeadPloogy, new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,-3), DeadPloogy.gameObject.transform.rotation, gameObject.transform.gameObject.transform.parent);

			Instantiate (Shost, new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,-6), Shost.gameObject.transform.rotation, gameObject.transform.gameObject.transform.parent);
			Destroy (gameObject);

		}
		if (other.gameObject.tag == "Enemy" && !DieOnce) {
			GameObject.Find ("Main").GetComponent<DataHolder> ().CurrentPloogies -= 1;

			DieOnce = true;
			GameObject.Find("Main").gameObject.GetComponent<DataHolder> ().DeadPloogies += 1;

			Follow.gameObject.GetComponent<Player> ().PloogyCount -= 1;
			Instantiate (DeadPloogy, new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,-3), DeadPloogy.gameObject.transform.rotation, gameObject.transform.gameObject.transform.parent);
			Instantiate (Shost, new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,-6), Shost.gameObject.transform.rotation, gameObject.transform.gameObject.transform.parent);

			Destroy (gameObject);

		}

	}

}
