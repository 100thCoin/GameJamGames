using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GEN : MonoBehaviour {

	public int Rand;

	public bool DoOnce;

	public GameObject C1;
	public GameObject C2;
	public GameObject C3;
	public GameObject C4;
	public GameObject C5;
	public GameObject C6;
	public GameObject C7;
	public GameObject C8;
	public GameObject C9;
	public GameObject C10;
	public GameObject C11;
	public GameObject C12;
	public GameObject C13;
	public GameObject C14;
	public GameObject C15;
	public GameObject C16;
	public GameObject C17;
	public GameObject C18;
	public GameObject C19;
	public GameObject C20;
	public GameObject C21;
	public GameObject C22;
	public GameObject C23;
	public GameObject C24;
	public GameObject C25;
	public GameObject C26;
	public GameObject C27;
	public GameObject C28;
	public GameObject C29;
	public GameObject C30;
	public GameObject C31;
	public GameObject C32;
	public GameObject C33;
	public GameObject C34;
	public GameObject C35;
	public GameObject C36;
	public GameObject C37;
	public GameObject C38;
	public GameObject C39;




	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	

		if (gameObject.transform.position.x - GameObject.Find ("Main Camera").gameObject.transform.position.x < 24) {
			if (!DoOnce) {
				Rand = Random.Range (1, 9);
				if (Rand == 1) {Instantiate (C1, new Vector3 (gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform.parent.gameObject.transform);}
				if (Rand == 2) {Instantiate (C2, new Vector3 (gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform.parent.gameObject.transform);}
				if (Rand == 3) {Instantiate (C3, new Vector3 (gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform.parent.gameObject.transform);}
				if (Rand == 4) {Instantiate (C4, new Vector3 (gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform.parent.gameObject.transform);}
				if (Rand == 5) {Instantiate (C5, new Vector3 (gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform.parent.gameObject.transform);}
				if (Rand == 6) {Instantiate (C6, new Vector3 (gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform.parent.gameObject.transform);}
				if (Rand == 7) {Instantiate (C7, new Vector3 (gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform.parent.gameObject.transform);}
				if (Rand == 8) {Instantiate (C8, new Vector3 (gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform.parent.gameObject.transform);}
				if (Rand == 9) {Instantiate (C9, new Vector3 (gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform.parent.gameObject.transform);}
				if (Rand == 10) {Instantiate (C10, new Vector3 (gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform.parent.gameObject.transform);}
				if (Rand == 11) {Instantiate (C11, new Vector3 (gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform.parent.gameObject.transform);}
				if (Rand == 12) {Instantiate (C12, new Vector3 (gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform.parent.gameObject.transform);}
				if (Rand == 13) {Instantiate (C13, new Vector3 (gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform.parent.gameObject.transform);}
				if (Rand == 14) {Instantiate (C14, new Vector3 (gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform.parent.gameObject.transform);}
				if (Rand == 15) {Instantiate (C15, new Vector3 (gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform.parent.gameObject.transform);}
				if (Rand == 16) {Instantiate (C16, new Vector3 (gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform.parent.gameObject.transform);}
				if (Rand == 17) {Instantiate (C17, new Vector3 (gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform.parent.gameObject.transform);}
				if (Rand == 18) {Instantiate (C18, new Vector3 (gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform.parent.gameObject.transform);}
				if (Rand == 19) {Instantiate (C19, new Vector3 (gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform.parent.gameObject.transform);}
				if (Rand == 20) {Instantiate (C10, new Vector3 (gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform.parent.gameObject.transform);}
				if (Rand == 21) {Instantiate (C21, new Vector3 (gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform.parent.gameObject.transform);}
				if (Rand == 22) {Instantiate (C22, new Vector3 (gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform.parent.gameObject.transform);}
				if (Rand == 23) {Instantiate (C23, new Vector3 (gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform.parent.gameObject.transform);}
				if (Rand == 24) {Instantiate (C24, new Vector3 (gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform.parent.gameObject.transform);}
				if (Rand == 25) {Instantiate (C25, new Vector3 (gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform.parent.gameObject.transform);}
				if (Rand == 26) {Instantiate (C26, new Vector3 (gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform.parent.gameObject.transform);}
				if (Rand == 27) {Instantiate (C27, new Vector3 (gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform.parent.gameObject.transform);}
				if (Rand == 28) {Instantiate (C28, new Vector3 (gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform.parent.gameObject.transform);}
				if (Rand == 29) {Instantiate (C29, new Vector3 (gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform.parent.gameObject.transform);}
				if (Rand == 30) {Instantiate (C10, new Vector3 (gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform.parent.gameObject.transform);}
				if (Rand == 31) {Instantiate (C31, new Vector3 (gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform.parent.gameObject.transform);}
				if (Rand == 32) {Instantiate (C32, new Vector3 (gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform.parent.gameObject.transform);}
				if (Rand == 33) {Instantiate (C33, new Vector3 (gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform.parent.gameObject.transform);}
				if (Rand == 34) {Instantiate (C34, new Vector3 (gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform.parent.gameObject.transform);}
				if (Rand == 35) {Instantiate (C35, new Vector3 (gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform.parent.gameObject.transform);}
				if (Rand == 36) {Instantiate (C36, new Vector3 (gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform.parent.gameObject.transform);}
				if (Rand == 37) {Instantiate (C37, new Vector3 (gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform.parent.gameObject.transform);}
				if (Rand == 38) {Instantiate (C38, new Vector3 (gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform.parent.gameObject.transform);}
				if (Rand == 39) {Instantiate (C39, new Vector3 (gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation, gameObject.transform.parent.gameObject.transform);}
				DoOnce = true;


				Destroy (gameObject);
			}
		}



	}
}
