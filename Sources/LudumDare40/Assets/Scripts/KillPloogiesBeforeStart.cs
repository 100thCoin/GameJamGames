using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPloogiesBeforeStart : MonoBehaviour {
	public int WieViele;
	public GameObject Ghost;

	// Use this for initialization
	void Start () {
		WieViele = GameObject.Find ("Main").GetComponent<DataHolder> ().PreviosDeadPloogies;

		while (WieViele > 0) {

			Destroy(gameObject.transform.Find("Ploogy"+WieViele).gameObject);

			GameObject.Find ("Main").GetComponent<DataHolder> ().CurrentPloogies -= 1;
			Instantiate (Ghost,GameObject.Find ("Player").gameObject.transform.position, gameObject.transform.rotation, gameObject.transform.parent.transform);
			WieViele = WieViele - 1;
		}


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
