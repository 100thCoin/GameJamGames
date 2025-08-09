using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadnomEnemy : MonoBehaviour {

	public float Radnom;

	public GameObject Crawlb;
	public GameObject Lamp;
	public GameObject Outlet;
	public GameObject Iron;
	public GameObject Phone;


	// Use this for initialization
	void Start () {

		Radnom = Random.Range (1, 7);

		if (Radnom == 1) {Instantiate (Crawlb, gameObject.transform.position, gameObject.transform.rotation,GameObject.Find("InGame(Clone)").transform);}
		if (Radnom == 2) {Instantiate (Lamp, gameObject.transform.position, gameObject.transform.rotation,GameObject.Find("InGame(Clone)").transform);}
		if (Radnom == 3) {Instantiate (Outlet, gameObject.transform.position, gameObject.transform.rotation,GameObject.Find("InGame(Clone)").transform);}
		if (Radnom == 4) {Instantiate (Iron, gameObject.transform.position, gameObject.transform.rotation,GameObject.Find("InGame(Clone)").transform);}
		if (Radnom == 5) {Instantiate (Phone, gameObject.transform.position, gameObject.transform.rotation,GameObject.Find("InGame(Clone)").transform);}
		if (Radnom == 6) {Instantiate (Crawlb, gameObject.transform.position, gameObject.transform.rotation,GameObject.Find("InGame(Clone)").transform);}

		Destroy (gameObject);


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
