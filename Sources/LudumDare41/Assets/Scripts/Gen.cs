using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gen : MonoBehaviour {

	public int WieViele;
	public GameObject GenerationTile;
	public GameObject ForestBG;

	public GameObject Town;

	public bool DragonGrass;


	public bool DoneOnce;
	public bool DOnceOnce2;

	// Use this for initialization
	void Start () {
	
		WieViele = 25 + GameObject.Find("Main").gameObject.GetComponent<DataHolder>().Town;

		float P = 0;
		float T = WieViele;
		float i = 0;

		while (WieViele > 0)
		{
			P = i/T;
			Instantiate(GenerationTile,new Vector2((256* P -128) + gameObject.transform.position.x,gameObject.transform.position.y+4),gameObject.transform.rotation,gameObject.transform);
			i = i+1;
			WieViele = WieViele - 1;
		}



	}
	
	// Update is called once per frame
	void Update () {

		if(GameObject.Find("Main Camera").gameObject.transform.position.x > gameObject.transform.position.x - 114 && !DoneOnce)
		{

			Instantiate(ForestBG,new Vector2(gameObject.transform.position.x -30,gameObject.transform.position.y+ 14),gameObject.transform.rotation,GameObject.Find("Main Camera").gameObject.transform);
			DoneOnce = true;
		}

		if(GameObject.Find("Main Camera").gameObject.transform.position.x > gameObject.transform.position.x + 110 && !DOnceOnce2)
		{
			if(!DragonGrass)
			{
			Instantiate(Town,new Vector2(gameObject.transform.position.x + 152,gameObject.transform.position.y),gameObject.transform.rotation,GameObject.Find("Map").gameObject.transform);
			


			}
			else
			{
				Instantiate(Town,new Vector2(gameObject.transform.position.x + 256,gameObject.transform.position.y),gameObject.transform.rotation,GameObject.Find("Map").gameObject.transform);



			}
			Destroy(this);
		}


	}
}
