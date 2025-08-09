using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratedTile : MonoBehaviour {

	public GameObject[] ThingsICanSpawn1;
	public GameObject[] ThingsICanSpawn2;
	public GameObject[] ThingsICanSpawn3;
	public GameObject[] ThingsICanSpawn4;
	public GameObject[] ThingsICanSpawn5;
	public GameObject[] ThingsICanSpawn6;
	public GameObject[] ThingsICanSpawn7;
	public GameObject[] ThingsICanSpawn8;
	public GameObject[] ThingsICanSpawn9;
	public GameObject[] ThingsICanSpawn10;




	// Use this for initialization
	void Start () {

		if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Town==0){ Instantiate(ThingsICanSpawn1[Random.Range(0,15)],gameObject.transform.position,gameObject.transform.rotation,gameObject.transform);}
		if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Town==1){ Instantiate(ThingsICanSpawn2[Random.Range(0,15)],gameObject.transform.position,gameObject.transform.rotation,gameObject.transform);}
		if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Town==2){ Instantiate(ThingsICanSpawn3[Random.Range(0,15)],gameObject.transform.position,gameObject.transform.rotation,gameObject.transform);}
		if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Town==3){ Instantiate(ThingsICanSpawn4[Random.Range(0,15)],gameObject.transform.position,gameObject.transform.rotation,gameObject.transform);}
		if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Town==4){ Instantiate(ThingsICanSpawn5[Random.Range(0,15)],gameObject.transform.position,gameObject.transform.rotation,gameObject.transform);}
		if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Town==5){ Instantiate(ThingsICanSpawn6[Random.Range(0,15)],gameObject.transform.position,gameObject.transform.rotation,gameObject.transform);}
		if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Town==6){ Instantiate(ThingsICanSpawn7[Random.Range(0,15)],gameObject.transform.position,gameObject.transform.rotation,gameObject.transform);}
		if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Town==7){ Instantiate(ThingsICanSpawn8[Random.Range(0,15)],gameObject.transform.position,gameObject.transform.rotation,gameObject.transform);}
		if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Town==8){ Instantiate(ThingsICanSpawn9[Random.Range(0,15)],gameObject.transform.position,gameObject.transform.rotation,gameObject.transform);}
		if(GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Town==9){ Instantiate(ThingsICanSpawn10[Random.Range(0,15)],gameObject.transform.position,gameObject.transform.rotation,gameObject.transform);}




		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
