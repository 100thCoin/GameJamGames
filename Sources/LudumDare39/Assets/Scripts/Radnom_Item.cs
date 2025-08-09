using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radnom_Item : MonoBehaviour {
	public float Radnom;

	public GameObject Grid;
	public GameObject Chest;


	// Use this for initialization
	void Start () {

		Radnom = Random.Range (1, 5);

		if (Radnom == 1) {Instantiate (Grid, new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,2), gameObject.transform.rotation,GameObject.Find("InGame(Clone)").transform);}
		if (Radnom == 2) {Instantiate (Chest, new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,2), gameObject.transform.rotation,GameObject.Find("InGame(Clone)").transform);}
		if (Radnom == 3) {Instantiate (Chest, new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,2), gameObject.transform.rotation,GameObject.Find("InGame(Clone)").transform);}

		Destroy (gameObject);


	}

	// Update is called once per frame
	void Update () {

	}
}