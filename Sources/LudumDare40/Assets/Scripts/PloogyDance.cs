using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PloogyDance : MonoBehaviour {

	public bool began;

	public GameObject heart;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.tag == "Ploogy" && !began) {
			began = true;
			StartCoroutine (dance ());

		}

	}

	IEnumerator dance()
	{
		yield return new WaitForSeconds(4);

		Instantiate (heart,new Vector3(gameObject.transform.position.x,gameObject.transform.position.y + 6,0),gameObject.transform.rotation);


		yield return new WaitForSeconds(4);

		GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().TrueWin = true;




	}





}
