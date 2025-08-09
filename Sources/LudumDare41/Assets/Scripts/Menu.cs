using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

	public bool QuitToTitle;

	public bool Back;

	public bool Creds;
	public bool Start;
	public bool Howto;
	public bool Slow;
	public bool fast;

	public bool Quit;

	public GameObject Game;

	// Update is called once per frame
	void OnMouseOver () {


		if(Input.GetKeyDown(KeyCode.Mouse0))
		{
			if(Creds)
			{

				GameObject.Find("Camera").gameObject.transform.position = new Vector3(0,-30,-10);

			}

			if(Start)
			{

				GameObject.Find("Camera").gameObject.transform.position = new Vector3(0,-60,-10);

			}
			if(Howto)
			{

				GameObject.Find("Camera").gameObject.transform.position = new Vector3(0,-90,-10);

			}



			if(Quit)
			{

				Application.Quit();

			}
			if(Back)
			{

				GameObject.Find("Camera").gameObject.transform.position = new Vector3(0,0,-10);

			}


			if(Slow || fast)
			{

				GameObject test = Instantiate(Game,new Vector3(0,0,-10),gameObject.transform.rotation) as GameObject;

				GameObject.Find("Main").gameObject.name = "Flubber";

				test.name = "Main";

				if(fast)
				{
				test.gameObject.GetComponent<DataHolder>().FastMode = true;
				}
				Destroy(GameObject.Find("Flubber").gameObject);








			}







		}




	}
}
