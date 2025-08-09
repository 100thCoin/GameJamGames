using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intermission : MonoBehaviour {

	public GameObject Dead;
	public GameObject TotalDead;
	public GameObject Behind;
	public GameObject Saved;



	// Use this for initialization
	void Start () {


		StartCoroutine (holdit ());


	}
	
	// Update is called once per frame
	void Update () {
		
		if (GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().LoadNext) {
			Destroy (gameObject);
		}
	}

	IEnumerator holdit()
	{
		yield return new WaitForSeconds (1);
		Dead.gameObject.GetComponent<TextMesh> ().text = "" + GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().DeadPloogies;
		TotalDead.gameObject.GetComponent<TextMesh> ().text = "" + GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().TotalDeadPloogies;
		Behind.gameObject.GetComponent<TextMesh> ().text = "" + GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().LeftBehindPloogies;
		Saved.gameObject.GetComponent<TextMesh> ().text = "" + GameObject.Find ("Main").gameObject.GetComponent<DataHolder> ().RemainingPloogies;





	}




}
