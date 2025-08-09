using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DressNPC : MonoBehaviour {

	public RuntimeAnimatorController[] Hat;
	public RuntimeAnimatorController[] Cloak;
	public Sprite[] Shoes;
	public RuntimeAnimatorController Face;

	public GameObject ShoesGO;
	public GameObject HatGO;
	public GameObject CloakGO;
	public GameObject FaceGO;


	// Use this for initialization
	void Start () {

		ShoesGO.gameObject.GetComponent<SpriteRenderer>().sprite = Shoes[Random.Range(0,5)];
		HatGO.gameObject.GetComponent<Animator>().runtimeAnimatorController = Hat[Random.Range(0,9)];
		CloakGO.gameObject.GetComponent<Animator>().runtimeAnimatorController = Cloak[Random.Range(0,7)];
		FaceGO.gameObject.GetComponent<Animator>().runtimeAnimatorController = Face;





	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
